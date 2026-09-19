using System.ComponentModel.DataAnnotations;

namespace MyCompany.MyProject.Grading
{
    public enum AcademicPerformance
    {
        [Display(Name = "Chưa có")]
        None = 0,

        [Display(Name = "Giỏi")]
        Good = 1,          // ĐTB >= 8.0

        [Display(Name = "Khá")]
        Fair = 2,          // 6.5 <= ĐTB < 8.0

        [Display(Name = "Trung bình")]
        Average = 3,       // 5.0 <= ĐTB < 6.5

        [Display(Name = "Yếu")]
        Weak = 4           // ĐTB < 5.0
    }
}
