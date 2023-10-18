using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorDomainService;
        public FornecedorController(IFornecedorService fornecedorDomainService)
        {
            _fornecedorDomainService= fornecedorDomainService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Fornecedor fornecedor)
        {
            _fornecedorDomainService.Cadastrar(fornecedor);
            return Ok();
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _fornecedorDomainService.ObterPorId(id);

            if (result != null)
                return Ok(result);
        
            return NoContent();
        }

        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _fornecedorDomainService.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Fornecedor fornecedor)
        {
            _fornecedorDomainService.Alterar(fornecedor);

            return Ok();            
        }

        [HttpGet]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            _fornecedorDomainService.Deletar(id);

            
            return Ok();

           
        }
    }
}
