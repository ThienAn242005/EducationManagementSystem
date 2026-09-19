using MyCompany.MyProject.Roles.Dto;
using System.Collections.Generic;

namespace MyCompany.MyProject.Web.Models.Common;

public interface IPermissionsEditViewModel
{
    List<FlatPermissionDto> Permissions { get; set; }
}