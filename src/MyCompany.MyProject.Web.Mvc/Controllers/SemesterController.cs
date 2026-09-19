using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCompany.MyProject.Controllers;

namespace MyCompany.MyProject.Web.Controllers
{
    [AbpMvcAuthorize]
    public class SemesterController : MyProjectControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
