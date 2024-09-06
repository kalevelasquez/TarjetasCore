using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Infrastructure.DBContext.Interfaces;
using Dapper;
using TarjetasCore.Service.Application.Interfaces.Queries;

namespace TarjetasCore.Service.Infrastructure.Queries
{
    public class ParametrosQueries: IParametrosQueries
    {
        private readonly ISqlServerDBContext _dbContext;

        public ParametrosQueries(ISqlServerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<ParametrosResponse>> GetParametros()
        {
            var querySelect = "SELECT * FROM PARAMETROS";

            using var connection = _dbContext.GetConnectionSqlServer();
            var result = await connection.QueryAsync<ParametrosResponse>(querySelect);
            return result.ToList();
        }
    }
}
