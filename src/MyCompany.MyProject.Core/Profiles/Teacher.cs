using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompany.MyProject.Academics;
using MyCompany.MyProject.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany.MyProject.Profiles
{
    public class Teacher : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required]
        [MaxLength(20)]
        public string TeacherCode { get; set; }
        [Required]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender {  get; set; }
        public DateTime? DayOfBirth { get; set; }
        public int? MainSubjectId { get; set; }
        [ForeignKey("MainSubjectId")]
        public virtual Subject MainSubject { get; set; }

        public bool IsPrincipal { get; set; }
    }
}
