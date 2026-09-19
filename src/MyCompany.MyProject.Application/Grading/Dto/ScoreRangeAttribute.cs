using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Grading.Dto
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ScoreRangeAttribute : RangeAttribute
    {
        public ScoreRangeAttribute() : base(typeof(double), "0", "10")
        {
            ErrorMessage = "Điểm phải nằm trong thang điểm từ 0 đến 10.";
        }
    }
}
