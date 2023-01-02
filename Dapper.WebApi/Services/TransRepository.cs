using Dapper.WebApi.Models;

using Dapper.WebApi.Services.Queries;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;

namespace Dapper.WebApi.Services
{
    public class TransRepository : BaseRepository, ITransRepository
    {
        private readonly ICommandText _commandText;

        public TransRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;

        }

        public async Task<IEnumerable<Trans>> GetAllTrans()
        {

            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<Trans>(_commandText.GetTrans);
                return query;
            });

        }

        public async ValueTask<Trans> GetById(int id)
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryFirstOrDefaultAsync<Trans>(_commandText.GetTransById, new { Id = id });
                return query;
            });

        }

        public async Task<IEnumerable<Trans>> GetByCurrencyCode(string currencyCode)
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<Trans>(_commandText.GetTransByCurrencyCode, new { CurrencyCode = currencyCode });
                return query;
            });

        }

        public async Task<IEnumerable<Trans>> GetByDateRange(string startDate, string endDate)
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryAsync<Trans>(_commandText.GetTransByDateRange, new { StartDate = startDate, EndDate = endDate });
                return query;
            });

        }

        public async Task<IEnumerable<Trans>> GetByStatus(string status)
        {
            try
            {
                return await WithConnection(async conn =>
                {
                    var query = await conn.QueryAsync<Trans>(_commandText.GetTransByStatus, new { Status = status });
                    return query;
                });
            }
            catch(Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return null;
            }
            

        }

        public async Task AddTrans(Trans entity)
        {
            try
            {
                await WithConnection(async conn =>
                {
                    await conn.ExecuteAsync(_commandText.AddTrans,
                        new { TransactionId = entity.TransactionId, Amount = entity.Amount, CurrencyCode = entity.CurrencyCode, Status = entity.Status, TransactionDate = entity.TransactionDate });
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }
        public async Task UpdateTrans(Trans entity, int id)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.UpdateTrans,
                    new { Amount = entity.Amount, CurrencyCode = entity.CurrencyCode, Status = entity.Status, Id = id });
            });

        }
        public async Task RemoveTrans(int id)
        {

            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.RemoveTrans, new { Id = id });
            });

        }

    }
}
