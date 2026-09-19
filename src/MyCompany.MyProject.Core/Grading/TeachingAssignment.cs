namespace MyCompany.MyProject.Grading
{
    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;
    using MyCompany.MyProject.Academics;
    using MyCompany.MyProject.Profiles;
    using System.ComponentModel.DataAnnotations.Schema;

        [Table("TeachingAssignments")]
        public class TeachingAssignment : FullAuditedEntity<long>, IMustHaveTenant
        {
            public int TenantId { get; set; }
            public long TeacherId { get; set; }
            [ForeignKey("TeacherId")]
            public virtual Teacher Teacher { get; set; }
            public int SubjectId { get; set; }
            [ForeignKey("SubjectId")]
            public virtual Subject Subject { get; set; }
            public int ClassId { get; set; }
            [ForeignKey("ClassId")]
            public virtual Class Class { get; set; }
            public int SemesterId { get; set; }
            [ForeignKey("SemesterId")]
            public virtual Semester Semester { get; set; }
        }
    }

