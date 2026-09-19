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
    public class ClassDtoBase
    {
        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        public GradeLevel Grade { get; set; }
    }

    // DTO hiển thị danh sách / chi tiết (kế thừa EntityDto để có trường Id: int)
    [AutoMapFrom(typeof(Class))]
    public class ClassDto : ClassDtoBase, IEntityDto<int>
    {
        public int Id { get; set; }
    }

    // DTO tạo mới và cập nhật (AutoMap sang Entity Class)
    [AutoMapTo(typeof(Class))]
    public class CreateUpdateClassDto : EntityDto<int>
    {
        public string ClassName { get; set; }
        public GradeLevel Grade { get; set; }
    }
}
