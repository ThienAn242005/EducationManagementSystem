using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Profiles.Dto
{
    public class StudentDtoBase : IEntityDto<long>
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string StudentCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? DayOfBirth { get; set; }

        public bool Gender { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public int ClassId { get; set; }
    }

    [AutoMapFrom(typeof(Student))]
    public class StudentDto : StudentDtoBase, IEntityDto<long>
    {
        public long Id { get; set; }
        public string ClassName { get; set; }
    }

    [AutoMapTo(typeof(Student))]
    public class CreateUpdateStudentDto : StudentDtoBase
    {
    }
}
