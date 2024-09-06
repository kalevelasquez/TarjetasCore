using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;

namespace TarjetasCore.Service.Application.Interfaces.Commands
{
    public interface ICreateNuevoPagoCommand
    {
        Task<bool> CreateNuevoPago(CreateNuevoPagoQuery query);
    }
}
