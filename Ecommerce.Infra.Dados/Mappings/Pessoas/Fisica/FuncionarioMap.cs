using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.Dados.Mappings.Pessoas.Fisica
{
    public class FuncionarioMap : PessoaFisicaMapBase<Funcionario>, IEntityTypeConfiguration<Funcionario>
    {
        public override void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            base.Configure(builder);
            builder.ToTable("Funcionario");

            builder.HasOne(t => t.Usuario)
                .WithOne(p => p.Funcionario)
                .HasForeignKey<Funcionario>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            
            builder.Property(x => x.Cargo)
                .HasMaxLength(256);
        }
    }
}
