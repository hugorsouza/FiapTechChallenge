using Ecommerce.Application.Model;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [ApiController]
    [Route("Pedidos")]
    public class FazerPedidosController : ControllerBase
    {
        private readonly IFazerPedidoService _fazerPedidoService;
        public FazerPedidosController(IFazerPedidoService fazerPedidoService)
        {
            _fazerPedidoService = fazerPedidoService;
        }

        [HttpPost("FazerPedido")]
        public IActionResult FazerPedido(FazerPedidoModel fazerPedidoModel)
        {
            //
            var retorno = _fazerPedidoService.FazerPedido(fazerPedidoModel);
            return Ok(retorno);
        }
    }
}
