using Abp.Application.Services;
using MyCompany.MyProject.MultiTenancy.Dto;

namespace MyCompany.MyProject.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

