﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.Application.Features.Parametros
{
    public class GetParametrosQuery : IRequest<ObjectResponse<List<ParametrosResponse>>>
    {

    }
}
