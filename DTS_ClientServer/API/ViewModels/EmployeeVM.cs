
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels;

public class EmployeeVM
{
    [Display(Name = "NIK")]
    public string EmployeeNIK { get; set; }
    [Display(Name = "Full Name")]
    public string EmployeeName { get; set; }
    [Display(Name = "Job Tenure")]
    public int JobTenure { get; set; }
    public string Degrees { get; set; }
}
