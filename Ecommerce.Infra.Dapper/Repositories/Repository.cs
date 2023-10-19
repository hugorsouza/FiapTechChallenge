using Ecommerce.Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Dapper.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;
        protected string ConnectionString => _connectionString;

        public Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionStrings:Ecommerce").Value;
        }

        public abstract void Alterar(T entidade);
        public abstract void Cadastrar(T entidade);
        public abstract void Deletar(int id);
        public abstract T ObterPorId(int id);
        public abstract IList<T> ObterTodos();

    }
}
