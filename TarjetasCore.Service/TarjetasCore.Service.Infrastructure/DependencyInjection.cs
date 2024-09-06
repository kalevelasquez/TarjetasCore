using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Interfaces.Commands;
using TarjetasCore.Service.Application.Interfaces.Queries;
using TarjetasCore.Service.Infrastructure.Commands;
using TarjetasCore.Service.Infrastructure.DBContext;
using TarjetasCore.Service.Infrastructure.DBContext.Interfaces;
using TarjetasCore.Service.Infrastructure.Queries;

namespace TarjetasCore.Service.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISqlServerDBContext, SqlServerDBContext>();
            services.AddTransient<IParametrosQueries, ParametrosQueries>();
            services.AddTransient<IGetInfoTarjetaQueries, GetInfoTarjetaQueries>();
            services.AddTransient<IGetHistorialTransaccionesQueries, GetHistorialTransaccionesQueries>();
            services.AddTransient<IGetHistorialComprasQueries, GetHistorialComprasQueries>();
            services.AddTransient<IGetHistorialPagosQueries, GetHistorialPagosQueries>();
            services.AddTransient<ICreateNuevaCompraCommand, CreateNuevaCompraCommand>();
            services.AddTransient<ICreateNuevoPagoCommand, CreateNuevoPagoCommand>();
        }
    }
}
