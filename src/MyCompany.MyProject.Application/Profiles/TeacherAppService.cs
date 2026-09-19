using System.Linq;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Profiles.Dto;

namespace MyCompany.MyProject.Profiles
{
    public class TeacherAppService : AsyncCrudAppService<Teacher, TeacherDto, long, PagedAndSortedResultRequestDto, CreateUpdateTeacherDto>
    {
        public TeacherAppService(IRepository<Teacher, long> repository) : base(repository)
        {
        }

        protected override IQueryable<Teacher> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return Repository.GetAll().Include(t => t.MainSubject);
        }
    }
}