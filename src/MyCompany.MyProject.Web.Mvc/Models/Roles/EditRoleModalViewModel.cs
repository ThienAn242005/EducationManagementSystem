using Abp.AutoMapper;
using MyCompany.MyProject.Roles.Dto;
using MyCompany.MyProject.Web.Models.Common;

namespace MyCompany.MyProject.Web.Models.Roles;

[AutoMapFrom(typeof(GetRoleForEditOutput))]
public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
{
    public bool HasPermission(FlatPermissionDto permission)
    {
        return GrantedPermissionNames.Contains(permission.Name);
    }
}
