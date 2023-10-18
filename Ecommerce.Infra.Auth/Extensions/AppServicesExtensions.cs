using Ecommerce.Application.Services;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Auth.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            return services.AddScoped<IProdutoService, ProdutoService>()
                .AddScoped<ICategoriaService, CategoriaService>()
                .AddScoped<IFabricanteService, FabricanteService>()
                .AddScoped<IFornecedorService, FornecedorService>();
        }
    }
}
