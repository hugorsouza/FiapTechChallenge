﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        void Cadastrar(T entidade);
        T ObterPorId(int id);
        IList<T> ObterTodos();
        void Alterar(T entidade);
        void Deletar(int id);
    }
}