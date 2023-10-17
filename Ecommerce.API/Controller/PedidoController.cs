using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [ApiController]
    [Route("Pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IFazerPedidoService _fazerPedidoService;
        private readonly IConsultarPedidoService _consultarPedidoService;
        public PedidosController(
            IFazerPedidoService fazerPedidoService,
            IConsultarPedidoService consultarPedidoService)
        {
            _fazerPedidoService = fazerPedidoService;
            _consultarPedidoService = consultarPedidoService;
        }

        [HttpPost("FazerPedido")]
        public IActionResult FazerPedido(FazerPedidoModel fazerPedidoModel)
        {
            var retorno = _fazerPedidoService.FazerPedido(fazerPedidoModel);
            return Ok(retorno);
        }

        [HttpGet("ConsultarPedido")]
        public IActionResult ConsultarPedido(string usuario)
        {
            var retorno = _consultarPedidoService.ConsultarPedido(usuario);
            return Ok(retorno);
        }
    }
}
