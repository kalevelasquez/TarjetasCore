using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Features.Transacciones
{
    public class CreateNuevaCompraQuery: IRequest<GenericResponse>
    {
        public string numeroTarjeta {  get; set; }
        public DateTime fechaCompra { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
    }
}
