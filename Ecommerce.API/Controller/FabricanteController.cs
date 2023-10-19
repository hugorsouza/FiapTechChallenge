using Ecommerce.Application.Services;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class FabricanteController : ControllerBase
    {
        private readonly IFabricanteService _fabricanteservice;
        public FabricanteController(IFabricanteService fabricanteservice)
        {
            _fabricanteservice = fabricanteservice;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Fabricante fabricante)
        {
            _fabricanteservice.Cadastrar(fabricante);
            return Ok();
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _fabricanteservice.ObterPorId(id);

            if (result != null)
                return Ok(result);
        
            return NoContent();
        }

        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _fabricanteservice.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Fabricante fabricante)
        {
            _fabricanteservice.Alterar(fabricante);

            return Ok();            
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            _fabricanteservice.Deletar(id);

            
            return Ok();

           
        }
    }
}
