using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyCompany.MyProject.Authorization;

public class MyProjectAuthorizationProvider : AuthorizationProvider
{
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
        context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
        context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
        context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        // Nhóm Học thuật
        var academics = context.CreatePermission(PermissionNames.Pages_Academics, L("AcademicsManagement"));
        academics.CreateChildPermission(PermissionNames.Pages_Academics_Subjects, L("Subjects"));
        academics.CreateChildPermission(PermissionNames.Pages_Academics_Semesters, L("Semesters"));
        academics.CreateChildPermission(PermissionNames.Pages_Academics_Classes, L("Classes"));

        // Nhóm Thành viên
        var schoolMembers = context.CreatePermission(PermissionNames.Pages_SchoolMembers, L("SchoolMembersManagement"));
        schoolMembers.CreateChildPermission(PermissionNames.Pages_SchoolMembers_Teachers, L("Teachers"));
        schoolMembers.CreateChildPermission(PermissionNames.Pages_SchoolMembers_Students, L("Students"));

        // Phân công giảng dạy
        context.CreatePermission(PermissionNames.Pages_TeachingAssignments, L("TeachingAssignments"));

        // Quản lý điểm số
        var grades = context.CreatePermission(PermissionNames.Pages_Grades, L("GradesManagement"));
        grades.CreateChildPermission(PermissionNames.Pages_Grades_View, L("GradesView"));
        grades.CreateChildPermission(PermissionNames.Pages_Grades_Input, L("GradesInput"));
        grades.CreateChildPermission(PermissionNames.Pages_Grades_Lock, L("GradesLock"));
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, MyProjectConsts.LocalizationSourceName);
    }
}
