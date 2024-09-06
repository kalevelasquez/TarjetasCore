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
    public class GetHistorialComprasQueries: IGetHistorialComprasQueries
    {
        private readonly ISqlServerDBContext _dbContext;

        public GetHistorialComprasQueries(ISqlServerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetHistorialComprasResponse>> GetHistorialCompras(string numeroTarjeta, int mes, int anio)
        {
            var querySelect = "EXEC sp_GetHistorialCompras @numeroTarjeta, @mes, @anio";

            var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.QueryAsync<GetHistorialComprasResponse>(querySelect, new { numeroTarjeta, mes, anio });
            return result.ToList();
        }
    }
}
