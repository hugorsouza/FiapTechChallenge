using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dapper.Registration
{
    public static class RegistrationDapper
    {
        private static IServiceCollection _serviceCollection;

        public static void Registration(IServiceCollection services, IConfiguration configuration) 
        {
            _serviceCollection = services;

            services.AddTransient<IServiceProvider>(s =>
            {
                var service = s.GetRequiredService<IFazerPedidoDomainService>();
                var fazerPedidoService = s.GetRequiredService<IFazerPedidoDomainService>();

                return new FazerPedidoService();
            });

            services.AddTransient<IFazerPedidoDomainService, FazerPedidoDomainService>();

            services.AddTransient<IFazerPedidoRepository>(s =>
            {
                return new FazerPedidosRepositoryDapper(configuration.GetConnectionString("Ecommerce"));
            });
        }
        public static T Resolve<T>()
        {
            var provider = _serviceCollection.BuildServiceProvider();
            return provider.GetRequiredService<T>();
        }
    }
}
