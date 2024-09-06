using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;
using TarjetasCore.Service.Application.Interfaces.Commands;
using TarjetasCore.Service.Infrastructure.DBContext.Interfaces;

namespace TarjetasCore.Service.Infrastructure.Commands
{
    public class CreateNuevoPagoCommand: ICreateNuevoPagoCommand
    {
        private readonly ISqlServerDBContext _dbContext;

        public CreateNuevoPagoCommand(ISqlServerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateNuevoPago(CreateNuevoPagoQuery query)
        {
            var querySave = "EXEC sp_CrearPago @numeroTarjeta, @fechaPago, @descripcion, @monto";
            using var connection = _dbContext.GetConnectionSqlServer();
            var parameters = new
            {
                numeroTarjeta = query.numeroTarjeta,
                fechaPago = query.fechaPago,
                descripcion = query.descripcion,
                monto = query.montoPago
            };
            var result = await connection.ExecuteAsync(querySave, parameters);
            return result > 0;

        }
    }
}
