using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Infra.Auth.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller;

[Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
[ApiController]
[Route("api/[controller]")]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioService _funcionarioService;
    private readonly IUsuarioManager _usuarioManager;
    public FuncionarioController(IFuncionarioService funcionarioService, IUsuarioManager usuarioManager)
    {
        _funcionarioService = funcionarioService;
        _usuarioManager = usuarioManager;
    }

    /// <summary>
    /// Obter todos os funcionários.
    /// Requer permissão de administrador.
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
    [HttpGet("ObterTodos")]
    [ProducesResponseType(typeof(IEnumerable<FuncionarioViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterTodos()
    {
        var resultado = await _funcionarioService.ObterTodos();
        return Ok(resultado);
    }
    
    /// <summary>
    /// Cadastrar funcionários.
    /// Requer permissão de administrador.
    /// </summary>
    /// <param name="cadastro"></param>
    /// <returns></returns>
    [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
    [HttpPost("Cadastrar")]
    [ProducesResponseType(typeof(FuncionarioViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cadastrar([FromBody] CadastroFuncionarioModel cadastro)
    {
        var resultado = await _funcionarioService.Cadastrar(cadastro);
        return Ok(resultado);
    } 
    
    /// <summary>
    /// Alterar funcionário por ID.
    /// Requer permissão de administrador.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cadastro"></param>
    /// <returns></returns>
    [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(FuncionarioViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alterar([FromRoute] int id, [FromBody] AlterarFuncionarioModel cadastro)
    {
        var resultado = await _funcionarioService.Alterar(cadastro, id);
        return Ok(resultado);
    }

    /// <summary>
    /// Obter dados pessoais do funcionário logado.
    /// </summary>
    /// <returns></returns>
    [HttpGet("MeusDados")]
    [ProducesResponseType(typeof(FuncionarioViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterDadosPessoais()
    {
        var usuario = _usuarioManager.ObterUsuarioAtual();
        var resultado = _funcionarioService.BuildViewModel(usuario.Funcionario);
        return Ok(resultado);
    }
    
    /// <summary>
    /// Obter funcionário por ID.
    /// Requer permissão de administrador.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cadastro"></param>
    /// <returns></returns>
    [Authorize(Policy = CustomPolicies.SomenteAdministrador)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FuncionarioViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterPorId([FromRoute] int id)
    {
        var resultado = await _funcionarioService.ObterPorId(id);
        return Ok(resultado);
    }
}