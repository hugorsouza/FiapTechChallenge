using System.Reflection;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Factory;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Infra.Dapper.Repositories;
using Ecommerce.Infra.Dapper.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddConnection()
                .AddRepositoriesComAssemblyScan();

        }

        private static IServiceCollection AddConnection(this IServiceCollection services)
        {
            return services.AddScoped<IDbConnectionFactory, DbConnectionFactory>()
                .AddScoped<UnitOfWork>()
                .AddScoped<ITransactionService>(sp => sp.GetService<UnitOfWork>())
                .AddScoped<IUnitOfWork>(sp => sp.GetService<UnitOfWork>());
        }
        
        private static IServiceCollection AddRepositoriesComAssemblyScan(this IServiceCollection services)
        {
            var assemblyInfra = typeof(Repository<>).Assembly;
            var referencedAssemblies = assemblyInfra.GetReferencedAssemblies().Select(Assembly.Load);
            var assemblies = new List<Assembly> { assemblyInfra }.Concat(referencedAssemblies).ToArray();

            var repositoryTypes = assemblies.SelectMany(assembly =>
                assembly.ExportedTypes.Where(
                    type => type.IsSubclassOf(typeof(Repository))
                            && !type.IsGenericType
                            && type is { IsInterface: false, IsAbstract: false }
                            && type != typeof(Repository)
                            && type != typeof(Repository<>)
                )
            ).ToList();

            var repositoryInterfaceTypes = assemblies.SelectMany(assembly =>
                assembly.ExportedTypes.Where(
                    type =>
                        typeof(IRepository).IsAssignableFrom(type)
                        && type is { IsGenericType: false, IsInterface: true }
                        && type != typeof(IRepository)
                        && type != typeof(IRepository<>)
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

            return services;
        }
    }
}
