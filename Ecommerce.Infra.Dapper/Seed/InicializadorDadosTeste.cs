using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dapper.Seed
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
            var usuarioRepository = provider.GetService<IUsuarioRepository>();
            var clienteRepository = provider.GetService<IClienteRepository>();
            var funcionarioRepository = provider.GetService<IFuncionarioRepository>();
            
            if (!clienteRepository.ObterTodos().Any())
            {
               
                foreach (var cliente in UsuariosDadosTeste.Clientes)
                {
                    cliente.Id = usuarioRepository.CadastrarObterId(cliente.Usuario);
                    clienteRepository.Cadastrar(cliente);
                }
            }
            
            if (!funcionarioRepository.ObterTodos().Any())
            {
                foreach (var funcionario in UsuariosDadosTeste.Funcionarios)
                {
                    funcionario.Id = usuarioRepository.CadastrarObterId(funcionario.Usuario);
                    funcionarioRepository.Cadastrar(funcionario);
                }
            }
        }
    }
}
