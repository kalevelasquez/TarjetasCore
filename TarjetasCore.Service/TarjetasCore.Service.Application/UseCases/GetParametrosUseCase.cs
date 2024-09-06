using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Interfaces.Queries;
using TarjetasCore.Service.Application.UseCases.Interfaces;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.UseCases
{
    public class GetParametrosUseCase: IGetParametrosUseCase
    {
        private readonly IParametrosQueries _parametrosQueries;

        public GetParametrosUseCase(IParametrosQueries parametrosQueries)
        {
            _parametrosQueries = parametrosQueries;
        }

        public async Task<ObjectResponse<List<ParametrosResponse>>> GetParametros()
        {
            ObjectResponse<List<ParametrosResponse>> response = new ObjectResponse<List<ParametrosResponse>>();
            response.Code = 0;
            response.Message = "Hubo un error al intentar obtener los parámetros";

            var request = await _parametrosQueries.GetParametros();

            if(request != null)
            {
                response.Code = 1;
                response.Message = "Éxito";
                response.Item = request.ToList();
            }

            return response;
        }
    }
}
