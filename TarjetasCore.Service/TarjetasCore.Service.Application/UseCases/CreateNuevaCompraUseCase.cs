using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Transacciones;
using TarjetasCore.Service.Application.Interfaces.Commands;
using TarjetasCore.Service.Application.UseCases.Interfaces;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.UseCases
{
    public class CreateNuevaCompraUseCase: ICreateNuevaCompraUseCase
    {
        private readonly ICreateNuevaCompraCommand _createNuevaCompraCommand;

        public CreateNuevaCompraUseCase(ICreateNuevaCompraCommand createNuevaCompraCommand)
        {
            _createNuevaCompraCommand = createNuevaCompraCommand;
        }

        public async Task<GenericResponse> CreateNuevaCompra(CreateNuevaCompraQuery query)
        {
            GenericResponse response = new GenericResponse();

            response.Code = 0;
            response.Message = "Hubo un error al intentar guardar la información de la compra";

            var request = await _createNuevaCompraCommand.CreateNuevaCompra(query);
            if (request)
            {
                response.Code = 1;
                response.Message = "Éxito";
            }

            return response;
        }
    }
}
