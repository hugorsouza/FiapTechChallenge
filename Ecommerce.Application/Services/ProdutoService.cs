using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class ProdutoService : Service<Produto>, IProdutoService
    {
        public override void Alterar(Produto entidade)
        {
            throw new NotImplementedException();
        }

        public override void Cadastrar(Produto entidade)
        {
            Console.WriteLine($"Nome: {entidade.Nome}");
            Console.WriteLine($"Ativo: {entidade.Ativo}");
            Console.WriteLine($"Preco: {entidade.Preco}");
            Console.WriteLine($"Descricao: {entidade.Descricao}");
            Console.WriteLine($"IdFabricante: {entidade.IdFabricante}");
            Console.WriteLine($"UrlImagem: {entidade.UrlImagem}");
            Console.WriteLine($"Nome: {entidade.Nome}");
        }

        public override void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public override Produto ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public override IList<Produto> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
