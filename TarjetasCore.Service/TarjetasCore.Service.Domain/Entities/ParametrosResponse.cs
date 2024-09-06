using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarjetasCore.Service.Domain.Entities
{
    public class ParametrosResponse
    {
        public int idParametro {  get; set; }
        public string nombreParametro { get; set; }
        public string valorParametro { get; set; }
        public string tipoParametro { get; set; }
    }
}
