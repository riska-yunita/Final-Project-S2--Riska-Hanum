using ProjectClientServer.Models;
using ProjectClientServer.Repositories.Contract;
using ProjectClientServer.ViewModel;

namespace ProjectClientServer.Repositories.Contract
{
    public interface IAccountRepository: IGeneralRepository<Account, string>
    {
        Task RegisterAsync(RegisterVM registerVM);
        Task<bool> LoginAsync(LoginVM loginVM);
    }
}
