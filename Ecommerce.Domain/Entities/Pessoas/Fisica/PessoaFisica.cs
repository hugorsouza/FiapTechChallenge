using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Domain.Entities.Pessoas.Fisica
{
    public abstract class PessoaFisica : EntityBase, IUsuario
    {
        protected PessoaFisica(int usuarioId, string nome, string sobrenome, string cpf, DateTime dataNascimento, Usuario usuario)
        {
            var agora = DateTime.UtcNow;
            Id = usuarioId;
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            DataCadastro = agora;
            DataAlteracao = agora;
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
        
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
        public Usuario Usuario { get; set; }

        public string NomeExibicao() => $"{Nome} {Sobrenome}";
    }
}
