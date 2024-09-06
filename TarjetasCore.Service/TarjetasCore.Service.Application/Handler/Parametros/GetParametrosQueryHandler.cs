using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Application.Features.Parametros;
using TarjetasCore.Service.Application.UseCases.Interfaces;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Handler.Parametros
{
    public class GetParametrosQueryHandler: IRequestHandler<GetParametrosQuery, ObjectResponse<List<ParametrosResponse>>>
    {
        private readonly IGetParametrosUseCase _getParametrosUseCase;

        public GetParametrosQueryHandler(IGetParametrosUseCase getParametrosUseCase)
        {
            _getParametrosUseCase = getParametrosUseCase;
        }

        public async Task<ObjectResponse<List<ParametrosResponse>>> Handle(GetParametrosQuery query, CancellationToken cancellationToken)
        {
            return await _getParametrosUseCase.GetParametros();
        }
    }
}
