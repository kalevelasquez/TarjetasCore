using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;
using TarjetasCore.Service.Application.UseCases.Interfaces;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Handler.Transacciones
{
    public class CreateNuevoPagoQueryHandler: IRequestHandler<CreateNuevoPagoQuery, GenericResponse>
    {
        private readonly ICreateNuevoPagoUseCase _createNuevoPagoUseCase;

        public CreateNuevoPagoQueryHandler(ICreateNuevoPagoUseCase createNuevoPagoUseCase)
        {
            _createNuevoPagoUseCase = createNuevoPagoUseCase;
        }

        public async Task<GenericResponse> Handle(CreateNuevoPagoQuery query, CancellationToken cancellationToken)
        {
            return await _createNuevoPagoUseCase.CreateNuevoPago(query);
        }
    }
}
