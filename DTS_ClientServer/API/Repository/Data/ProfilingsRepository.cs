using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Contracts;
using API.ViewModels;

namespace API.Repository.Data;

public class ProfilingsRepository : GeneralRepository<Profiling, string, MyContext>, IProfilingsRepository
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;
    public ProfilingsRepository(MyContext myContext,
        IEmployeeRepository employeeRepository,
        IEducationRepository educationRepository,
        IUniversityRepository universityRepository) : base(myContext)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    public async Task<IEnumerable<AvgGpaVM>> GetAvgGPA(int year)
    {
        var getEmployees = await _employeeRepository.GetAllAsync();
        var getProfilings = await GetAllAsync();
        var getEducations = await _educationRepository.GetAllAsync();
        var getUniversities = await _universityRepository.GetAllAsync();
        var avgGPA = _context.Educations.Average(g => g.Gpa);

        IEnumerable<AvgGpaVM> employees = getEmployees
                        .Join(getProfilings,
                            e => e.Nik,
                            p => p.EmployeeNik,
                            (e, p) => new
                            { e, p })
                        .Join(_context.Educations,
                            ep => ep.p.EducationId,
                            edu => edu.Id,
                            (ep, edu) => new
                            { ep, edu })
                        .Join(_context.Universities,
                            epdu => epdu.edu.UniversityId,
                            u => u.Id,
                            (epdu, u) => new AvgGpaVM
                            {
                                NIK = epdu.ep.e.Nik,
                                FullName = epdu.ep.e.FirstName + epdu.ep.e.LastName,
                                HiringDate = epdu.ep.e.HiringDate,
                                Major = epdu.edu.Major,
                                GPA = epdu.edu.Gpa,
                                University = u.Name
                            })
                        .Where(x=>x.GPA > avgGPA && (x.HiringDate).Year == year);
        return employees;
    }
    public async Task<IEnumerable<TotalByMajorVM>> GetTotalByMajor()
    {
        var getEmployees = await _employeeRepository.GetAllAsync();
        var getProfilings = await GetAllAsync();
        var getEducations = await _educationRepository.GetAllAsync();
        var getUniversities = await _universityRepository.GetAllAsync();

        IEnumerable<TotalByMajorVM> employees = getEmployees
                        .Join(getProfilings,
                            e => e.Nik,
                            p => p.EmployeeNik,
                            (e, p) => new
                            { e, p })
                        .Join(getEducations,
                            ep => ep.p.EducationId,
                            edu => edu.Id,
                            (ep, edu) => new
                            { ep, edu })
                        .Join(getUniversities,
                            epdu => epdu.edu.UniversityId,
                            u => u.Id,
                            (epdu, u) => new
                            { epdu, u })
                        .GroupBy(x => new
                        { x.epdu.edu.Major, x.u.Name })
                        .Select(x => new TotalByMajorVM
                        {
                            Major = x.Key.Major,
                            University = x.Key.Name,
                            TotalEmployee = x.Count(),
                        })
                       .OrderByDescending(x => x.TotalEmployee)
                       .ToList();
        return employees;
    }

    public async Task<IEnumerable<WorkPeriodVM>> GetWorkPeriod()
    {
        var getEmployees = await _employeeRepository.GetAllAsync();
        var getProfilings = await GetAllAsync();
        var getEducations = await _educationRepository.GetAllAsync();

        IEnumerable<WorkPeriodVM> employees = getEmployees
                        .Join(getProfilings,
                            e => e.Nik,
                            p => p.EmployeeNik,
                            (e, p) => new
                            { e, p })
                        .Join(getEducations,
                            ep => ep.p.EducationId,
                            edu => edu.Id,
                            (ep, edu) => new WorkPeriodVM
                            {
                                NIK = ep.e.Nik,
                                FullName = ep.e.FirstName + " " + ep.e.LastName,
                                BirthDate = ep.e.Birthdate,
                                Gender = ep.e.Gender,
                                HiringDate = ep.e.HiringDate,
                                Email = ep.e.Email,
                                PhoneNumber = ep.e.PhoneNumber,
                                WorkPeriod = DateTime.Today.Year - ep.e.HiringDate.Year

                            })
                       .OrderByDescending(ep => ep.WorkPeriod);
        return employees;
    }
}