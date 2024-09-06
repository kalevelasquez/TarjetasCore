using Microsoft.AspNetCore.Mvc;
using TarjetasCore.Service.Application.Features.Parametros;
using TarjetasCore.Service.Domain.Entities.Base;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Application.Features.InfoTarjeta;

namespace TarjetasCore.Service.WebApi.Controllers.v1
{
    public class TarjetaController : BaseApiController
    {
        [HttpGet("GetInfoTarjeta")]
        [ProducesResponseType(typeof(ObjectResponse<GetInfoTarjetaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetInfoTarjeta([FromQuery] GetInfoTarjetaQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error al intentar obtener la información de la tarjeta " + ex.Message);
            }
        }

    }
}
