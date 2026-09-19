using MyCompany.MyProject.Roles.Dto;
using System.Collections.Generic;

namespace MyCompany.MyProject.Web.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}
