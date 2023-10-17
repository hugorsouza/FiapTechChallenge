using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaDomainService _categoriaDomainService;
        public CategoriaController(ICategoriaDomainService categoriaDomainService)
        {
            _categoriaDomainService= categoriaDomainService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Categoria categoria)
        {
            _categoriaDomainService.Cadastrar(categoria);
            return Ok();
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _categoriaDomainService.ObterPorId(id);

            if (result != null)
                return Ok(result);
        
            return NoContent();
        }

        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _categoriaDomainService.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Categoria categoria)
        {
            _categoriaDomainService.Alterar(categoria);

            return Ok();            
        }

        [HttpGet]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            _categoriaDomainService.Deletar(id);

            
            return Ok();
                       
        }
    }
}
