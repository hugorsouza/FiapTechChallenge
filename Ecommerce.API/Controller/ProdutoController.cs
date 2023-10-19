using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoservice;
        public ProdutoController(IProdutoService produtoservice)
        {
            _produtoservice= produtoservice;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            _produtoservice.Cadastrar(produto);
            return Ok();
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _produtoservice.ObterPorId(id);

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _produtoservice.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Produto produto)
        {
            _produtoservice.Alterar(produto);

            return Ok();
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            _produtoservice.Deletar(id);

            return Ok();

        }
    }
}
