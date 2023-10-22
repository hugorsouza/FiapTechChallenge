using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Entities.Pessoas.Juridica;
using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Domain.Entities.Pessoas.Autenticacao
{
    public class Usuario : EntityBase
    {
        public Usuario(string nomeExibicao, string email, string senha, PerfilUsuario perfil)
        {
            var dataCadastro = DateTime.UtcNow;
            NomeExibicao = nomeExibicao;
            Email = email;
            EmailNormalizado = email.Trim().ToUpperInvariant();
            Senha = senha;
            DataCadastro = dataCadastro;
            DataAlteracao = dataCadastro; //não foi alterado
            Perfil = perfil;
            EmailConfirmado = false;
            Ativo = true;
        }

        public Usuario()
        {
            
        }

        public string NomeExibicao { get; set; }
        public string Email { get; set; }
        public string EmailNormalizado { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public bool EmailConfirmado { get; set; }
        public bool Ativo { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
