using Dapper;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Interfaces.Queries;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Infrastructure.DBContext.Interfaces;

namespace TarjetasCore.Service.Infrastructure.Queries
{
    public class GetInfoTarjetaQueries: IGetInfoTarjetaQueries
    {
        private readonly ISqlServerDBContext _dbContext;

        public GetInfoTarjetaQueries(ISqlServerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetInfoTarjetaResponse> GetInfoTarjeta(string numeroTarjeta)
        {
            var querySelect = "EXEC sp_ObtenerDatosTarjeta @numeroTarjeta";

            using var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.QueryAsync<GetInfoTarjetaResponse>(querySelect, new {numeroTarjeta});

            return result.FirstOrDefault();
        }
    }
}
