using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorservice;
        public FornecedorController(IFornecedorService fornecedorservice)
        {
            _fornecedorservice= fornecedorservice;
        }

        /// <summary>
        /// Cadastro de fornecedor
        /// </summary>
        /// <param name="fornecedor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Fornecedor fornecedor)
        {
            _fornecedorservice.Cadastrar(fornecedor);
            return Ok();
        }

        /// <summary>
        /// Obter fornecedor por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _fornecedorservice.ObterPorId(id);

            if (result != null)
                return Ok(result);
        
            return NoContent();
        }

        /// <summary>
        /// Obter todos os fornecedores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _fornecedorservice.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        /// <summary>
        /// Alterar fornecedores
        /// </summary>
        /// <param name="fornecedor"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Fornecedor fornecedor)
        {
            _fornecedorservice.Alterar(fornecedor);

            return Ok();            
        }
        
        /// <summary>
        /// Deletar fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            _fornecedorservice.Deletar(id);

            
            return Ok();

           
        }
    }
}
