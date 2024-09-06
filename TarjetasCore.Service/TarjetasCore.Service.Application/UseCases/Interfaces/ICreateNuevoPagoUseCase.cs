using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.UseCases.Interfaces
{
    public interface ICreateNuevoPagoUseCase
    {
        Task<GenericResponse> CreateNuevoPago(CreateNuevoPagoQuery query);
    }
}
