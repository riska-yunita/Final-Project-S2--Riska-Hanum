using API.Models;
using API.ViewModels;
namespace API.Repository.Contracts;

public interface IAccountRepository : IGeneralRepository<Account, string>
{
	Task RegisterAsync(RegisterVM registerVM);
    Task<bool> LoginAsync(LoginVM loginVM);

}
