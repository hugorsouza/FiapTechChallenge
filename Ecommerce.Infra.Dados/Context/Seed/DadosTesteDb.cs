using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dados.Context.Seed
{
    public static class DadosTesteDb
    {
        public static WebApplication SeedDatabase(this WebApplication app, bool migrate)
        {
            ArgumentNullException.ThrowIfNull(app);

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (migrate)
            {
                context.Database.Migrate();
            }

            Inicializar(context);

            return app;
        }

        public static void Inicializar(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            context.Database.BeginTransaction();

            InserirPessoasEUsuarios(context);
            context.SaveChanges();

            context.Database.CommitTransaction();
        }

        public static void InserirPessoasEUsuarios(ApplicationDbContext context)
        {
            if (!context.PessoasFisicas.Any())
            {
                context.AddRange(DadosTeste.Pessoas);
            }
        }
    }
}
