using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;
using TarjetasCore.Service.Domain.Entities.Base;
using TarjetasCore.Service.Domain.Entities;

namespace TarjetasCore.Service.Application.UseCases.Interfaces
{
    public interface IGetHistorialPagosUseCase
    {
        Task<ObjectResponse<List<GetHistorialPagosResponse>>> GetHistorialPagos(GetHistorialPagosQuery query);
    }
}
