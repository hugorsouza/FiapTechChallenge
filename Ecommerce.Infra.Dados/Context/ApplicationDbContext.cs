using Ecommerce.Domain.Entities.Pessoas.Autenticacao;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Entities.Pessoas.Juridica;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infra.Dados.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            
        }
    
        public required DbSet<Cliente> Clientes { get; set; }
        public required DbSet<Empresa> Empresas { get; set; }
        public required DbSet<Funcionario> Funcionarios { get; set; }
        public required DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
