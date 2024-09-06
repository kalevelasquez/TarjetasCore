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

namespace TarjetasCore.Service.Application.UseCases
{
    public class GetHistorialTransaccionesUseCase: IGetHistorialTransaccionesUseCase
    {
        private readonly IGetHistorialTransaccionesQueries _getHistorialTransaccionesQueries;

        public GetHistorialTransaccionesUseCase(IGetHistorialTransaccionesQueries getHistorialTransaccionesQueries)
        {
            _getHistorialTransaccionesQueries = getHistorialTransaccionesQueries;
        }

        public async Task<ObjectResponse<List<GetHistorialTransaccionesResponse>>> GetHistorialTransacciones(GetHistorialTransaccionesQuery query)
        {
            ObjectResponse<List<GetHistorialTransaccionesResponse>> response = new ObjectResponse<List<GetHistorialTransaccionesResponse>>();
            response.Code = 0;
            response.Message = "Hubo un error al intentar obtener la información de las transacciones de la tarjeta de crédito";

            try
            {
                var request = await _getHistorialTransaccionesQueries.GetHistorialTransacciones(query.numeroTarjeta, query.mes, query.anio);

                if (request != null)
                {
                    response.Code = 1;
                    response.Message = "Éxito";
                    response.Item = request.ToList();
                }
                else
                {
                    response.Code = 1;
                    response.Message = "No se encontraron transacciones correspondientes al mes";
                }
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al intentar obtener la información de las transcciones del mes " + ex.Message;
            }

            return response;
        }
    }
}
