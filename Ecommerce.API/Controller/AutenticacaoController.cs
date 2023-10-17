using Ecommerce.Application.Model.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var resultado = await _autenticacaoService.Login(login);
            return Ok(resultado);
        }

        [HttpGet("estou-logado")]
        public IActionResult TesteAutorizacao()
        {
            return Ok();
        }

        [Authorize(Roles = PerfilUsuarioHelper.Cliente)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("sou-cliente")]
        public IActionResult TesteRoleCliente()
        {
            return Ok();
        }

        [Authorize(Roles = PerfilUsuarioHelper.Operador)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("sou-operador")]
        public IActionResult TesteRoleOperador()
        {
            return Ok();
        }
    }
}
