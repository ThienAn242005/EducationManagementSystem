namespace MyCompany.MyProject.Authorization;

public static class PermissionNames
{
    public const string Pages_Tenants = "Pages.Tenants";

    public const string Pages_Users = "Pages.Users";
    public const string Pages_Users_Activation = "Pages.Users.Activation";

    public const string Pages_Roles = "Pages.Roles";
    // 1. Quản lý danh mục đào tạo
    public const string Pages_Academics = "Pages.Academics";
    public const string Pages_Academics_Subjects = "Pages.Academics.Subjects";
    public const string Pages_Academics_Semesters = "Pages.Academics.Semesters";
    public const string Pages_Academics_Classes = "Pages.Academics.Classes";

    // 2. Quản lý thành viên nhà trường
    public const string Pages_SchoolMembers = "Pages.SchoolMembers";
    public const string Pages_SchoolMembers_Teachers = "Pages.SchoolMembers.Teachers";
    public const string Pages_SchoolMembers_Students = "Pages.SchoolMembers.Students";

    // 3. Phân công giảng dạy
    public const string Pages_TeachingAssignments = "Pages.TeachingAssignments";

    // 4. Phân hệ Điểm số
    public const string Pages_Grades = "Pages.Grades";
    public const string Pages_Grades_View = "Pages.Grades.View";
    public const string Pages_Grades_Input = "Pages.Grades.Input";
    public const string Pages_Grades_Lock = "Pages.Grades.Lock";
}
