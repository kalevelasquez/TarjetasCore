﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Domain.Entities;

namespace TarjetasCore.Service.Application.Interfaces.Queries
{
    public interface IGetInfoTarjetaQueries
    {
        Task<GetInfoTarjetaResponse> GetInfoTarjeta(string numeroTarjeta);
    }
}
