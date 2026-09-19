using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace MyCompany.MyProject.Web.Views;

public abstract class MyProjectRazorPage<TModel> : AbpRazorPage<TModel>
{
    [RazorInject]
    public IAbpSession AbpSession { get; set; }

    protected MyProjectRazorPage()
    {
        LocalizationSourceName = MyProjectConsts.LocalizationSourceName;
    }
}
