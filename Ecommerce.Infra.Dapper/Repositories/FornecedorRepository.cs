using Ecommerce.Domain.Entity;
using Ecommerce.Domain.Repository;
using Ecommerce.Domain.Services;
using Ecommerce.Infra.Dapper.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Infra.Dapper.Interfaces;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorRepository(IConfiguration configuration, IUnitOfWork unitOfWork) : base(configuration, unitOfWork)
        {
        }

        public override void Alterar(Fornecedor entidade)
        {
            _fornecedorRepository.Alterar(entidade);
        }

        public override void Cadastrar(Fornecedor entidade)
        {
            _fornecedorRepository.Cadastrar(entidade);
        }

        public override void Deletar(int id)
        {
            _fornecedorRepository.Deletar(id);
        }

        public override Fornecedor ObterPorId(int id)
        {
            return _fornecedorRepository.ObterPorId(id);
        }

        public override IList<Fornecedor> ObterTodos()
        {
            return _fornecedorRepository.ObterTodos();
        }
    }
}
