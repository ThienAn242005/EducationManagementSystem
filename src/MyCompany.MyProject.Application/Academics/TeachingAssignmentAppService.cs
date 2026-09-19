using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Academics.Dto;
using MyCompany.MyProject.Grading;
using System.Linq;

namespace MyCompany.MyProject.Academics
{
    public class TeachingAssignmentAppService : AsyncCrudAppService<TeachingAssignment, TeachingAssignmentDto, long, PagedAndSortedResultRequestDto, CreateAssignmentDto>
    {
        public TeachingAssignmentAppService(IRepository<TeachingAssignment, long> repository) : base(repository)
        {
        }
        protected override IQueryable<TeachingAssignment> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return Repository.GetAll()
                .Include(x => x.Teacher)
                .Include(x => x.Class)
                .Include(x => x.Subject)
                .Include(x => x.Semester);
        }
    }
}
