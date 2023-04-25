using ProjectClientServer.Models;
using ProjectClientServer.ViewModel;
using System.Collections;

namespace ProjectClientServer.Repositories.Contract
{
    public interface IProfilingRepository:IGeneralRepository<Profiling, string>
    {
        Task<IEnumerable<AvgGpaVM>> GetAvgGpa(int tahun);
        Task<IEnumerable> TotalByMajor();
        Task<IEnumerable> WorkPeriod();
    }
}
