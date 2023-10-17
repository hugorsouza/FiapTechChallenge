using AutoMapper;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Services
{
    public class ConsultarPedidoService : IConsultarPedidoService
    {
        private readonly IConsultarPedidoDomainService _consultarPedidoDomainService;
        private readonly IMapper _mapper = AutoMapper.AutoMapperConfig.Mapper;
        public ConsultarPedidoService(IConsultarPedidoDomainService consultarPedidoDomainService)
        {
            _consultarPedidoDomainService = consultarPedidoDomainService;
        }

        public ConsultarPedidoEntity ConsultarPedido(string usuario)
        {
            var consultaPedido = _mapper.Map<ConsultarPedidoEntity>(_consultarPedidoDomainService.ConsultarPedidoDomain(usuario));

            return consultaPedido;
        }
    }
}
