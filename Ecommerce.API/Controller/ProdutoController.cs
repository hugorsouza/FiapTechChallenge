using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoDomainService _produtoDomainService;
        public ProdutoController(IProdutoDomainService produtoDomainService)
        {
            _produtoDomainService= produtoDomainService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            _produtoDomainService.Cadastrar(produto);
            return Ok();
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            var result = _produtoDomainService.ObterPorId(id);

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            var result = _produtoDomainService.ObterTodos();

            if (result != null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Produto produto)
        {
            _produtoDomainService.Alterar(produto);

            return Ok();
        }

        [HttpGet]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            _produtoDomainService.Deletar(id);

            return Ok();

        }
    }
}
