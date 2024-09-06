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
    public class GetHistorialComprasUseCase: IGetHistorialComprasUseCase
    {
        private readonly IGetHistorialComprasQueries _getHistorialComprasQueries;

        public GetHistorialComprasUseCase(IGetHistorialComprasQueries getHistorialComprasQueries)
        {
            _getHistorialComprasQueries = getHistorialComprasQueries;
        }

        public async Task<ObjectResponse<List<GetHistorialComprasResponse>>> GetHistorialCompras(GetHistorialComprasQuery query)
        {
            ObjectResponse<List<GetHistorialComprasResponse>> response = new ObjectResponse<List<GetHistorialComprasResponse>>();
            response.Code = 0;
            response.Message = "Hubo un error al intentar obtener la información de las compras de la tarjeta de crédito";

            var request = await _getHistorialComprasQueries.GetHistorialCompras(query.numeroTarjeta, query.mes, query.anio);

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
                    response.Message = "No existen compras relacionas con el número de tarjeta de crédito y el mes ingresado";
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
