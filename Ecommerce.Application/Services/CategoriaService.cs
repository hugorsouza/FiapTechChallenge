using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Entities.Produtos;
using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public Categoria Alterar(Categoria model)
        {
            var categoria = ObterPorId(model.Id);

            if (categoria is null)
                throw new ArgumentException($"Erro: A Categoria {model.Id} não está cadastrada na Base!");

            _categoriaRepository.Alterar(model);

            return model;
        }

        public CategoriaViewModel Cadastrar(CategoriaViewModel model)
        {
            var categoria = buidCategoria(model);

            if (ObterTodos().Where(x=> x!=null)
                .Any(x => x.Nome.Equals(categoria.Nome)))
                throw new ArgumentException($"Erro: A Categoria {categoria.Nome} Já está cadastrada!");

            _categoriaRepository.Cadastrar(categoria);

            var categoriaViewModel = BuildViewModel(categoria);

            return categoriaViewModel;
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public Categoria ObterPorId(int id)
        {
            var result = _categoriaRepository.ObterPorId(id);
            return result;
        }

        public  IList<Categoria> ObterTodos()
        {
            return _categoriaRepository.ObterTodos();
        }

        private CategoriaViewModel BuildViewModel(Categoria categoria)
        {
            if (categoria is null) 
                return null;

            return new CategoriaViewModel(categoria.Nome, categoria.Descricao, categoria.Ativo);
        }

        private Categoria buidCategoria(CategoriaViewModel model)
        {
            if (model is null)
                return null;

            return new Categoria(model.Descricao, model.Nome, model.Ativo);

        }
    }
}
