using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Contracts;

namespace API.Repository.Data;

public class AccountsRolesRepository : GeneralRepository<AccountRole, int, MyContext>, IAccountRolesRepository
{
    private IRoleRepository _roleRepository;
    public AccountsRolesRepository(
    	MyContext myContext,
    	IRoleRepository roleRepository) : base(myContext)
    {
    	_roleRepository = roleRepository;
    }

    public async Task<IEnumerable<string>> GetRolesByNikAsync(string nik)
    {
    	var getAcccountRoleByAccountNik = GetAllAsync().Result.Where(x=>x.AccountNik == nik);
    	var getRole = await _roleRepository.GetAllAsync();

    	var getRoleByNik = from ar in getAcccountRoleByAccountNik
    						join r in getRole on ar.RoleId equals r.Id
    						select r.Name;
    	return getRoleByNik;
    }
}