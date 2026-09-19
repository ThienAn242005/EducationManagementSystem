using Abp.Application.Services;
using MyCompany.MyProject.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
