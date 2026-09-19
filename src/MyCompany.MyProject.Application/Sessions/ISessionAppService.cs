using Abp.Application.Services;
using MyCompany.MyProject.Sessions.Dto;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
