using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Academics.Dto
{
    public class SemesterDtoBase
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int StartYear { get; set; }

        public bool IsCurrent { get; set; }
    }

    [AutoMapFrom(typeof(Semester))]
    public class SemesterDto : SemesterDtoBase, IEntityDto<int>
    {
        public int Id { get; set; }
    }

    [AutoMapTo(typeof(Semester))]
    public class CreateUpdateSemesterDto : SemesterDtoBase, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}
