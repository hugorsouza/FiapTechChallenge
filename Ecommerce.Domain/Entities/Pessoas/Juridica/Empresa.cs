using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Domain.Entities.Pessoas.Juridica;

public class Empresa : EntityBase, IUsuario
{
    public Empresa(string emailContato, string cnpj, string razaoSocial, string nomeFantasia, Usuario usuario)
    {
        if (usuario.Perfil != PerfilUsuario.Funcionario)
            throw new ArgumentException($"Perfil inválido para {GetType().Name}: {usuario.Perfil}");
        
        Cnpj = cnpj;
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        EmailContato = emailContato;
        Usuario = usuario;
    }

    public Empresa()
    {
        
    }
    
    public string Cnpj { get; set; }
    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public string EmailContato { get; set; }
    public Usuario Usuario { get; set; }
    public int UsuarioId { get; set; }
}