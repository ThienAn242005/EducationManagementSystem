using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Authorization;
using MyCompany.MyProject.Controllers;

namespace MyCompany.MyProject.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Grades_View)]
    public class GradeController : MyProjectControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
