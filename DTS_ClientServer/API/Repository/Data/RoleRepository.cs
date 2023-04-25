using API.Models;
using API.Repository.Contracts;
using API.Context;

namespace API.Repository.Data;

public class RoleRepository : GeneralRepository<Role, int, MyContext>, IRoleRepository
{
    public RoleRepository(MyContext context) : base(context) { }
}
