using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using MyCompany.MyProject.Profiles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Profiles
{
    public class StudentAppService : AsyncCrudAppService<Student, StudentDto, long, PagedAndSortedResultRequestDto, CreateUpdateStudentDto>
    {
        public StudentAppService(IRepository<Student, long> repository) : base(repository)
        {
        }
    }
}
