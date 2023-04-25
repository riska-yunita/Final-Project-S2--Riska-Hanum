using ProjectClientServer.Contexts;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories.Contract;

namespace ProjectClientServer.Repositories.Data
{
    public class RoleRepository : GeneralRepository<Role, int, MyContext>, IRoleRepository
    {
        
        public RoleRepository(MyContext context) : base(context)
        {
        }
    }
}
