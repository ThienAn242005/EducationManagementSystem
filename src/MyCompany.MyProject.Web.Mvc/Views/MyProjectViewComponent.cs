using Abp.AspNetCore.Mvc.ViewComponents;

namespace MyCompany.MyProject.Web.Views;

public abstract class MyProjectViewComponent : AbpViewComponent
{
    protected MyProjectViewComponent()
    {
        LocalizationSourceName = MyProjectConsts.LocalizationSourceName;
    }
}
