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
        if (input.IsCurrent)
        {
            var currentSemesters = await Repository.GetAllListAsync(x => x.IsCurrent);

            foreach (var semester in currentSemesters)
            {
                semester.IsCurrent = false;
                await Repository.UpdateAsync(semester);
            }
        }

        return await base.CreateAsync(input);
    }
}