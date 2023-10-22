using Ecommerce.Domain.Entities.Pessoas.Autenticacao;

namespace Ecommerce.Domain.Entities.Pessoas.Fisica;

public class Cliente : PessoaFisica
{
    public Cliente(
        int usuarioId,
        string nome, 
        string sobrenome,
        string cpf,
        DateTime dataNascimento,
        Usuario usuario,
        bool recebeNewsletterEmail = false) 
        : base(
            usuarioId, nome, sobrenome, cpf,
            dataNascimento, usuario)
    {
        if (usuario.Perfil != PerfilUsuario.Cliente)
            throw new ArgumentException($"Perfil inválido para {GetType().Name}: {usuario.Perfil}");
        RecebeNewsletterEmail = recebeNewsletterEmail;
    }

    public Cliente()
    {
            
    }

    public bool RecebeNewsletterEmail { get; set; }
}