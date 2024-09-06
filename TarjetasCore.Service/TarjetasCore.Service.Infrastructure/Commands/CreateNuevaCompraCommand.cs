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
    public class CreateNuevaCompraCommand: ICreateNuevaCompraCommand
    {
        private readonly ISqlServerDBContext _dbContext;

        public CreateNuevaCompraCommand(ISqlServerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateNuevaCompra(CreateNuevaCompraQuery query)
        {
            try
            {
                var querySave = "EXEC sp_CrearCompra @numeroTarjeta, @fechaCompra, @descripcion, @monto";
                using var connection = _dbContext.GetConnectionSqlServer();
                var result = await connection.ExecuteAsync(querySave, new { query.numeroTarjeta, query.fechaCompra, query.descripcion, query.monto });
                return result > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
