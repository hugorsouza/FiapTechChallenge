using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Infra.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService;
        private readonly IUsuarioManager _usuarioManager;

        public AutenticacaoController(IAutenticacaoService autenticacaoService, IUsuarioManager usuarioManager)
        {
            _autenticacaoService = autenticacaoService;
            _usuarioManager = usuarioManager;
        }

        /// <summary>
        /// Login de usuários
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginWithRefreshResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var resultado = await _autenticacaoService.Login(login);
            return Ok(resultado);
        }

        /// <summary>
        /// Login de usuários por refresh token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshLoginModel login)
        {
            var resultado = await _autenticacaoService.RefreshLogin(login);
            return Ok(resultado);
        }

        /// <summary>
        /// Alterar senha de usuário
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AlterarSenha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Alterar([FromBody] AlterarSenhaModel model)
        {
            _usuarioManager.AlterarSenha(model);
            return Ok();
        } 

        /// <summary>
        /// Verificar se token possui role de Cliente
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = PerfilUsuarioExtensions.Cliente)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("sou-cliente")]
        public IActionResult TesteRoleCliente()
        {
            return Ok();
        }

        /// <summary>
        /// Verificar se token possui role de Funcionário
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("sou-operador")]
        public IActionResult TesteRoleOperador()
        {
            return Ok();
        }
        
        /// <summary>
        /// Verificar se token atende a política de acesso de "Administradores"
        /// </summary>
        /// <returns></returns>
        [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("sou-administrador")]
        public IActionResult TestePoliticaAdmin()
        {
            return Ok();
        }
    }
}
