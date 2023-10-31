using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.API.Controller
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoservice;
        private readonly ILogger<CategoriaController> _logger;
        public ProdutoController(IProdutoService produtoservice, ILogger<CategoriaController> logger)
        {
            _produtoservice = produtoservice;
            _logger = logger;
        }

        /// <summary>
        /// Cadastrar produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [ProducesResponseType(typeof(ProdutoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [Route("Cadastrar")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoViewModel produto)
        {
            var result = await _produtoservice.Cadastrar(produto);
            return Ok(result);
        }

        /// <summary>
        /// Obter produto por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _produtoservice.ObterPorId(id);

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        /// <summary>
        /// Obter todos os produtos
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _produtoservice.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        /// <summary>
        /// Alterar produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Produto produto)
        {
            var result = _produtoservice.Alterar(produto);
            return Ok(result);
        }        

        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("Upload/{id}")]
        public async Task<IActionResult> Upload(IFormFile arquivo, [FromRoute] int id)
        {
            var result = await _produtoservice.Upload(arquivo, id);

            return Ok(result);
        }

        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("DeleteImagem/{id}")]
        public async Task<IActionResult> DeleteImagem([FromRoute] int id)
        {
            await _produtoservice.DeletarimagemProduto(id);

            return Ok();
        }
    }
}
