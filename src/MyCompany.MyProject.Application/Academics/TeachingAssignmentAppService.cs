using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Academics.Dto;
using MyCompany.MyProject.Grading;
using System.Linq;

namespace MyCompany.MyProject.Academics
{
    public class TeachingAssignmentAppService
        : AsyncCrudAppService<
            TeachingAssignment,
            TeachingAssignmentDto,
            long,
            TeachingAssignmentResultRequestDto,
            CreateAssignmentDto>
    {
        public TeachingAssignmentAppService(
            IRepository<TeachingAssignment, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<TeachingAssignment> CreateFilteredQuery(
            TeachingAssignmentResultRequestDto input)
        {
            return Repository.GetAll()
                .Include(x => x.Teacher)
                .Include(x => x.Class)
                .Include(x => x.Subject)
                .Include(x => x.Semester);
        }

        protected override TeachingAssignmentDto MapToEntityDto(
            TeachingAssignment entity)
        {
            return new TeachingAssignmentDto
            {
                Id = entity.Id,

                TeacherId = entity.TeacherId,
                TeacherFullName = entity.Teacher != null
                    ? entity.Teacher.FullName
                    : "-",

                ClassId = entity.ClassId,
                ClassName = entity.Class != null
                    ? entity.Class.ClassName
                    : "-",

                SubjectId = entity.SubjectId,
                SubjectName = entity.Subject != null
                    ? entity.Subject.Name
                    : "-",

                SemesterId = entity.SemesterId,
                SemesterName = entity.Semester != null
                    ? entity.Semester.Name
                    : "-"
            };
        }
    }
}