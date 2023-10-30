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
        [Route("Cadastrar")]
        [HttpPost]
        public IActionResult Post([FromBody] ProdutoViewModel produto)
        {
            try
            {
                var result = _produtoservice.Cadastrar(produto);
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
        /// Obter produto por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var result = _produtoservice.ObterPorId(id);

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
        /// Obter todos os produtos
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            try
            {
                var result = _produtoservice.ObterTodos();

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
        /// Alterar produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Produto produto)
        {
            try
            {
                var result = _produtoservice.Alterar(produto);
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
        /// Deletar produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _produtoservice.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [HttpPost]
        [Route("Upload/{id}")]
        public async Task<IActionResult> Upload(IFormFile arquivo, [FromRoute] int id)
        {
            try
            {
                var result = await _produtoservice.Upload(arquivo, id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [HttpPost]
        [Route("DeleteImagem/{id}")]
        public async Task<IActionResult> DeleteImagem([FromRoute] int id)
        {
            try
            {
                await _produtoservice.DeletarimagemProduto(id);

                return Ok();
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
