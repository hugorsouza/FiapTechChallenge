
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Infra.Dapper.Interfaces;
using Ecommerce.Infra.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application.Registration
{
    public static class RegistrationDapper
    {
        private static IServiceCollection _services;

        public static void Registration(this IServiceCollection services, IConfiguration configuration) 
        {
            _services = services;

            services.AddTransient<IConsultarPedidoService>(s =>
            {
                var service = s.GetRequiredService<IConsultarPedidoDomainService>();
                var consultarPedidoDomainService = s.GetRequiredService<IConsultarPedidoDomainService>();

                return new ConsultarPedidoService(consultarPedidoDomainService);
            });

            services.AddTransient<IConsultarPedidoDomainService, ConsultarPedidoDomainService>();
            services.AddTransient<IConsultarPedidoRepositoryDapper>(s =>
            {
                return new ConsultarPedidoRepositoryDapper(configuration.GetConnectionString("Ecommerce"));
            });

        }
        //public static T Resolve<T>()
        //{
        //    var provider = _services.BuildServiceProvider();
        //    return provider.GetRequiredService<T>();
        //}
    }
}
