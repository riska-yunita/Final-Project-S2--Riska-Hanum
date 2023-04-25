using MessagePack;
using ProjectClientServer.Contexts;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories.Contract;

namespace ProjectClientServer.Repositories.Data
{
    public class AccountRoleRepository : GeneralRepository<AccountRole, int, MyContext>, IAccountRoleRepository
    {
        private readonly IRoleRepository _roleRepository;
        public AccountRoleRepository(MyContext context, IRoleRepository roleRepository) : base(context)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<string>> GetRolesByNikAsync(string nik)
        {
            var getAccountRole = GetAllAsync().Result.Where(x => x.AccountNik == nik);
            var getRole = await _roleRepository.GetAllAsync();

            var getRoleByNik = from ar in getAccountRole
                               join r in getRole on ar.RoleId equals r.Id
                               select r.Name;
            return getRoleByNik;
        }
    }
}
