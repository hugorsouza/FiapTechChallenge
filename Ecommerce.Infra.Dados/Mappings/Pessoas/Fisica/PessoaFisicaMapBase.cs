using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.Dados.Mappings.Pessoas.Fisica
{
    public abstract class PessoaFisicaMapBase<TPessoa>
        where TPessoa : PessoaFisica
    {
        public virtual void Configure(EntityTypeBuilder<TPessoa> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(x => x.Cpf).IsUnique();

            builder.Property(x => x.Id)
                .HasColumnOrder(0);

            builder.Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnOrder(1);

            builder.Property(x => x.Sobrenome)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnOrder(2);

            builder.Property(x => x.Cpf)
                .HasMaxLength(11)
                .IsRequired()
                .HasColumnOrder(3);;

            builder.Property(x => x.DataNascimento)
                .HasColumnOrder(4);;
            builder.Property(x => x.DataCadastroUtc)
                .HasColumnOrder(5);;
            builder.Property(x => x.DataAlteracaoUtc)
                .HasColumnOrder(6);;
        }
    }
}
