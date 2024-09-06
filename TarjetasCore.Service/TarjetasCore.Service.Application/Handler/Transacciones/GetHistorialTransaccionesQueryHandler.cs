using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;
using TarjetasCore.Service.Application.UseCases.Interfaces;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Handler.Transacciones
{
    public class GetHistorialTransaccionesQueryHandler: IRequestHandler<GetHistorialTransaccionesQuery, ObjectResponse<List<GetHistorialTransaccionesResponse>>>
    {
        private readonly IGetHistorialTransaccionesUseCase _getHistorialTransaccionesUseCase;

        public GetHistorialTransaccionesQueryHandler(IGetHistorialTransaccionesUseCase getHistorialTransaccionesUseCase)
        {
            _getHistorialTransaccionesUseCase = getHistorialTransaccionesUseCase;
        }

        public async Task<ObjectResponse<List<GetHistorialTransaccionesResponse>>> Handle(GetHistorialTransaccionesQuery query, CancellationToken cancellationToken)
        {
            return await _getHistorialTransaccionesUseCase.GetHistorialTransacciones(query);
        }
    }
}
