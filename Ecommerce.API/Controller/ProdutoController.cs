using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
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
        public IActionResult Post([FromBody] ProdutoViewModel produto)
        {
            try
            {
                _produtoservice.Cadastrar(produto);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest("Erro ao cadastrar o produto");
            }
            
            
        }

        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var result = _produtoservice.ObterPorId(id);

                if (result != null)
                    return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest("Erro ao listar o produto");
            }
            
            return NoContent();
        }

        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            try
            {
                var result = _produtoservice.ObterTodos();

                if (result != null)
                    return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest("Erro ao listar os produtos");
            }


            return NoContent();
        }

        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Produto produto)
        {
            try
            {
                _produtoservice.Alterar(produto);
            }
            catch (Exception)
            {

                return BadRequest("Erro ao Alterar o produto");
            }


            return Ok();
        }

        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _produtoservice.Deletar(id);
            }
            catch (Exception)
            {

                return BadRequest("Erro ao deletar o produto");
            }

            return Ok();

        }
    }
}
