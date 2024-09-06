using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarjetasCore.Service.Domain.Entities
{
    public class GetHistorialTransaccionesResponse
    {
        public int idTransaccion { get; set; }
        public string numeroTarjeta { get; set; }
        public DateTime fechaTransaccion { get; set; }
        public int mes {  get; set; }
        public int anio { get; set; }
        public string descripcion { get; set; }
        public decimal monto { get; set; }
        public string tipoTransact {  get; set; }
    }
}
