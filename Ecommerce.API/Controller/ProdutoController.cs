using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Controller
{
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
    }
}
