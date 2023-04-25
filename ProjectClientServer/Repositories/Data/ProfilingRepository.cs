using ProjectClientServer.Contexts;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories.Contract;
using ProjectClientServer.ViewModel;
using System.Collections;
using System.ComponentModel;

namespace ProjectClientServer.Repositories.Data
{
    public class ProfilingRepository : GeneralRepository<Profiling, string, MyContext>, IProfilingRepository
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        public ProfilingRepository(MyContext context, IEducationRepository repository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository) : base(context)
        {
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
        }

        public async Task<IEnumerable<AvgGpaVM>> GetAvgGpa(int tahun)
        {
            var getEmployees = await _employeeRepository.GetAllAsync();
            var getEducations = await _educationRepository.GetAllAsync();
            var getUniversities = await _universityRepository.GetAllAsync();
            var getProfilings = await GetAllAsync();

            var avg = _context.TbMEducations.Average(e => e.Gpa);
            IEnumerable<AvgGpaVM> data = getEmployees
                .Join(getProfilings, e => e.Nik, p => p.EmployeeNik,
                (e, p) => new { e, p })
                .Join(getEducations, ep => ep.p.EducationId, d => d.Id,
                (ep, d) => new { d, ep })
                .Join(getUniversities, epd => epd.d.UniversityId, u => u.Id,
                (epd, u) => new AvgGpaVM
                {
                    Nik = epd.ep.e.Nik,
                    FullName = epd.ep.e.FirstName + " " + epd.ep.e.LastName,
                    HiringDate = epd.ep.e.HiringDate,
                    University = u.Name,
                    Major = epd.d.Major,
                    Gpa = epd.d.Gpa,
                })
                .Where(x => x.Gpa > avg && (x.HiringDate).Year == tahun);
                //.GroupBy(x => new { x.Major, x.University });

            return data;
        }

        public async Task<IEnumerable> TotalByMajor()
        {
            var getEmployees = await _employeeRepository.GetAllAsync();
            var getEducations = await _educationRepository.GetAllAsync();
            var getUniversities = await _universityRepository.GetAllAsync();
            var getProfilings = await GetAllAsync();

            var total = getEmployees
                .Join(getProfilings, e => e.Nik, p => p.EmployeeNik,
                (e, p) => new { e, p })
                .Join(getEducations, ep => ep.p.EducationId, d => d.Id,
                (ep, d) => new { d, ep })
                .Join(getUniversities, epd => epd.d.UniversityId, u => u.Id,
                (epd, u) => new {u, epd })
                .GroupBy(x => new { x.epd.d.Major, x.u.Name })
                .Select(x => new {
                    Major = x.Key.Major,
                    UniversityName = x.Key.Name,
                    TotalEmployee = x.Count(),
                })
                //.Count(x => x.TotalEmployee)
                .OrderByDescending(x => x.TotalEmployee);

            return total;
        }

        public async Task<IEnumerable> WorkPeriod()
        {
            var getEmployees = await _employeeRepository.GetAllAsync();
            var getProfilings = await GetAllAsync();

            var workPeriod = getEmployees
                .Select(e => new
                {
                    Nik = e.Nik,
                    FullName = e.FirstName + " " + e.LastName,
                    BirthDate = e.Birthdate,
                    Gender = e.Gender.ToString(),
                    HiringDate = e.HiringDate,
                    Email = e.Email,
                    Phone = e.PhoneNumber,
                    WorkPeriod = DateTime.Today.Year - e.HiringDate.Year,
                })
                .OrderByDescending(x => x.WorkPeriod);

            return workPeriod;
        }

    }
}
