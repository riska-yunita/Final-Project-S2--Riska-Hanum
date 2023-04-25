using API.Models;
using API.ViewModels;
using System.Collections;

namespace API.Repository.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee, string>
{
    Task<IEnumerable<EmployeeMasterVm>> GetEmployeeMaster();

    Task<UserDataVM> GetUserDataByEmailAsync(string email);
}
