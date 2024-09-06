using Microsoft.AspNetCore.Mvc;
using TarjetasCore.Service.Application.Features.Parametros;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Domain.Entities.Base;

namespace TarjetasCore.Service.WebApi.Controllers.v1
{
    public class ParametrosController : BaseApiController
    {
        [HttpGet("GetParametrosConfigurables")]
        [ProducesResponseType(typeof(ObjectResponse<ParametrosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetParametros([FromQuery] GetParametrosQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error al intentar obtener los parámetros " + ex.Message);
            }
        }
    }
}
