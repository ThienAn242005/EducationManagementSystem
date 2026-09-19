using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Grading.Dto
{
    public class SaveGradeListInput
    {
        [Required]
        public int ClassId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int SemesterId { get; set; }

        // Danh sách điểm từng học sinh
        public List<SaveGradeItemDto> GradeRecords { get; set; } = new List<SaveGradeItemDto>();
    }
}
