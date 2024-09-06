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
    public class CreateNuevoPagoUseCase: ICreateNuevoPagoUseCase
    {
        private readonly ICreateNuevoPagoCommand _createNuevoPagoCommand;

        public CreateNuevoPagoUseCase(ICreateNuevoPagoCommand createNuevoPagoCommand)
        {
            _createNuevoPagoCommand = createNuevoPagoCommand;
        }

        public async Task<GenericResponse> CreateNuevoPago(CreateNuevoPagoQuery query)
        {
            GenericResponse response = new GenericResponse();
            response.Code = 0;
            response.Message = "Hubo un error al intentar guardar la información del pago realizado";

            var request = await _createNuevoPagoCommand.CreateNuevoPago(query);
            if (request)
            {
                response.Code = 1;
                response.Message = "Éxito";
            }

            return response;
        }
    }
}
