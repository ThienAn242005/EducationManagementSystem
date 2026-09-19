using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MyCompany.MyProject.Academics.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Academics
{
    public class SubjectAppService : AsyncCrudAppService<Subject, SubjectDto, int, PagedAndSortedResultRequestDto, CreateUpdateSubjectDto>
    {
        public SubjectAppService(IRepository<Subject, int> repository) : base(repository)
        {
        }
    }
}
