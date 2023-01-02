using Dapper.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.WebApi.Services
{
    public interface ITransRepository
    {
        ValueTask<Trans> GetById(int id);
        Task AddTrans(Trans entity);
        Task UpdateTrans(Trans entity, int id);
        Task RemoveTrans(int id);
        Task<IEnumerable<Trans>> GetAllTrans();
        Task<IEnumerable<Trans>> GetByCurrencyCode(string currentCode);
        Task<IEnumerable<Trans>> GetByDateRange(string startDate, string endDate);
        Task<IEnumerable<Trans>> GetByStatus(string status);
    }
}
