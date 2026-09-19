using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using MyCompany.MyProject.Authorization;

namespace MyCompany.MyProject.Web.Startup;

/// <summary>
/// This class defines menus for the application.
/// </summary>
public class MyProjectNavigationProvider : NavigationProvider
{
    public override void SetNavigation(INavigationProviderContext context)
    {
        context.Manager.MainMenu
            .AddItem(
                new MenuItemDefinition(
                    PageNames.About,
                    L("About"),
                    url: "About",
                    icon: "fas fa-info-circle"
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    PageNames.Home,
                    L("HomePage"),
                    url: "",
                    icon: "fas fa-home",
                    requiresAuthentication: true
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    PageNames.Tenants,
                    L("Tenants"),
                    url: "Tenants",
                    icon: "fas fa-building",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    PageNames.Users,
                    L("Users"),
                    url: "Users",
                    icon: "fas fa-users",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                )
            )
            .AddItem(
                new MenuItemDefinition(
                    PageNames.Roles,
                    L("Roles"),
                    url: "Roles",
                    icon: "fas fa-theater-masks",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                )
            )
            // ==================== QUẢN LÝ ĐÀO TẠO (ACADEMICS) ====================
            .AddItem(
                new MenuItemDefinition(
                    "Academics",
                    new FixedLocalizableString("Quản lý Đào tạo"),
                    icon: "fas fa-graduation-cap"
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Classes",
                        new FixedLocalizableString("Lớp học"),
                        url: "Class",
                        icon: "fas fa-chalkboard"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Subjects",
                        new FixedLocalizableString("Môn học"),
                        url: "Subject",
                        icon: "fas fa-book"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Semesters",
                        new FixedLocalizableString("Học kỳ"),
                        url: "Semester",
                        icon: "fas fa-calendar-alt"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "TeachingAssignments",
                        new FixedLocalizableString("Phân công Giảng dạy"),
                        url: "TeachingAssignment",
                        icon: "fas fa-tasks"
                    )
                )
            )
            // ==================== QUẢN LÝ HỒ SƠ (PROFILES) ====================
            .AddItem(
                new MenuItemDefinition(
                    "Profiles",
                    new FixedLocalizableString("Quản lý Hồ sơ"),
                    icon: "fas fa-id-card"
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Students",
                        new FixedLocalizableString("Hồ sơ Học sinh"),
                        url: "Student",
                        icon: "fas fa-user-graduate"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Teachers",
                        new FixedLocalizableString("Hồ sơ Giáo viên"),
                        url: "Teacher",
                        icon: "fas fa-chalkboard-teacher"
                    )
                )
            )
            // ==================== SỔ ĐIỂM HỌC SINH (GRADING) ====================
            .AddItem(
                new MenuItemDefinition(
                    "Grading",
                    new FixedLocalizableString("Sổ Điểm Học Sinh"),
                    url: "Grade",
                    icon: "fas fa-table",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Grades_View)
                )
            )
            // ==================== DEMO / DEFAULT MENU ====================
            .AddItem( // Menu items below is just for demonstration!
                new MenuItemDefinition(
                    "MultiLevelMenu",
                    L("MultiLevelMenu"),
                    icon: "fas fa-circle"
                ).AddItem(
                    new MenuItemDefinition(
                        "AspNetBoilerplate",
                        new FixedLocalizableString("ASP.NET Boilerplate"),
                        icon: "far fa-circle"
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetBoilerplateHome",
                            new FixedLocalizableString("Home"),
                            url: "https://aspnetboilerplate.com?ref=abptmpl",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetBoilerplateTemplates",
                            new FixedLocalizableString("Templates"),
                            url: "https://aspnetboilerplate.com/Templates?ref=abptmpl",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetBoilerplateSamples",
                            new FixedLocalizableString("Samples"),
                            url: "https://aspnetboilerplate.com/Samples?ref=abptmpl",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetBoilerplateDocuments",
                            new FixedLocalizableString("Documents"),
                            url: "https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "AspNetZero",
                        new FixedLocalizableString("ASP.NET Zero"),
                        icon: "far fa-circle"
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetZeroHome",
                            new FixedLocalizableString("Home"),
                            url: "https://aspnetzero.com?ref=abptmpl",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetZeroFeatures",
                            new FixedLocalizableString("Features"),
                            url: "https://aspnetzero.com/Features?ref=abptmpl",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetZeroPricing",
                            new FixedLocalizableString("Pricing"),
                            url: "https://aspnetzero.com/Pricing?ref=abptmpl#pricing",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetZeroFaq",
                            new FixedLocalizableString("Faq"),
                            url: "https://aspnetzero.com/Faq?ref=abptmpl",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "AspNetZeroDocuments",
                            new FixedLocalizableString("Documents"),
                            url: "https://aspnetzero.com/Documents?ref=abptmpl",
                            icon: "far fa-dot-circle",
                            target: "_blank"
                        )
                    )
                )
            );
    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, MyProjectConsts.LocalizationSourceName);
    }
}