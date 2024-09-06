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
    public class GetHistorialPagosUseCase : IGetHistorialPagosUseCase
    {
        private readonly IGetHistorialPagosQueries _getHistorialPagosQueries;

        public GetHistorialPagosUseCase(IGetHistorialPagosQueries getHistorialPagosQueries)
        {
            _getHistorialPagosQueries = getHistorialPagosQueries;
        }

        public async Task<ObjectResponse<List<GetHistorialPagosResponse>>> GetHistorialPagos(GetHistorialPagosQuery query)
        {
            ObjectResponse<List<GetHistorialPagosResponse>> response = new ObjectResponse<List<GetHistorialPagosResponse>>();
            response.Code = 0;
            response.Message = "Hubo un error al intentar obtener la información de los pagos de la tarjeta de crédito";

            var request = await _getHistorialPagosQueries.GetHistorialPagos(query.numeroTarjeta, query.mes, query.anio);

            try
            {
                if (request != null)
                {
                    response.Code = 1;
                    response.Message = "Éxito";
                    response.Item = request.ToList();
                }
                else
                {
                    response.Code = 1;
                    response.Message = "No existen pagos relacionos con el número de tarjeta de crédito y el mes ingresado";
                }
            }
            catch (Exception ex)
            {

                response.Message = "Hubo un error al intentar obtener la información de la tarjeta de crédito " + ex.Message;
            }

            return response;
        }
    }
}
