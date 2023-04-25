using API.Models;

namespace API.Repository.Contracts;

public interface IUniversityRepository : IGeneralRepository<University, int>
{
	Task<bool> IsNameExistAsync(string name);
    Task<University?> GetByNameAsync(string name);
}
