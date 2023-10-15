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
    }
}
