using API.Models;

namespace API.Repository.Contracts;

public interface IAccountRolesRepository : IGeneralRepository<AccountRole, int>
{
   Task<IEnumerable<string>> GetRolesByNikAsync(string nik);
}
