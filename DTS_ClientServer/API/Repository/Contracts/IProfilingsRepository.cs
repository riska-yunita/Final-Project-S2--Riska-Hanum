using API.Models;
using API.ViewModels;

namespace API.Repository.Contracts
{
    public interface IProfilingsRepository : IGeneralRepository<Profiling, string>
    {
        Task<IEnumerable<AvgGpaVM>> GetAvgGPA(int year);
        Task<IEnumerable<TotalByMajorVM>> GetTotalByMajor();
        Task<IEnumerable<WorkPeriodVM>> GetWorkPeriod();
    }
    
}
