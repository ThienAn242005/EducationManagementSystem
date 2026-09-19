using System.ComponentModel.DataAnnotations;

namespace MyCompany.MyProject.Academics
{
    public enum GradeLevel
    {
        [Display(Name = "Khối 10")]
        Grade10 = 1,

        [Display(Name = "Khối 11")]
        Grade11 = 2,

        [Display(Name = "Khối 12")]
        Grade12 = 3
    }
}