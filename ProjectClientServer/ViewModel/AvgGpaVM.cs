using ProjectClientServer.Models;

namespace ProjectClientServer.ViewModel
{
    public class AvgGpaVM
    {
        public string Nik { get; set; }
        public string FullName { get; set; }
        public DateTime HiringDate { get; set; }
        public string Major { get; set; }
        public string University { get; set; }

        public decimal Gpa { get; set; }

        //public IEnumerable<Employee> Employees { get; set; }
    }
}
