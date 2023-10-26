using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaservice;
        private readonly ILogger<CategoriaController> _logger;
        public CategoriaController(ICategoriaService categoriaservice, ILogger<CategoriaController> logger)
        {
            _categoriaservice= categoriaservice;
            _logger = logger;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] CategoriaViewModel categoria)
        {
            try
            {
                
                var result = _categoriaservice.Cadastrar(categoria);
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
                var result = _categoriaservice.ObterPorId(id);

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
                var result = _categoriaservice.ObterTodos();

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
        public IActionResult Alterar([FromBody] Categoria categoria)
        {
            try
            {
                var result = _categoriaservice.Alterar(categoria);

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
                _categoriaservice.Deletar(id);
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
