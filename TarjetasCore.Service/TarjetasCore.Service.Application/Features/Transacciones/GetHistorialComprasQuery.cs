using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Features.Transacciones
{
    public class GetHistorialComprasQuery: IRequest<ObjectResponse<List<GetHistorialComprasResponse>>>
    {
        public string numeroTarjeta { get; set; }
        public int mes { get; set; }
        public int anio { get; set; }
    }
}
