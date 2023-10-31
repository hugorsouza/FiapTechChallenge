using Ecommerce.Application.Model.Produto;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
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
        [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
        [ProducesResponseType(typeof(FabricanteViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [ProducesResponseType(typeof(IEnumerable<Fabricante>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
        [ProducesResponseType(typeof(Fabricante), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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
    }
}
