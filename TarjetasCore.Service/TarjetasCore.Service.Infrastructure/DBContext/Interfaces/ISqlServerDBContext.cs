using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarjetasCore.Service.Infrastructure.DBContext.Interfaces
{
    public interface ISqlServerDBContext
    {
        IDbConnection GetConnectionSqlServer();
    }
}
