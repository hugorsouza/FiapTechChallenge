using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.Dados.Mappings.Pessoas
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.NomeExibicao)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.EmailNormalizado)
                .HasComputedColumnSql("TRIM(UPPER([Email]))");

            builder.Property(x => x.Senha)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.DataCadastroUtc);
            builder.Property(x => x.DataAlteracaoUtc);
            builder.Property(x => x.Perfil)
                .HasColumnType("int");
        }
    }
}
