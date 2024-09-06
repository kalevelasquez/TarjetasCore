using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarjetasCore.Service.Domain.Entities.Base
{
    public class GenericResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
