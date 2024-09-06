using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Features.Transacciones
{
    public class CreateNuevoPagoQuery: IRequest<GenericResponse>
    {
        public string numeroTarjeta { get; set; }
        public DateTime fechaPago { get; set; }
        public string descripcion { get; set; }
        public decimal montoPago { get; set; }
    }
}
