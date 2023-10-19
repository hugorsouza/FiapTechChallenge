using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [ApiController]
    [Route("Pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IConsultarPedidoService _consultarPedidoService;
        public PedidosController(
            IConsultarPedidoService consultarPedidoService)
        {
            _consultarPedidoService = consultarPedidoService;
        }

        [HttpGet("ConsultarPedido")]
        public IActionResult ConsultarPedido(string usuario)
        {
            var retorno = _consultarPedidoService.ConsultarPedido(usuario);
            return Ok(retorno);
        }
    }
}
