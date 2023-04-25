using Microsoft.EntityFrameworkCore;
using ProjectClientServer.Contexts;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories.Contract;
using ProjectClientServer.ViewModel;
using System.Collections;

namespace ProjectClientServer.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
    {
        
        public EmployeeRepository(MyContext context) : base(context)
        {
            
        }

        public async Task<UserDataVM?> GetUserByEmail(string email)
        {
            var employee = await _context.Set<Employee>().FirstOrDefaultAsync(x => x.Email == email);
            return new UserDataVM
            {
                Nik = employee!.Nik,
                Email = employee.Email,
                FullName = string.Concat(employee.FirstName, " ", employee.LastName)
            };
        }

        public async Task<IEnumerable> MasterData()
        {
            var master = _context.TbMEmployees
                .Join(_context.TbTrProfilings, e => e.Nik, p => p.EmployeeNik,
                (e, p) => new { p, e })
                .Join(_context.TbMEducations, ep => ep.p.EducationId, d => d.Id,
                (ep, d) => new { ep, d })
                .Join(_context.TbMUniversities, epd => epd.d.UniversityId, u => u.Id,
                (epd, u) => new
                {
                    Nik = epd.ep.e.Nik,
                    FullName = (epd.ep.e.FirstName + " " + epd.ep.e.LastName),
                    Gender = epd.ep.e.Gender.ToString(),
                    Email = epd.ep.e.Email,
                    Major = epd.d.Major,
                    Degree = epd.d.Degree,
                    Gpa = epd.d.Gpa,
                    UniversityName = u.Name
                });

            return master;
        }
    }
}
