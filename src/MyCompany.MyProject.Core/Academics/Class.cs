using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompany.MyProject.Profiles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany.MyProject.Academics
{
    [Table("Classes")]
    public class Class : FullAuditedEntity<int>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        public GradeLevel Grade { get; set; } 
        public long? HeadTeacherId { get; set; }
        [ForeignKey("HeadTeacherId")]
        public virtual Teacher HeadTeacher { get; set; }
    }
}
