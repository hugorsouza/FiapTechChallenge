using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dapper.DataBase.Seed
{
    public static class InicializadorDadosTeste
    {
        public static WebApplication SeedDatabase(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            Inicializar(services);
            return app;
        }

        public static void Inicializar(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);
            var uow = provider.GetService<IUnitOfWork>();
            uow.BeginTransaction();

            InserirUsuarios(provider);

            uow.Commit();
        }

        public static void InserirUsuarios(IServiceProvider provider)
        {
            var clienteRepository = provider.GetService<IClienteRepository>();
            if (clienteRepository.ObterTodos().Any())
                return;
                
            //var funcionarioRepository = provider.GetService<IFuncionarioRepository>();
            var usuarioRepository = provider.GetService<IUsuarioRepository>();
            foreach (var cliente in UsuariosDadosTeste.Clientes)
            {
                cliente.Id = usuarioRepository.CadastrarObterId(cliente.Usuario);
                clienteRepository.Cadastrar(cliente);
            }
            
            foreach (var funcionario in UsuariosDadosTeste.Funcionarios)
            {
                
            }
        }
    }
}
