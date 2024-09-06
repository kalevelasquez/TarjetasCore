using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Interfaces.Queries;
using TarjetasCore.Service.Application.UseCases;
using TarjetasCore.Service.Application.UseCases.Interfaces;

namespace TarjetasCore.Service.Application
{
    public static class DependencyInjection
    {
        public static void AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient<IGetParametrosUseCase, GetParametrosUseCase>();
            services.AddTransient<IGetInfoTarjetaUseCase, GetInfoTarjetaUseCase>();
            services.AddTransient<IGetHistorialTransaccionesUseCase, GetHistorialTransaccionesUseCase>();
            services.AddTransient<IGetHistorialComprasUseCase,  GetHistorialComprasUseCase>();
            services.AddTransient<IGetHistorialPagosUseCase, GetHistorialPagosUseCase>();
            services.AddTransient<ICreateNuevaCompraUseCase,  CreateNuevaCompraUseCase>();
            services.AddTransient<ICreateNuevoPagoUseCase, CreateNuevoPagoUseCase>();
        }
    }
}
