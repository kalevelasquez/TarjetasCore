using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;
using TarjetasCore.Service.Application.Interfaces.Queries;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;
using TarjetasCore.Service.Infrastructure.DBContext.Interfaces;

namespace TarjetasCore.Service.Infrastructure.Queries
{
    public class GetHistorialTransaccionesQueries: IGetHistorialTransaccionesQueries
    {
        private readonly ISqlServerDBContext _dbContext;

        public GetHistorialTransaccionesQueries(ISqlServerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GetHistorialTransaccionesResponse>> GetHistorialTransacciones(string numeroTarjeta, int mes, int anio)
        {
            var querySelect = "EXEC sp_GetHistorialTarjeta @numeroTarjeta, @mes, @anio";

            var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.QueryAsync<GetHistorialTransaccionesResponse>(querySelect, new {numeroTarjeta, mes, anio});
            return result.ToList();
        }
    }
}
