using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Repository;
using Ecommerce.Infra.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IUsuarioRepository, MockUsuarioRepository>()
                .AddScoped<IPessoaFisicaRepository, MockPessoaFisicaRepository>()
                .AddScoped<IProdutoRepository, ProdutoRepository>()
                .AddScoped<ICategoriaRepository, CategoriaRepository>()
                .AddScoped<IFabricanteRepository, FabricanteRepository>()
                .AddScoped<IFornecedorRepository, FornecedorRepository>();
        }
    }
}
