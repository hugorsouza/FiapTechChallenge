using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Domain.Entities.Pessoas.Fisica;

public class Funcionario : PessoaFisica
{
    public Funcionario(string nome, string sobrenome, string cpf, DateTime dataNascimento, string cargo, Usuario usuario) 
        : base(nome, sobrenome, cpf, dataNascimento, usuario)
    {
        if (usuario.Perfil != PerfilUsuario.Funcionario)
            throw new ArgumentException($"Perfil inválido para {GetType().Name}: {usuario.Perfil}");
        Cargo = cargo;
    }

    public Funcionario()
    {
        
    }
    
    public string Cargo { get; set; }
}