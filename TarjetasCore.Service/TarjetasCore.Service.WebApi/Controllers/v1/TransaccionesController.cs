using Microsoft.AspNetCore.Mvc;
using TarjetasCore.Service.Application.Features.InfoTarjeta;
using TarjetasCore.Service.Domain.Entities.Base;
using TarjetasCore.Service.Domain.Entities;
using TarjetasCore.Service.Application.Features.Transacciones;

namespace TarjetasCore.Service.WebApi.Controllers.v1
{
    public class TransaccionesController : BaseApiController
    {
        [HttpGet("GetHistorialTransacciones")]
        [ProducesResponseType(typeof(ObjectResponse<GetHistorialTransaccionesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetHistorialTransacciones([FromQuery] GetHistorialTransaccionesQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error al intentar obtener la información de las transacciones de la tarjeta " + ex.Message);
            }
        }

        [HttpGet("GetHistorialCompras")]
        [ProducesResponseType(typeof(ObjectResponse<GetHistorialComprasResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetHistorialCompras([FromQuery] GetHistorialComprasQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error al intentar obtener la información de las compras de la tarjeta " + ex.Message);
            }
        }

        [HttpGet("GetHistorialPagos")]
        [ProducesResponseType(typeof(ObjectResponse<GetHistorialPagosResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> GetHistorialPagos([FromQuery] GetHistorialPagosQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error al intentar obtener la información de los pagos de la tarjeta " + ex.Message);
            }
        }

        [HttpPost("CrearNuevaCompra")]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> CreateNuevaCompra([FromBody] CreateNuevaCompraQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error al intentar guardar la información sobre la compra realizada " + ex.Message);
            }
        }

        [HttpPost("CrearNuevoPago")]
        [ProducesResponseType(typeof(GenericResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse))]
        public async Task<IActionResult> CreateNuevoPago([FromBody] CreateNuevoPagoQuery query)
        {
            try
            {
                return Ok(await Mediator.Send(query));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error al intentar guardar la información sobre el pago realizada " + ex.Message);
            }
        }
    }
}
