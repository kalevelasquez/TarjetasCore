using MediatR;
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

namespace TarjetasCore.Service.Application.Handler.InfoTarjeta
{
    public class GetInfoTarjetaQueryHandler: IRequestHandler<GetInfoTarjetaQuery, ObjectResponse<GetInfoTarjetaResponse>>
    {
        private readonly IGetInfoTarjetaUseCase _getInfoTarjetaUseCase;

        public GetInfoTarjetaQueryHandler(IGetInfoTarjetaUseCase getInfoTarjetaUseCase)
        {
            _getInfoTarjetaUseCase = getInfoTarjetaUseCase;
        }

        public async Task<ObjectResponse<GetInfoTarjetaResponse>> Handle(GetInfoTarjetaQuery query, CancellationToken cancellationToken)
        {
            return await _getInfoTarjetaUseCase.GetInfoTarjeta(query);
        }
    }
}
