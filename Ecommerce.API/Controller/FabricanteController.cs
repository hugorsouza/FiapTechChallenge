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

        /// <summary>
        /// Cadastrar fabricante
        /// </summary>
        /// <param name="fabricante"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obter fabricante por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorId/{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var result = _fabricanteservice.ObterPorId(id);

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
        /// Obter todos os fabricantes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ObterPorTodos")]
        public IActionResult Otertodos()
        {
            try
            {
                var result = _fabricanteservice.ObterTodos();

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
        /// Alterar fabricante
        /// </summary>
        /// <param name="fabricante"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletar fabricante
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
