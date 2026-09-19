using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Academics
{
    [Table("Semesters")]
    public class Semester : FullAuditedEntity<int>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int StartYear { get; set; }

        public int SemesterNumber { get; set; }
        public bool IsCurrent { get; set; }
        [NotMapped]
        public string AcademicYearDisplay => $"{StartYear} - {StartYear + 1}";

    }
}
