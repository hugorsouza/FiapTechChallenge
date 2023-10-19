using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaservice;
        public CategoriaController(ICategoriaService categoriaservice)
        {
            _categoriaservice= categoriaservice;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Categoria categoria)
        {
            _categoriaservice.Cadastrar(categoria);
            return Ok();
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _categoriaservice.ObterPorId(id);

            if (result != null)
                return Ok(result);
        
            return NoContent();
        }

        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _categoriaservice.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Categoria categoria)
        {
            _categoriaservice.Alterar(categoria);

            return Ok();            
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            _categoriaservice.Deletar(id);

            
            return Ok();
                       
        }
    }
}
