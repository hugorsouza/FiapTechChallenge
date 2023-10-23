using Ecommerce.Application.Model.Pessoas.Cadastro;
using Ecommerce.Application.Model.Produto;
using Ecommerce.Domain.Entities.Pessoas.Fisica;
using Ecommerce.Domain.Entities.Produtos;
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
            {
                //Lançar Erro
                
            }

            _categoriaRepository.Alterar(model);

            return model;
        }

        public CategoriaViewModel Cadastrar(CategoriaViewModel model)
        {
            var categoria = buidCategoria(model);

            var categorias = ObterTodos().ToList();

            if (categorias.Any(x => x.Nome.Equals(categoria.Nome))) 
                  //Lança Exceção

            _categoriaRepository.Cadastrar(categoria);

            var categoriaViewModel = BuildViewModel(categoria);

            return categoriaViewModel;
        }

        public void Deletar(int id)
        {

            var categoria = ObterPorId(id);

            if (categoria is null)
            {
                //Lançar Erro

            }
            _categoriaRepository.Deletar(id);
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
