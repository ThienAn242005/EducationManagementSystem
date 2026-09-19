using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompany.MyProject.Academics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Profiles
{
    [Table("Students")]
    public class Student : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        [MaxLength(20)]
        public string StudentCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Gender { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string ParentPhoneNumber { get; set; }
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
    }
}
