using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompany.MyProject.Academics;
using MyCompany.MyProject.Profiles;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany.MyProject.Grading
{
    [Table("QlsGradeLockStatuses")]
    public class GradeLockStatus : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        public int SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockedDate { get; set; }
        public long? LockedByUserId { get; set; }
        [ForeignKey("LockedByUserId")]
        public virtual Teacher LockedByUser { get; set; }
    }
}
