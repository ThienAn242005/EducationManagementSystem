using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Grading.Dto
{
    public class GradeStudentRowDto
    {
        public long StudentId { get; set; }
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public long? GradeRecordId { get; set; }

        public decimal? ScoreTX1 { get; set; }
        public decimal? ScoreTX2 { get; set; }
        public decimal? ScoreTX3 { get; set; }
        public decimal? ScoreGK { get; set; }
        public decimal? ScoreCK { get; set; }
        public decimal? AverageScore { get; set; }
        public AcademicPerformance Performance { get; set; }
        public string PerformanceDisplay { get; set; }
    }
}
