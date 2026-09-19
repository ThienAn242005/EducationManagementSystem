using Abp.AspNetCore.Mvc.Authorization;
using MyCompany.MyProject.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MyCompany.MyProject.Web.Controllers;

[AbpMvcAuthorize]
public class HomeController : MyProjectControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
