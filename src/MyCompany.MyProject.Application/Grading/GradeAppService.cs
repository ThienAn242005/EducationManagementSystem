using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Authorization;
using MyCompany.MyProject.Grading.Dto;
using MyCompany.MyProject.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Grading
{
    [AbpAuthorize]
    public class GradeAppService : MyProjectAppServiceBase
    {
        private readonly IRepository<GradeRecord, long> _gradeRecordRepo;
        private readonly IRepository<GradeLockStatus, long> _lockStatusRepo;
        private readonly IRepository<Student, long> _studentRepo;
        private readonly IRepository<Teacher, long> _teacherRepo;
        private readonly IRepository<TeachingAssignment, long> _assignmentRepo;

        public GradeAppService(
            IRepository<GradeRecord, long> gradeRecordRepo,
            IRepository<GradeLockStatus, long> lockStatusRepo,
            IRepository<Student, long> studentRepo,
            IRepository<Teacher, long> teacherRepo,
            IRepository<TeachingAssignment, long> assignmentRepo)
        {
            _gradeRecordRepo = gradeRecordRepo;
            _lockStatusRepo = lockStatusRepo;
            _studentRepo = studentRepo;
            _teacherRepo = teacherRepo;
            _assignmentRepo = assignmentRepo;
        }

        // 1. Tải bảng điểm
        [AbpAuthorize(PermissionNames.Pages_Grades_View)]
        public async Task<List<GradeStudentRowDto>> GetGradeBookAsync(GetGradeBookInput input)
        {
            var students = await _studentRepo.GetAll()
                .Where(s => s.ClassId == input.ClassId)
                .OrderBy(s => s.StudentCode)
                .ToListAsync();

            var currentGrades = await _gradeRecordRepo.GetAll()
                .Where(g => g.SubjectId == input.SubjectId && g.SemesterId == input.SemesterId)
                .ToListAsync();

            var gradeMap = currentGrades.ToDictionary(g => g.StudentId);

            return students.Select(student =>
            {
                gradeMap.TryGetValue(student.Id, out var record);

                return new GradeStudentRowDto
                {
                    StudentId = student.Id,
                    StudentCode = student.StudentCode,
                    FullName = student.FullName,
                    GradeRecordId = record?.Id,
                    ScoreTX1 = record?.ScoreTX1,
                    ScoreTX2 = record?.ScoreTX2,
                    ScoreTX3 = record?.ScoreTX3,
                    ScoreGK = record?.ScoreGK,
                    ScoreCK = record?.ScoreCK,
                    AverageScore = record?.AverageScore,
                    Performance = record?.Performance ?? AcademicPerformance.None,
                    PerformanceDisplay = record != null ? GetPerformanceName(record.Performance) : "Chưa có"
                };
            }).ToList();
        }

        // 2. Lưu bảng điểm
        [AbpAuthorize(PermissionNames.Pages_Grades_Input)]
        public async Task SaveGradesAsync(SaveGradeListInput input)
        {
            // Kiểm tra trạng thái khóa sổ
            var isLocked = await _lockStatusRepo.GetAll()
                .AnyAsync(l => l.ClassId == input.ClassId &&
                               l.SubjectId == input.SubjectId &&
                               l.SemesterId == input.SemesterId &&
                               l.IsLocked);

            if (isLocked)
            {
                throw new UserFriendlyException("Sổ điểm của môn này tại lớp đã bị Hiệu trưởng khóa, không thể chỉnh sửa!");
            }

            // Kiểm tra phân công giảng dạy
            await ValidateTeachingAssignmentAsync(input.ClassId, input.SubjectId, input.SemesterId);

            // Tối ưu Query: Kéo toàn bộ điểm hiện có của các học sinh được gửi lên về một lần
            var studentIds = input.GradeRecords.Select(r => r.StudentId).ToList();
            var existingGrades = await _gradeRecordRepo.GetAll()
                .Where(g => g.SubjectId == input.SubjectId &&
                            g.SemesterId == input.SemesterId &&
                            studentIds.Contains(g.StudentId))
                .ToListAsync();

            var gradeLookup = existingGrades.ToDictionary(g => g.StudentId);

            foreach (var item in input.GradeRecords)
            {
                if (!gradeLookup.TryGetValue(item.StudentId, out var gradeRecord))
                {
                    gradeRecord = new GradeRecord
                    {
                        StudentId = item.StudentId,
                        SubjectId = input.SubjectId,
                        SemesterId = input.SemesterId
                    };
                    await _gradeRecordRepo.InsertAsync(gradeRecord);
                }

                gradeRecord.ScoreTX1 = item.ScoreTX1;
                gradeRecord.ScoreTX2 = item.ScoreTX2;
                gradeRecord.ScoreTX3 = item.ScoreTX3;
                gradeRecord.ScoreGK = item.ScoreGK;
                gradeRecord.ScoreCK = item.ScoreCK;

                // Tính ĐTB và suy ra Xếp loại ngay tại tầng AppService
                var avg = CalculateAverage(item.ScoreTX1, item.ScoreTX2, item.ScoreTX3, item.ScoreGK, item.ScoreCK);
                gradeRecord.AverageScore = avg;
                gradeRecord.Performance = DeterminePerformance(avg);
            }
        }

        // 3. Khóa / Mở sổ điểm (ABP tự động xử lý Creator/Modifier Audit)
        [AbpAuthorize(PermissionNames.Pages_Grades_Lock)]
        public async Task SetLockStatusAsync(LockGradeInput input)
        {
            var lockStatus = await _lockStatusRepo.FirstOrDefaultAsync(l =>
                l.ClassId == input.ClassId &&
                l.SubjectId == input.SubjectId &&
                l.SemesterId == input.SemesterId);

            if (lockStatus == null)
            {
                lockStatus = new GradeLockStatus
                {
                    ClassId = input.ClassId,
                    SubjectId = input.SubjectId,
                    SemesterId = input.SemesterId,
                    IsLocked = input.IsLocked
                };
                await _lockStatusRepo.InsertAsync(lockStatus);
            }
            else
            {
                lockStatus.IsLocked = input.IsLocked;
            }
        }

        // ================= PRIVATE HELPERS =================

        private async Task ValidateTeachingAssignmentAsync(int classId, int subjectId, int semesterId)
        {
            if (await IsGrantedAsync(PermissionNames.Pages_Grades_Lock)) return;

            var currentUserId = AbpSession.UserId;
            var currentTeacher = await _teacherRepo.FirstOrDefaultAsync(t => t.UserId == currentUserId);
            if (currentTeacher == null)
            {
                throw new UserFriendlyException("Tài khoản của bạn không liên kết với hồ sơ Giáo viên!");
            }

            var isAssigned = await _assignmentRepo.GetAll()
                .AnyAsync(a => a.TeacherId == currentTeacher.Id &&
                               a.ClassId == classId &&
                               a.SubjectId == subjectId &&
                               a.SemesterId == semesterId);

            if (!isAssigned)
            {
                throw new UserFriendlyException("Bạn không được phân công dạy môn học này tại lớp đã chọn!");
            }
        }

        private static decimal? CalculateAverage(decimal? tx1, decimal? tx2, decimal? tx3, decimal? gk, decimal? ck)
        {
            decimal totalScore = 0;
            int totalWeight = 0;

            if (tx1.HasValue) { totalScore += tx1.Value; totalWeight += 1; }
            if (tx2.HasValue) { totalScore += tx2.Value; totalWeight += 1; }
            if (tx3.HasValue) { totalScore += tx3.Value; totalWeight += 1; }
            if (gk.HasValue) { totalScore += gk.Value * 2; totalWeight += 2; }
            if (ck.HasValue) { totalScore += ck.Value * 3; totalWeight += 3; }

            return totalWeight > 0 ? Math.Round(totalScore / totalWeight, 1) : null;
        }

        private static AcademicPerformance DeterminePerformance(decimal? avg)
        {
            if (!avg.HasValue) return AcademicPerformance.None;
            if (avg.Value >= 8.0m) return AcademicPerformance.Good;
            if (avg.Value >= 6.5m) return AcademicPerformance.Fair;
            if (avg.Value >= 5.0m) return AcademicPerformance.Average;
            return AcademicPerformance.Weak;
        }

        private static string GetPerformanceName(AcademicPerformance performance) => performance switch
        {
            AcademicPerformance.Good => "Giỏi",
            AcademicPerformance.Fair => "Khá",
            AcademicPerformance.Average => "Trung bình",
            AcademicPerformance.Weak => "Yếu",
            _ => "Chưa có"
        };
    }
}