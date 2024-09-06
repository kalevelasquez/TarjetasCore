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
    public class CreateNuevaCompraQueryHandler: IRequestHandler<CreateNuevaCompraQuery, GenericResponse>
    {
        private readonly ICreateNuevaCompraUseCase _createNuevaCompraUseCase;

        public CreateNuevaCompraQueryHandler(ICreateNuevaCompraUseCase createNuevaCompraUseCase)
        {
            _createNuevaCompraUseCase = createNuevaCompraUseCase;
        }

        public async Task<GenericResponse> Handle(CreateNuevaCompraQuery query, CancellationToken cancellationToken)
        {
            return await _createNuevaCompraUseCase.CreateNuevaCompra(query);
        }
    }
}
