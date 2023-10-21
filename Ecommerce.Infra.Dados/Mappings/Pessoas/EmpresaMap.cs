using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Juridica;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infra.Dados.Mappings.Pessoas;

public class EmpresaMap : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("Empresa");
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Cnpj)
            .IsUnique();
        
        builder.HasOne(t => t.Usuario)
            .WithOne(p => p.Empresa)
            .HasForeignKey<Empresa>(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
            
        builder.Property(x => x.Cnpj)
            .HasMaxLength(14);
        
        builder.Property(x => x.RazaoSocial)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(x => x.NomeFantasia)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(x => x.EmailContato)
            .HasMaxLength(256)
            .IsRequired(false);
    }
}