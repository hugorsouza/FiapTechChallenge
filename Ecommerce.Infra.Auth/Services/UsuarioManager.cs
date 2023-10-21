using System.ComponentModel;
using Ecommerce.Application.Model.Pessoas;
using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Interfaces.Repository;

namespace Ecommerce.Infra.Auth.Services;

public class UsuarioManager : IUsuarioManager
{
    private readonly ISenhaHasher _hasher;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioManager(ISenhaHasher hasher, IUsuarioRepository usuarioRepository)
    {
        _hasher = hasher;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario> CadastrarUsuario(Usuario usuario)
    {
        await _usuarioRepository.Inserir(usuario);
        return usuario;
    }
    
    public async Task<Usuario> AlterarSenha(Usuario usuario, string novaSenhaTextoPlano)
    {
        var senhaHash = GerarHashSenha(novaSenhaTextoPlano);
        //await _usuarioRepository.Inserir(usuario);
        return usuario;
    }

    public Usuario CriarUsuarioParaCliente(CadastroClienteModel model) => CriarUsuario(model, PerfilUsuario.Cliente);

    public Usuario CriarUsuarioParaFuncionario(CadastroFuncionarioModel model) => CriarUsuario(model, PerfilUsuario.Funcionario);

    //public Usuario CriarUsuarioParaEmpresaTerceira(CadastroEmpresaModel model) => CriarUsuario(model, PerfilUsuario.EmpresaTerceira);
    
    private Usuario CriarUsuario<T>(T model, PerfilUsuario perfil)
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