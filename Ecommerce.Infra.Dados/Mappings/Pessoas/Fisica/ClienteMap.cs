using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.Dados.Mappings.Pessoas.Fisica
{
    public class ClienteMap : PessoaFisicaMapBase<Cliente>, IEntityTypeConfiguration<Cliente>
    {
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);
            builder.ToTable("Cliente");
            
            builder.HasOne(t => t.Usuario)
                .WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(x => x.RecebeNewsletterEmail);
        }
    }
}
