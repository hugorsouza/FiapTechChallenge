using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.CrossCutting
{
    public class BootStapper
    {
        private readonly IServiceCollection _services;

        public static IServiceCollection AddAplicationServices(IServiceCollection services) 
        {
            services
                .AddScoped<IFazerPedidoService, FazerPedidoService>();

            //_services = services;
            return services;
        }
    }
}