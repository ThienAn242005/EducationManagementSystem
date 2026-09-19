using MyCompany.MyProject.Configuration.Dto;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
