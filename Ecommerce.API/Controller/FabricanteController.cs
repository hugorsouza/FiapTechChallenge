using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.API.Controller
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class FabricanteController : ControllerBase
    {
        private readonly IFabricanteService _fabricanteservice;
        private readonly ILogger<CategoriaController> _logger;
        public FabricanteController(IFabricanteService fabricanteservice, ILogger<CategoriaController> logger)
        {
            _fabricanteservice = fabricanteservice;
            _logger = logger;
        }

        /// <summary>
        /// Cadastrar fabricante
        /// </summary>
        /// <param name="fabricante"></param>
        /// <returns></returns>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
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
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Obter fabricante por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
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
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Obter todos os fabricantes
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
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
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Alterar fabricante
        /// </summary>
        /// <param name="fabricante"></param>
        /// <returns></returns>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
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
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Deletar fabricante
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
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
                var erro = @$"{ex.Message} - {ex.StackTrace} - {ex.GetType}";
                _logger.LogError(erro);
                return BadRequest(ex.Message);
            }
            
        }
    }
}
