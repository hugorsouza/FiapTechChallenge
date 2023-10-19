using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Repository;
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
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public override void Cadastrar(Produto entidade)
        {
            _produtoRepository.Cadastrar(entidade);
        }

        public override void Alterar(Produto entidade)
        {
            _produtoRepository.Alterar(entidade);
        }     
        public override void Deletar(int id)
        {
            _produtoRepository.Deletar(id);
        }

        public override Produto ObterPorId(int id)
        {
           return  _produtoRepository.ObterPorId(id);
        }

        public override IList<Produto> ObterTodos()
        {
            return _produtoRepository.ObterTodos();
        }
    }
}
