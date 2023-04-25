using ProjectClientServer.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectClientServer.ViewModel
{
    public class RegisterVM
    {
        [Display(Name = "NIK")]
        public string Nik { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number"), Phone]
        public string PhoneNumber { get; set; }

        public string Major { get; set; }

        public string Degree { get; set; }

        [Range(0, 4, ErrorMessage = "The {0} Tidak boleh kurang {1} dan lebih dari {2}")]
        [Display(Name = "GPA")]
        public decimal Gpa { get; set; }

        [Display(Name = "University Name")]
        public string UniversityName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password"), DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
