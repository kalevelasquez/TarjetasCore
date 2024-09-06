using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;
using TarjetasCore.Service.Application.Interfaces.Queries;
using TarjetasCore.Service.Application.UseCases.Interfaces;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Handler.Transacciones
{
    public class GetHistorialPagosQueryHandler: IRequestHandler<GetHistorialPagosQuery, ObjectResponse<List<GetHistorialPagosResponse>>>
    {
        private readonly IGetHistorialPagosUseCase _getHistorialPagosUseCase;

        public GetHistorialPagosQueryHandler(IGetHistorialPagosUseCase getHistorialPagosUseCase)
        {
            _getHistorialPagosUseCase = getHistorialPagosUseCase;
        }

        public async Task<ObjectResponse<List<GetHistorialPagosResponse>>> Handle(GetHistorialPagosQuery query, CancellationToken cancellationToken)
        {
            return await _getHistorialPagosUseCase.GetHistorialPagos(query);
        }
    }
}
