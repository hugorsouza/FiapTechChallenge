<<<<<<< HEAD
﻿using Ecommerce.Application.DTO;
=======
﻿using Ecommerce.Application.Model;
>>>>>>> b71a3fe (Dapper)
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
        public IActionResult FazerPedido(FazerPedidoDTO fazerPedidoDTO)
        {
            var retorno = _fazerPedidoService.FazerPedido(fazerPedidoDTO);
            return Ok(retorno);
        }
    }
}
