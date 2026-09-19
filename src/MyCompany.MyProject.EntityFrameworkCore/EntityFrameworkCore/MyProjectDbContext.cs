using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Academics;
using MyCompany.MyProject.Authorization.Roles;
using MyCompany.MyProject.Authorization.Users;
using MyCompany.MyProject.Grading;
using MyCompany.MyProject.MultiTenancy;
using MyCompany.MyProject.Profiles;

namespace MyCompany.MyProject.EntityFrameworkCore;

public class MyProjectDbContext : AbpZeroDbContext<Tenant, Role, User, MyProjectDbContext>
{
    public virtual DbSet<Teacher> Teachers { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<Semester> Semesters { get; set; }
    public virtual DbSet<Class> Classes { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<TeachingAssignment> TeachingAssignments { get; set; }
    public virtual DbSet<GradeLockStatus> GradeLockStatuses { get; set; }
    public virtual DbSet<GradeRecord> GradeRecords { get; set; }

    public MyProjectDbContext(DbContextOptions<MyProjectDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Ràng buộc: Mỗi lớp, mỗi môn trong một kỳ chỉ có duy nhất 1 giáo viên phụ trách
        modelBuilder.Entity<TeachingAssignment>()
            .HasIndex(a => new { a.ClassId, a.SubjectId, a.SemesterId })
            .IsUnique();

        // 2. Ràng buộc: Một học sinh chỉ có 1 bản ghi điểm cho mỗi môn trong một kỳ
        modelBuilder.Entity<GradeRecord>()
            .HasIndex(g => new { g.StudentId, g.SubjectId, g.SemesterId })
            .IsUnique();

        // 3. Ràng buộc: Trạng thái khóa sổ điểm duy nhất cho mỗi lớp/môn/kỳ
        modelBuilder.Entity<GradeLockStatus>()
            .HasIndex(l => new { l.ClassId, l.SubjectId, l.SemesterId })
            .IsUnique();

        // 4. Tránh lỗi đụng độ Cascade Delete giữa Class và Teacher
        modelBuilder.Entity<Class>()
            .HasOne(c => c.HeadTeacher)
            .WithMany()
            .HasForeignKey(c => c.HeadTeacherId)
            .OnDelete(DeleteBehavior.Restrict);
        // 5. Mã môn không được trùng trong cùng Tenant
        modelBuilder.Entity<Subject>()
            .HasIndex(s => new { s.TenantId, s.Code })
            .IsUnique();

        // 6. Tên môn không được trùng trong cùng Tenant
        modelBuilder.Entity<Subject>()
            .HasIndex(s => new { s.TenantId, s.Name })
            .IsUnique();

        // 7. Tên học kỳ không được trùng trong cùng Tenant
        modelBuilder.Entity<Semester>()
            .HasIndex(s => new { s.TenantId, s.Name })
            .IsUnique();
        // 8. Tên lớp học không được trùng trong cùng Tenant
        modelBuilder.Entity<Class>()
            .HasIndex(c => new { c.TenantId, c.ClassName })
            .IsUnique();
    }
}
