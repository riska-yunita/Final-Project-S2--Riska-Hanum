using API.Models;

namespace API.ViewModels
{
    public class WorkPeriodVM
    {
        public string NIK { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int WorkPeriod { get; set; }
    }
}
