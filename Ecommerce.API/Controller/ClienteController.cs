using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller;

[Authorize(Roles = $"{PerfilUsuarioExtensions.Cliente},{PerfilUsuarioExtensions.Funcionario}")]
[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IUsuarioManager _usuarioManager;
    public ClienteController(IClienteService clienteService, IUsuarioManager usuarioManager)
    {
        _clienteService = clienteService;
        _usuarioManager = usuarioManager;
    }
    
    /// <summary>
    /// Obter todos os clientes cadastrados.
    /// Requer role de funcionário.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClienteViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterTodos()
    {
        var resultado = await _clienteService.ObterTodos();
        return Ok(resultado);
    }
    
    /// <summary>
    /// Cadastrar clientes.
    /// </summary>
    /// <param name="cadastro"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cadastrar([FromBody] CadastroClienteModel cadastro)
    {
        var resultado = await _clienteService.Cadastrar(cadastro);
        return Ok(resultado);
    } 
    
    /// <summary>
    /// Alterar dados pessoais do cliente logado.
    /// </summary>
    /// <param name="cadastro"></param>
    /// <returns></returns>
    [Authorize(Roles = PerfilUsuarioExtensions.Cliente)]
    [HttpPut]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alterar([FromBody] AlterarClienteModel cadastro)
    {
        var resultado = await _clienteService.Alterar(cadastro);
        return Ok(resultado);
    }

    /// <summary>
    /// Alterar dados pessoais do cliente logado.
    /// </summary>
    /// <param name="cadastro"></param>
    /// <returns></returns>
    [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Alterar([FromRoute] int id, [FromBody] AlterarClienteAdminModel cadastro)
    {
        var resultado = await _clienteService.Alterar(id, cadastro);
        return Ok(resultado);
    }

    /// <summary>
    /// Obter dados pessoais do cliente logado.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = PerfilUsuarioExtensions.Cliente)]
    [HttpGet("MeusDados")]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterDadosPessoais()
    {
        var usuario = _usuarioManager.ObterUsuarioAtual();
        var resultado = _clienteService.BuildViewModel(usuario.Cliente);
        return Ok(resultado);
    }
    
    /// <summary>
    /// Obter um cliente específico pelo ID.
    /// Requer role de funcionário.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = PerfilUsuarioExtensions.Funcionario)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ObterPorId([FromRoute] int id)
    {
        var resultado = await _clienteService.ObterPorId(id);
        return Ok(resultado);
    }
}