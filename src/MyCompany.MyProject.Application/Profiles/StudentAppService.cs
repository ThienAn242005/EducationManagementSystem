using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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
        protected override StudentDto MapToEntityDto(Student entity)
        {
            return new StudentDto
            {
                Id = entity.Id,
                StudentCode = entity.StudentCode,
                FullName = entity.FullName,
                DateOfBirth = entity.DateOfBirth,
                Gender = entity.Gender,
                Address = entity.Address,
                ClassId = entity.ClassId,
                ClassName = entity.Class != null ? entity.Class.ClassName : "-"
            };
        }
        protected override IQueryable<Student> CreateFilteredQuery(
    PagedAndSortedResultRequestDto input)
        {
            return Repository.GetAll()
                .Include(x => x.Class);
        }
    }
}
