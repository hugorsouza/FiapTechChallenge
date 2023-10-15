using Ecommerce.Domain.Entity.Autenticacao;
using Ecommerce.Domain.Entity.Shared;

namespace Ecommerce.Domain.Entity
{
    public class PessoaFisica : EntityBase, IUsuario
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
