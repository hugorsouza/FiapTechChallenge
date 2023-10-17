using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Domain.Entities.Autenticacao
{
    public class Usuario : EntityBase
    {
        public string NomeExibicao { get; set; }
        public string Email { get; set; }
        public string EmailNormalizado { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public virtual PessoaFisica? PessoaFisica { get; set; }
    }
}
