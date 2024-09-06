using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.InfoTarjeta;
using TarjetasCore.Service.Application.Interfaces.Queries;
using TarjetasCore.Service.Application.UseCases.Interfaces;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.UseCases
{
    public class GetInfoTarjetaUseCase: IGetInfoTarjetaUseCase
    {
        private readonly IGetInfoTarjetaQueries _getInfoTarjetaQueries;

        public GetInfoTarjetaUseCase(IGetInfoTarjetaQueries getInfoTarjetaQueries)
        {
            _getInfoTarjetaQueries = getInfoTarjetaQueries;
        }

        public async Task<ObjectResponse<GetInfoTarjetaResponse>> GetInfoTarjeta(GetInfoTarjetaQuery query)
        {
            ObjectResponse<GetInfoTarjetaResponse> response = new ObjectResponse<GetInfoTarjetaResponse>();

            response.Code = 0;
            response.Message = "Hubo un error al intentar obtener información de la tarjeta de crédito " + query.numeroTarjeta;

            try
            {
                var request = await _getInfoTarjetaQueries.GetInfoTarjeta(query.numeroTarjeta);

                if (request != null)
                {
                    response.Code = 1;
                    response.Message = "Éxito";
                    response.Item = request;
                }
                else
                {
                    response.Code = 1;
                    response.Message = "No se encontraron registros relacionados a la tarjeta";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            

            return response;

        }
    }
}
