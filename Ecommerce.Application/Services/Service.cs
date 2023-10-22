using Ecommerce.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public abstract class Service<T> : IService<T> where T : class
    {
        public abstract void Alterar(T entidade);
        public abstract void Cadastrar(T entidade);
        public abstract void Deletar(int id);    
        public abstract T ObterPorId(int id);      
        public abstract IList<T> ObterTodos();      
        
    }
}
