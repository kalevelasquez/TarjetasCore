using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TarjetasCore.Service.Domain.Entities
{
    public class GetInfoTarjetaResponse
    {
        public string numeroTarjeta {  get; set; }
        public string nombreAsociado { get; set; }
        public string fechaVencimiento { get; set; }
        public int cvv {  get; set; }
        public decimal limite {  get; set; }
        public decimal saldoActual { get; set; }
        public decimal saldoDisponible { get; set; }
        public DateTime fechaCorte { get; set; }
        public decimal interesConfigurableMinimo { get; set; }
        public decimal interesConfigurable {  get; set; }
        public decimal interesBonificable { get; set; }
        public decimal cuotaMinimaPagar { get; set; }
        public decimal montoTotalContado { get; set; }
    }
}
