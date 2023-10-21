using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
