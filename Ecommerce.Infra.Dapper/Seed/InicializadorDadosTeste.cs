using Ecommerce.Domain.Entities.Estoque;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Domain.Repository;
using Ecommerce.Infra.Dapper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infra.Dapper.Seed
{
    public static class InicializadorDadosTeste
    {
        public static async Task<WebApplication> SeedDatabase(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app);

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            await Inicializar(services);
            return app;
        }

        public static async Task Inicializar(IServiceProvider provider)
        {
            ArgumentNullException.ThrowIfNull(provider);
            var uow = provider.GetService<IUnitOfWork>();
            uow.BeginTransaction();
            InserirUsuarios(provider);
            uow.Commit();

            await InserirProdutos(provider);
        }

        public static void InserirUsuarios(IServiceProvider provider)
        {
            var usuarioRepository = provider.GetService<IUsuarioRepository>();
            var clienteRepository = provider.GetService<IClienteRepository>();
            var funcionarioRepository = provider.GetService<IFuncionarioRepository>();
            
            if (!clienteRepository.ObterTodos().Any())
            {
               
                foreach (var cliente in DadosIniciaisTeste.Clientes)
                {
                    cliente.Id = usuarioRepository.CadastrarObterId(cliente.Usuario);
                    clienteRepository.Cadastrar(cliente);
                }
            }
            
            if (!funcionarioRepository.ObterTodos().Any())
            {
                foreach (var funcionario in DadosIniciaisTeste.Funcionarios)
                {
                    funcionario.Id = usuarioRepository.CadastrarObterId(funcionario.Usuario);
                    funcionarioRepository.Cadastrar(funcionario);
                }
            }
        }

        public static async Task InserirProdutos(IServiceProvider provider)
        {
            const string hardware = "Hardware";
            const string periferico = "Periféricos";
            const string celular = "Celulares";

            var produtoRepository = provider.GetService<IProdutoRepository>();
            if (produtoRepository.ObterTodos().Any())
                return;

            var usuarioRepository = provider.GetService<IUsuarioRepository>();
            var fabricanteRepository = provider.GetService<IFabricanteRepository>();
            var categoriaRepository = provider.GetService<ICategoriaRepository>();
            var categoriaNomes = new string[]
            {
                hardware, periferico, celular
            };
            var categorias = categoriaRepository.ObterTodos();
            foreach (var categoria in categoriaNomes)
            {
                if (!categorias.Any(x => string.Equals(x.Nome, categoria, StringComparison.InvariantCultureIgnoreCase)))
                    categoriaRepository.Cadastrar(new Categoria($"Categoria de teste: {categoria}", categoria, true));
            }
            categorias = categoriaRepository.ObterTodos();

            var fabricantes = fabricanteRepository.ObterTodos();
            if (!fabricantes.Any())
            {
                foreach (var _ in Enumerable.Range(1, 15))
                    fabricanteRepository.Cadastrar(DadosIniciaisTeste.FakerFabricante);
            }
            fabricantes = fabricanteRepository.ObterTodos();

            var usuarioParaPedido = usuarioRepository.ObterUsuarioPorEmail(DadosIniciaisTeste.EmailTesteFuncionarioAdmin);
            var categoriasTeste = categorias
                .Where(x => categoriaNomes.Any(
                    catConst => string.Equals(catConst, x.Nome, StringComparison.InvariantCultureIgnoreCase)
                    )
                ).ToList();
            var rand = new Random();
            foreach (var categoria in categoriasTeste)
            {
                var count = 0;
                foreach (var fabricante in fabricantes)
                {
                    var estoque = new Estoque(usuarioParaPedido.Funcionario.Cpf, 
                        usuarioParaPedido.NomeExibicao, 
                        0, 
                        rand.Next(1, 999), 
                        DateTime.UtcNow);
                    var produto = new Produto(true, 
                        $"{categoria.Nome} {count}", 
                        rand.Next(10, 9999),
                        $"Teste {count}", 
                        fabricante.Id,
                        "", 
                        categoria.Id);

                    await produtoRepository.CadastrarAsync(produto, estoque);
                    produtoRepository.AdicionaUrlImagem(produto.Id, $"https://fiaptechchallenger.blob.core.windows.net/imagemprodutos/{produto.Id}.jpg");

                    count++;
                }
            }
        }
    }
}
