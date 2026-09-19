using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Grading.Dto
{
    public abstract class GradeRecordDtoBase
    {
        [ScoreRange]
        public decimal? ScoreTX1 { get; set; }

        [ScoreRange]
        public decimal? ScoreTX2 { get; set; }

        [ScoreRange]
        public decimal? ScoreTX3 { get; set; }

        [ScoreRange]
        public decimal? ScoreGK { get; set; }

        [ScoreRange]
        public decimal? ScoreCK { get; set; }
    }
}
