using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Infra.Dados.Context;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dados.Repositories.Shared;

namespace Ecommerce.Infra.Dados.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration, bool isDev)
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Ecommerce"),
                        b =>
                        {
                        }
                    );
                    options.EnableDetailedErrors(detailedErrorsEnabled: isDev);
                })
                .AddRepositories();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var assemblyInfra = typeof(RepositoryBase<>).Assembly;
            var referencedAssemblies = assemblyInfra.GetReferencedAssemblies().Select(Assembly.Load);
            var assemblies = new List<Assembly> { assemblyInfra }.Concat(referencedAssemblies).ToArray();

            var repositoryTypes = assemblies.SelectMany(assembly =>
                assembly.ExportedTypes.Where(
                    type => type.IsSubclassOf(typeof(RepositoryBase))
                            && !type.IsGenericType
                            && type is { IsInterface: false, IsAbstract: false }
                            && type != typeof(RepositoryBase)
                            && type != typeof(RepositoryBase<>)
                )
            ).ToList();

            var repositoryInterfaceTypes = assemblies.SelectMany(assembly =>
                assembly.ExportedTypes.Where(
                    type =>
                        typeof(IRepositoryBase).IsAssignableFrom(type)
                        && type is { IsGenericType: false, IsInterface: true }
                        && type != typeof(IRepositoryBase)
                        && type != typeof(IRepositoryBase<>)
                )
            ).ToList();

            foreach (var repositoryType in repositoryTypes)
            {
                var repInterfaceType = repositoryInterfaceTypes
                    .FirstOrDefault(repInterface => repInterface.IsAssignableFrom(repositoryType));
                if (repInterfaceType is null)
                    continue;

                services.AddScoped(repInterfaceType, repositoryType);
            }

            //return services.AddScoped<IUsuarioRepository, UsuarioRepository>()
            //    .AddScoped<IPessoaFisicaRepository, PessoaFisicaRepository>();
            return services;
        }
    }
}
