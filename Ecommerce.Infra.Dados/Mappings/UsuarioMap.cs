using Ecommerce.Domain.Entities.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.Dados.Mappings
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
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.EmailNormalizado)
                .HasComputedColumnSql("TRIM(UPPER([Email]))");

            builder.Property(x => x.Senha)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.DataCadastro);
            builder.Property(x => x.DataAlteracao);
            builder.Property(x => x.Perfil)
                .HasColumnType("int");
        }
    }
}
