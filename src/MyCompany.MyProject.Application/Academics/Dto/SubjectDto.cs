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
    public class SubjectDtoBase
    {
        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1, 10)]
        public int Factor { get; set; } = 1;
    }

    [AutoMapFrom(typeof(Subject))]
    public class SubjectDto : SubjectDtoBase, IEntityDto<int>
    {
        public int Id { get; set; }
    }

    [AutoMapTo(typeof(Subject))]
    public class CreateUpdateSubjectDto : SubjectDtoBase, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}
