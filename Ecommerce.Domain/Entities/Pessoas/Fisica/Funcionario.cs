using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Domain.Entities.Pessoas.Fisica;

public class Funcionario : PessoaFisica
{
    public Funcionario(int usuarioId, string nome, string sobrenome, string cpf, DateTime dataNascimento, string cargo, Usuario usuario, bool administrador = false) 
        : base(usuarioId, nome, sobrenome, cpf, dataNascimento, usuario)
    {
        if (usuario.Perfil != PerfilUsuario.Funcionario)
            throw new ArgumentException($"Perfil inválido para {GetType().Name}: {usuario.Perfil}");
        Cargo = cargo;
        Administrador = administrador;
    }

    public Funcionario()
    {
        
    }
    
    public string Cargo { get; set; }
    public bool Administrador { get; set; }
}