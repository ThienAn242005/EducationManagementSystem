using MyCompany.MyProject.Roles.Dto;
using System.Collections.Generic;

namespace MyCompany.MyProject.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
