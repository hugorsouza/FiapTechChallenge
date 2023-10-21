using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Services.Interfaces.Pessoas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }
    
    [AllowAnonymous]
    [HttpPost("cadastro")]
    [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] CadastroClienteModel cadastro)
    {
        var resultado = await _clienteService.Cadastrar(cadastro);
        return Ok(resultado);
    } 
}