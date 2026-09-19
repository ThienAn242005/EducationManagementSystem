using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace MyCompany.MyProject.Profiles.Dto
{
    public class TeacherDtoBase : EntityDto<long>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string TeacherCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public bool Gender { get; set; }

        public DateTime? DayOfBirth { get; set; }

        public int? MainSubjectId { get; set; }

        public bool IsPrincipal { get; set; }
    }

    [AutoMapFrom(typeof(Teacher))]
    public class TeacherDto : TeacherDtoBase
    {
        public string MainSubjectName { get; set; }
    }

    [AutoMapTo(typeof(Teacher))]
    public class CreateUpdateTeacherDto : TeacherDtoBase
    {
    }
}