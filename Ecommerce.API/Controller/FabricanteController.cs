using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Entities.Produtos;
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
        public IActionResult Cadastrar([FromBody] FabricanteViewModel fabricante)
        {
            try
            {
               var result = _fabricanteservice.Cadastrar(fabricante);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
            try
            {
                var result = _fabricanteservice.Alterar(fabricante);

                return Ok(result);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _fabricanteservice.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
