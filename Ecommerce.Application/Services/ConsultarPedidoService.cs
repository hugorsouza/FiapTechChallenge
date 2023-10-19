using AutoMapper;
using Ecommerce.Application.DTO;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Services
{
    public class ConsultarPedidoService : IConsultarPedidoService
    {
        private readonly IConsultarPedidoDapperService _consultarPedidoDapperService;
        private readonly IMapper _mapper = AutoMapper.AutoMapperConfig.Mapper;
        public ConsultarPedidoService(IConsultarPedidoDapperService consultarPedidoDomainService)
        {
            _consultarPedidoDapperService = consultarPedidoDomainService;
        }

        public ConsultarPedidoEntity ConsultarPedido(string usuario)
        {
            var consultaPedido = _mapper.Map<ConsultarPedidoEntity>(_consultarPedidoDapperService.ConsultarPedidoDomain(usuario));

            return consultaPedido;
        }
    }
}
