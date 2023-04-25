using API.Models;
using API.Repository.Contracts;
using API.Context;

namespace API.Repository;

public class EducationRepository : GeneralRepository<Education, int, MyContext>, IEducationRepository
{
    public EducationRepository(MyContext context) : base(context) { }
}