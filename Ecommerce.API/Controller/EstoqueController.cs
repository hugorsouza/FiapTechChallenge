using Ecommerce.Application.Services.Interfaces.Estoque;
using Ecommerce.Infra.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EstoqueController :ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEstoqueService _estoqueService;

        public EstoqueController(
            ILogger<EstoqueController> logger,
            IEstoqueService estoqueService)
        {
            _logger = logger;
            _estoqueService = estoqueService;
        }

        /// <summary>
        /// Alterar quantidade de itens no estoque
        /// </summary>
        [HttpPost("AlterarItemEstoque/{id}/{quantidade}")]
        public IActionResult AlterarItemEstoque(int id, int quantidade)
        {
            try
            {
                var result = _estoqueService.AlterarItemEstoque(id, quantidade);
                return Ok($"Alteração realizada com sucesso, quantidade atual é {result.QuantidadeAtual}");
            }
            catch (Exception ex)
            {
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Localizar itens estoque pelo ID
        /// </summary>
        [HttpGet("ObterItemEstoquePorId/{id}")]
        public IActionResult ObterItemEstoquePorId(int id)
        {
            try
            {
                var result = _estoqueService.ObterItemEstoquePorId(id);

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
        /// Localizar lista completa do estoque
        /// </summary>
        [HttpGet("ObterListaCompletaEstoque")]
        public IActionResult ObterListaCompletaEstoque()
        {
            try
            {
                var result = _estoqueService.ObterListaCompletaEstoque();

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
        /// Deletar item estoque
        /// Requer permissão de administrador.
        /// </summary>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [HttpDelete]
        [Route("DeletarItemEstoque/{id}")]
        public IActionResult DeletarItemEstoque(int id)
        {
            try
            {
                _estoqueService.DeletarItemEstoque(id);
                return Ok($"Item {id} deletado com Sucesso");
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
