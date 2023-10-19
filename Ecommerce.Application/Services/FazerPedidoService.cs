
﻿using Ecommerce.Application.Model;
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

        public FazerPedidoModel FazerPedido(FazerPedidoModel fazerPedidoModel)
        {
            var pedido = new FazerPedidoEntity
            {
                Id = 999,
            };
            return fazerPedidoModel;
        }
    }
}
