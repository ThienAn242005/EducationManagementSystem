using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using MyCompany.MyProject.Academics.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Academics
{
    public class SubjectAppService :
        AsyncCrudAppService<
            Subject,
            SubjectDto,
            int,
            PagedAndSortedResultRequestDto,
            CreateUpdateSubjectDto,
            CreateUpdateSubjectDto>
    {
        private readonly IRepository<Class, int> _classRepository;

        public SubjectAppService(
            IRepository<Subject, int> repository,
            IRepository<Class, int> classRepository)
            : base(repository)
        {
            _classRepository = classRepository;
        }

        public async Task<List<SubjectDto>> GetSubjectsByClassAsync(int classId)
        {
            var classEntity = await _classRepository
                .FirstOrDefaultAsync(classId);

            if (classEntity == null)
            {
                throw new UserFriendlyException(
                    "Không tìm thấy lớp học!"
                );
            }

            return await Repository
                .GetAll()
                .Where(s => s.Grade == classEntity.Grade)
                .OrderBy(s => s.Name)
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Code = s.Code,
                    Name = s.Name,
                    Grade = s.Grade,
                    Factor = s.Factor,
                    NumberOfLessons = s.NumberOfLessons
                })
                .ToListAsync();
        }
    }
}