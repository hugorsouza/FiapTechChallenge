using System.ComponentModel;
using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Auth.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infra.Auth.Services;

public class UsuarioManager : IUsuarioManager
{
    private readonly ISenhaHasher _hasher;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IValidator<AlterarSenhaModel> _validatorAlterarSenha;
    private readonly IAuthorizationService _authorizationService;
    private HttpContext Context => _contextAccessor.HttpContext ?? throw new InvalidOperationException("Um método que depende do HttpContext foi invocado fora do contexto de um http request");

    public UsuarioManager(ISenhaHasher hasher, IUsuarioRepository usuarioRepository, IHttpContextAccessor contextAccessor, IClienteRepository clienteRepository, IValidator<AlterarSenhaModel> validatorAlterarSenha, IAuthorizationService authorizationService, IFuncionarioRepository funcionarioRepository)
    {
        _hasher = hasher;
        _usuarioRepository = usuarioRepository;
        _clienteRepository = clienteRepository;
        _validatorAlterarSenha = validatorAlterarSenha;
        _authorizationService = authorizationService;
        _funcionarioRepository = funcionarioRepository;
        _contextAccessor = contextAccessor;
    }

    public Usuario CadastrarUsuario(Usuario usuario)
    {
        usuario.Id = _usuarioRepository.CadastrarObterId(usuario);
        return usuario;
    }
    
    public Usuario Alterar(Usuario usuario)
    {
        _usuarioRepository.Alterar(usuario);
        return usuario;
    }
    
    public void AlterarSenha(AlterarSenhaModel model)
    {
        _validatorAlterarSenha.ValidateAndThrow(model);
        var usuario = ObterUsuarioAtual();
        AlterarSenha(model, usuario!);
    }
    
    public void AlterarSenha(AlterarSenhaModel model, Usuario usuario)
    {
        _validatorAlterarSenha.ValidateAndThrow(model);
        usuario.Senha = GerarHashSenha(model.Senha);
        _usuarioRepository.AlterarSenha(usuario);
    }

    public Usuario? ObterUsuarioAtual()
    {
        var email = ObterEmailUsuarioAtual();
        return email is null ? null : ObterUsuarioPorEmail(email);
    }

    public string? ObterEmailUsuarioAtual()
    {
        return Context.User.Identity?.Name;
    }

    public Usuario? ObterPorId(int id)
    {
        var usuario =  _usuarioRepository.ObterPorId(id);
        return ObterDadosRelacionados(usuario);
    }
    
    public Usuario? ObterUsuarioPorEmail(string email)
    {
        email = email.Trim().ToUpperInvariant();
        var usuario = _usuarioRepository.ObterUsuarioPorEmail(email);
        return ObterDadosRelacionados(usuario);
    }

    private Usuario? ObterDadosRelacionados(Usuario? usuario)
    {
        if (usuario is null)
            return null;
        
        switch (usuario.Perfil)
        {
            case PerfilUsuario.Cliente:
                usuario.Cliente = _clienteRepository.ObterPorId(usuario.Id);
                break;
            case PerfilUsuario.Funcionario:
                usuario.Funcionario = _funcionarioRepository.ObterPorId(usuario.Id);
                break;
            default:
                break;
        }

        return usuario;
    }

    public async Task<bool> SouAdministrador()
    {
        var autorizado =
            await _authorizationService.AuthorizeAsync(Context.User, null, CustomPolicies.SomenteAdministrador);
        return autorizado.Succeeded;
    }
    
    public Usuario BuildUsuarioParaCliente(CadastroClienteModel model) => BuildUsuario(model, PerfilUsuario.Cliente);

    public Usuario BuildUsuarioParaFuncionario(CadastroFuncionarioModel model) => BuildUsuario(model, PerfilUsuario.Funcionario);
    
    private Usuario BuildUsuario<T>(T model, PerfilUsuario perfil)
        where T : CadastroUsuarioModelBase
    {
        if (!Enum.IsDefined(perfil))
            throw new InvalidEnumArgumentException($"O perfil {perfil} ainda não foi implementado");
        
        var usuario = new Usuario(
            nomeExibicao: (model.ObterNomeExibicao() ?? "").Trim(),
            email: model.Email.ToLowerInvariant(),
            senha: GerarHashSenha(model.Senha),
            perfil: perfil
        );
        return usuario;
    }

    private string GerarHashSenha(string senhaTexto)
    {
        return _hasher.Hash(senhaTexto);
    }
}