using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompany.MyProject.Academics;
using MyCompany.MyProject.Profiles;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany.MyProject.Grading
{
    [Table("GradeRecords")]
    public class GradeRecord : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public long StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        public int SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? ScoreTX1 { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal? ScoreTX2 { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal? ScoreTX3 { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal? ScoreGK { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal? ScoreCK { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal? AverageScore { get; set; }
        public AcademicPerformance Performance { get; set; } = AcademicPerformance.None;
    }
}
