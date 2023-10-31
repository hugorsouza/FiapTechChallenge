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
            var result = _estoqueService.AlterarItemEstoque(id, quantidade);
            return Ok($"Alteração realizada com sucesso, quantidade atual é {result.QuantidadeAtual}");
        }

        /// <summary>
        /// Localizar itens estoque pelo ID
        /// </summary>
        [HttpGet("ObterItemEstoquePorId/{id}")]
        public async Task<IActionResult> ObterItemEstoquePorId(int id)
        {
            var result = await _estoqueService.ObterItemEstoquePorId(id);

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        /// <summary>
        /// Localizar lista completa do estoque
        /// </summary>
        [HttpGet("ObterListaCompletaEstoque")]
        public async Task<IActionResult> ObterListaCompletaEstoque()
        {
            var result = await _estoqueService.ObterListaCompletaEstoque();
            if (result != null)
                return Ok(result);

            return NoContent();
        }
    }
}
