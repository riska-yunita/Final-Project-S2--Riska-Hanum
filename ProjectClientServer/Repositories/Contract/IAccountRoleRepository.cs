using ProjectClientServer.Models;

namespace ProjectClientServer.Repositories.Contract
{
    public interface IAccountRoleRepository:IGeneralRepository<AccountRole, int>
    {
        Task<IEnumerable<string>> GetRolesByNikAsync(string nik);
        /*IEnumerable<AccountRole> GetAll();
        AccountRole? GetById(int id);
        IEnumerable<AccountRole> Search(string nik);
        int Insert(AccountRole accountRole);
        int Update(AccountRole accountRole);
        int Delete(int id);*/
    }
}
