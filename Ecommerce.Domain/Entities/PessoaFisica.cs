using Ecommerce.Domain.Entities.Autenticacao;
using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Domain.Entities
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
        public virtual Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
    }
}
