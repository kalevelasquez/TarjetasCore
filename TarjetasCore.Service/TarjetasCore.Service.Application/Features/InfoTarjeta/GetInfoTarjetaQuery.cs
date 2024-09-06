using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Features.InfoTarjeta
{
    public class GetInfoTarjetaQuery: IRequest<ObjectResponse<GetInfoTarjetaResponse>>
    {
        public string numeroTarjeta { get; set; }
    }
}
