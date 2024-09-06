using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Infrastructure.DBContext.Interfaces;

namespace TarjetasCore.Service.Infrastructure.DBContext
{
    public class SqlServerDBContext: ISqlServerDBContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _dbConnection;
        public SqlServerDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnectionSqlServer()
        {

            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            _dbConnection = new SqlConnection(connectionString);

            return _dbConnection;
        }
    }
}
