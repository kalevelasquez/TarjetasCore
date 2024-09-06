using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TarjetasCore.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseApiController: ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
