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
    public class GetHistorialComprasQueryHandler: IRequestHandler<GetHistorialComprasQuery, ObjectResponse<List<GetHistorialComprasResponse>>>
    {
        private readonly IGetHistorialComprasUseCase _getHistorialComprasUseCase;

        public GetHistorialComprasQueryHandler(IGetHistorialComprasUseCase getHistorialComprasUseCase)
        {
            _getHistorialComprasUseCase = getHistorialComprasUseCase;
        }

        public async Task<ObjectResponse<List<GetHistorialComprasResponse>>> Handle(GetHistorialComprasQuery query, CancellationToken cancellationToken)
        {
            return await _getHistorialComprasUseCase.GetHistorialCompras(query);
        }
    }
}
