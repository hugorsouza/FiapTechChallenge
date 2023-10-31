using Ecommerce.Application.Services.Interfaces.Pedido;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Infra.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Authorize(Roles = $"{PerfilUsuarioExtensions.Cliente},{PerfilUsuarioExtensions.Funcionario}")]
    [Route("api/[Controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoService _pedidoService;

        public PedidoController(
            ILogger<PedidoController> logger,
            IPedidoService pedidoService)
        {
            _logger = logger;
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Cadastrar pedido
        /// </summary>
        [HttpPost("CadastrarPedido")]
        public IActionResult CadastrarPedido(int produtoId, int quantidade)
        {
            try
            {
                var result = _pedidoService.CadastrarPedido(produtoId, quantidade);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Localizar pedido pelo ID
        /// </summary>
        [HttpGet("ObterPedidoPorId/{id}")]
        public IActionResult ObterPedidoPorId(int id)
        {
            try
            {
                var result = _pedidoService.ObterPedidoPorId(id);

                if (result != null)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Localizar lista de pedidos completa
        /// </summary>
        [HttpGet("ObterTodosPedido")]
        public IActionResult ObterTodosPedido()
        {
            try
            {
                var result = _pedidoService.ObterTodosPedido();

                if (result != null)
                    return Ok(result);


                return NoContent();
            }
            catch (Exception ex)
            {
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Alterar pedido
        /// </summary>
        [HttpPut("AlterarPedido/{IdPedido}")]
        public async Task<IActionResult> AlterarPedido(int idPedido)
        {
            try
            {
                var result = await _pedidoService.AlterarPedido(idPedido);

                if (result != null)
                    return Ok(result);


                return NoContent();
            }
            catch (Exception ex)
            {
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletar pedido
        /// Requer permissão de administrador.
        /// </summary>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [HttpDelete]
        [Route("DeletarPedido/{idPedido}")]
        public IActionResult DeletarPedido(int idPedido)
        {
            try
            {
                _pedidoService.DeletarPedido(idPedido);
                return Ok($"Pedido {idPedido} deletado com Sucesso");
            }
            catch (Exception ex)
            {
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
        }
    }
}
