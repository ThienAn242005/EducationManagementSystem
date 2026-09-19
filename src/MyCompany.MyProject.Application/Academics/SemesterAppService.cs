using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using MyCompany.MyProject.Academics;
using MyCompany.MyProject.Academics.Dto;
using System;
using System.Threading.Tasks;

public class SemesterAppService : AsyncCrudAppService<
    Semester,
    SemesterDto,
    int,
    PagedAndSortedResultRequestDto,
    CreateUpdateSemesterDto>
{
    public SemesterAppService(IRepository<Semester, int> repository)
        : base(repository)
    {
    }

    public override async Task<SemesterDto> CreateAsync(CreateUpdateSemesterDto input)
    {
        try
        {
            return await base.CreateAsync(input);
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            var inner = ex.InnerException;

            while (inner != null)
            {
                message += " | INNER: " + inner.Message;
                inner = inner.InnerException;
            }

            throw new UserFriendlyException(
                "LỖI CREATE SEMESTER: " + message
            );
        }
    }
}