<<<<<<< HEAD
﻿using Ecommerce.Application.DTO;
=======
﻿using Ecommerce.Application.Model;
>>>>>>> b71a3fe (Dapper)
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Services
{
    public class FazerPedidoService : IFazerPedidoService
    {
        private readonly IFazerPedidoDomainService _fazerPedidoDomainService;
        public FazerPedidoService()
        {
        }
        public FazerPedidoDTO FazerPedido(FazerPedidoDTO fazerPedidoDTO)
        {
<<<<<<< HEAD
            //var fazerPedido = _fazerPedidoService.FazerPedido(fazerPedidoDTO); 
            return fazerPedidoDTO;
=======
            var pedido = new FazerPedidoEntity
            {
                Id = 999,
            };
            
            return fazerPedidoModel;
>>>>>>> b71a3fe (Dapper)
        }
    }
}
