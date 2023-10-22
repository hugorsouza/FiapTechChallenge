using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Model.Pessoas.Autenticacao;
using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Interfaces.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infra.Auth.Services;

public class UsuarioManager : IUsuarioManager
{
    private readonly ISenhaHasher _hasher;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly HttpContext? _context;//Será nulo fora do escopo de requests
    private readonly IValidator<AlterarSenhaModel> _validatorAlterarSenha;
    public UsuarioManager(ISenhaHasher hasher, IUsuarioRepository usuarioRepository, IHttpContextAccessor contextAccessor, IClienteRepository clienteRepository, IValidator<AlterarSenhaModel> validatorAlterarSenha)
    {
        _hasher = hasher;
        _usuarioRepository = usuarioRepository;
        _clienteRepository = clienteRepository;
        _validatorAlterarSenha = validatorAlterarSenha;
        _context = contextAccessor.HttpContext;
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
        NullGuardHttpContext(_context);
        var email = _context.User.Identity?.Name;
        return email is null ? null : ObterUsuarioPorEmail(email);
    }

    public Usuario? ObterUsuarioPorEmail(string email)
    {
        email = email.Trim().ToUpperInvariant();
        var usuario = _usuarioRepository.ObterUsuarioPorEmail(email);
        if (usuario is null)
            return null;
        
        switch (usuario.Perfil)
        {
            case PerfilUsuario.Cliente:
                usuario.Cliente = _clienteRepository.ObterPorId(usuario.Id);
                break;
            case PerfilUsuario.Funcionario:
            case PerfilUsuario.EmpresaTerceira:
            default:
                break;
        }

        return usuario;
    }

    public Usuario BuildUsuarioParaCliente(CadastroClienteModel model) => BuildUsuario(model, PerfilUsuario.Cliente);

    public Usuario BuildUsuarioParaFuncionario(CadastroFuncionarioModel model) => BuildUsuario(model, PerfilUsuario.Funcionario);

    //public Usuario CriarUsuarioParaEmpresaTerceira(CadastroEmpresaModel model) => BuildUsuario(model, PerfilUsuario.EmpresaTerceira);
    
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
    
    private static void NullGuardHttpContext([NotNull]HttpContext? context)
    {
        if (context is null) throw new InvalidOperationException("Um método que depende do HttpContext foi invocado fora do contexto de um http request");
    }
}