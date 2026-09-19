using Abp.Application.Services.Dto;

namespace MyCompany.MyProject.Academics.Dto
{
    public class TeachingAssignmentResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}