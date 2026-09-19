using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Grading.Dto
{
    public class SaveGradeItemDto : ICustomValidate
    {
        [Required]
        public long StudentId { get; set; }

        public decimal? ScoreTX1 { get; set; }
        public decimal? ScoreTX2 { get; set; }
        public decimal? ScoreTX3 { get; set; }
        public decimal? ScoreGK { get; set; }
        public decimal? ScoreCK { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            var scores = new[] { ScoreTX1, ScoreTX2, ScoreTX3, ScoreGK, ScoreCK };

            foreach (var score in scores)
            {
                if (score.HasValue && (score.Value < 0 || score.Value > 10))
                {
                    context.Results.Add(new ValidationResult("Điểm phải từ 0 đến 10", new[] { nameof(score) }));
                    break;
                }
            }
        }
    }
}
