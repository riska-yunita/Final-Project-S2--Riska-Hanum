using ProjectClientServer.Contexts;
using ProjectClientServer.Models;
using ProjectClientServer.Repositories.Contract;

namespace ProjectClientServer.Repositories.Data
{
    public class EducationRepository : GeneralRepository<Education, int, MyContext>, IEducationRepository
    {
        
        public EducationRepository(MyContext context) : base(context)
        {
        }
    }
}
