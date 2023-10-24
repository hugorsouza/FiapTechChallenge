using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaservice;
        public CategoriaController(ICategoriaService categoriaservice)
        {
            _categoriaservice= categoriaservice;
        }

        /// <summary>
        /// Cadastrar categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] CategoriaViewModel categoria)
        {
            try
            {
                var result = _categoriaservice.Cadastrar(categoria);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }

        /// <summary>
        /// Obter categoria por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var result = _categoriaservice.ObterPorId(id);

                if (result != null)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Obter todas as categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            try
            {
                var result = _categoriaservice.ObterTodos();

                if (result != null)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message); 
            }

        }

        /// <summary>
        /// Alterar categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar([FromBody] Categoria categoria)
        {
            try
            {
                var result = _categoriaservice.Alterar(categoria);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletar categoria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Deletar/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _categoriaservice.Deletar(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
                                  
        }
    }
}
