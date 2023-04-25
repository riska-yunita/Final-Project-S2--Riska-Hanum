using API.Models;
using API.Repository.Contracts;
using API.Repository;
using API.ViewModels;
using API.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data;

public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
{
    public EmployeeRepository(MyContext myContext) : base(myContext) 
    {
       
    }

    public async Task<IEnumerable<EmployeeMasterVm>> GetEmployeeMaster()
    {
        IEnumerable<EmployeeMasterVm> employees = _context.Employees
                        .Join(_context.Profilings,
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
                            (epdu, u) => new EmployeeMasterVm
                            {
                                NIK = epdu.ep.e.Nik,
                                FullName = epdu.ep.e.FirstName + epdu.ep.e.LastName,
                                Major = epdu.edu.Major,
                                Degree = epdu.edu.Degree,
                                GPA = epdu.edu.Gpa,
                                University = u.Name
                            }
                        );
        return employees;
    }

    public async Task<UserDataVM> GetUserDataByEmailAsync(string email)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        return new UserDataVM
        {
            Nik = employee!.Nik,
            Email = employee.Email,
            FullName = string.Concat(employee.FirstName, " ", employee.LastName)
        };
    }

}