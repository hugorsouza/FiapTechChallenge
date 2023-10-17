using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.Dados.Mappings
{
    public class PessoaFisicaMap : IEntityTypeConfiguration<PessoaFisica>
    {
        public void Configure(EntityTypeBuilder<PessoaFisica> builder)
        {
            builder.ToTable("PessoaFisica");

            builder.HasKey(t => t.Id);

            builder.HasIndex(x => x.Cpf).IsUnique();

            builder.HasOne(t => t.Usuario)
                .WithOne(p => p.PessoaFisica)
                .HasForeignKey<PessoaFisica>(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasIndex(x => x.UsuarioId).IsUnique();

            builder.Property(x => x.Id)
                .UseIdentityColumn();


            builder.Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Sobrenome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(x => x.DataNascimento);
            builder.Property(x => x.DataCadastro);
            builder.Property(x => x.DataAlteracao);
        }
    }
}
