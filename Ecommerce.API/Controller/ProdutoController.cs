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

        /// <summary>
        /// Cadastrar produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] ProdutoViewModel produto)
        {
            try
            {   
                var result = _produtoservice.Cadastrar(produto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }

        /// <summary>
        /// Obter produto por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
                var result = _produtoservice.ObterPorId(id);

                if (result != null)
                    return Ok(result);

            
            return NoContent();
        }

        /// <summary>
        /// Obter todos os produtos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
  
                var result = _produtoservice.ObterTodos();

                if (result != null)
                    return Ok(result);
                            
            return NoContent();
        }

        /// <summary>
        /// Alterar produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Produto produto)
        {
            try
            {
                var result = _produtoservice.Alterar(produto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                       
        }

        /// <summary>
        /// Deletar produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _produtoservice.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                      

        }
    }
}
