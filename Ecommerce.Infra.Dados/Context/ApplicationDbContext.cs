using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infra.Dados.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            
        }

        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
