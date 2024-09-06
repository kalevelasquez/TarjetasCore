using Dapper;
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
    public class GetHistorialPagosQueries: IGetHistorialPagosQueries
    {
        private readonly ISqlServerDBContext _dbContext;

        public GetHistorialPagosQueries(ISqlServerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetHistorialPagosResponse>> GetHistorialPagos(string numeroTarjeta, int mes, int anio)
        {
            var querySelect = "EXEC sp_GetHistorialPagos @numeroTarjeta, @mes, @anio";

            var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.QueryAsync<GetHistorialPagosResponse>(querySelect, new { numeroTarjeta, mes, anio });
            return result.ToList();
        }
    }
}
