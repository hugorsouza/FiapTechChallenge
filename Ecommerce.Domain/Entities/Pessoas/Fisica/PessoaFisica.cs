using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Domain.Entities.Pessoas.Fisica
{
    public abstract class PessoaFisica : EntityBase, IUsuario
    {
        protected PessoaFisica(string nome, string sobrenome, string cpf, DateTime dataNascimento, Usuario usuario)
        {
            var agora = DateTime.UtcNow;
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            DataCadastroUtc = agora;
            DataAlteracaoUtc = agora;
            Usuario = usuario;
        }

        protected PessoaFisica()
        {
            //Faker
        }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        
        public DateTime DataCadastroUtc { get; set; }
        public DateTime DataAlteracaoUtc { get; set; }
        public Usuario Usuario { get; set; }
    }
}
