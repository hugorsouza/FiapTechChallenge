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
    public class CategoriaService : Service<Categoria>, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public override void Alterar(Categoria entidade)
        {
            _categoriaRepository.Alterar(entidade);
        }

        public override void Cadastrar(Categoria entidade)
        {
            _categoriaRepository.Cadastrar(entidade);
        }

        public override void Deletar(int id)
        {
            _categoriaRepository.Deletar(id);
        }

        public override Categoria ObterPorId(int id)
        {
            return _categoriaRepository.ObterPorId(id);
        }

        public override IList<Categoria> ObterTodos()
        {
            return _categoriaRepository.ObterTodos();
        }
    }
}
