using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany.MyProject.Academics
{
        [Table("Subjects")]
        public class Subject : FullAuditedEntity<int>, IMustHaveTenant
        {
            public int TenantId { get; set; }

            [Required]
            [MaxLength(20)]
            public string Code { get; set; }

            [Required]
            [MaxLength(100)]
            public string Name { get; set; }
            public int Factor { get; set; } = 1;

            public int NumberOfLessons { get; set; }
            public GradeLevel Grade { get; set; }
    }
    }
