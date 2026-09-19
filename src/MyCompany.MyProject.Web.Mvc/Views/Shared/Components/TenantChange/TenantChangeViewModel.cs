using Abp.AutoMapper;
using MyCompany.MyProject.Sessions.Dto;

namespace MyCompany.MyProject.Web.Views.Shared.Components.TenantChange;

[AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
public class TenantChangeViewModel
{
    public TenantLoginInfoDto Tenant { get; set; }
}
