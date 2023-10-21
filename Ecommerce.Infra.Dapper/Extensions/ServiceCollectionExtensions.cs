using Ecommerce.Domain.Repository;
using Ecommerce.Infra.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IProdutoRepository, ProdutoRepository>()
                .AddScoped<ICategoriaRepository, CategoriaRepository>()
                .AddScoped<IFabricanteRepository, FabricanteRepository>()
                .AddScoped<IFornecedorRepository, IFornecedorRepository>();

        }
    }
}
