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
        private readonly IFabricanteDomainService _fabricanteDomainService;
        public FabricanteController(IFabricanteDomainService fabricanteDomainService)
        {
            _fabricanteDomainService = fabricanteDomainService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Fabricante fabricante)
        {
            _fabricanteDomainService.Cadastrar(fabricante);
            return Ok();
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _fabricanteDomainService.ObterPorId(id);

            if (result != null)
                return Ok(result);
        
            return NoContent();
        }

        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _fabricanteDomainService.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Fabricante fabricante)
        {
            _fabricanteDomainService.Alterar(fabricante);

            return Ok();            
        }

        [HttpGet]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            _fabricanteDomainService.Deletar(id);

            
            return Ok();

           
        }
    }
}
