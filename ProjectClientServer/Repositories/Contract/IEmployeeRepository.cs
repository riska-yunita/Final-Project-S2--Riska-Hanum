using ProjectClientServer.Models;
using ProjectClientServer.ViewModel;
using System.Collections;
using System.Collections.Generic;

namespace ProjectClientServer.Repositories.Contract
{
    public interface IEmployeeRepository:IGeneralRepository<Employee, string>
    {
        Task<UserDataVM?> GetUserByEmail(string email);
        Task<IEnumerable> MasterData();
    }
}
