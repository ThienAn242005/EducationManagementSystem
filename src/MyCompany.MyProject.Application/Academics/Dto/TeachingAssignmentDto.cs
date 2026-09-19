using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyCompany.MyProject.Grading;
using System.ComponentModel.DataAnnotations;

namespace MyCompany.MyProject.Academics.Dto
{
    [AutoMapTo(typeof(TeachingAssignment))]
    public class CreateAssignmentDto : IEntityDto<long>
    {
        public long Id { get; set; }
        [Required]
        public long TeacherId { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int SemesterId { get; set; }
    }

    [AutoMapFrom(typeof(TeachingAssignment))]
    public class TeachingAssignmentDto : EntityDto<long>
    {
        public long TeacherId { get; set; }
        public string TeacherFullName { get; set; }

        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
    }
}
