using ProjectClientServer.Models;

namespace ProjectClientServer.Repositories.Contract
{
    public interface IUniversityRepository:IGeneralRepository<University, int>
    {
        Task<University?> GetByNameAsync(string name);
        Task<bool> IsNameExistAsync(string name);
    }
}
