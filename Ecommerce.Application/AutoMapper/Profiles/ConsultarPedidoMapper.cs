

using AutoMapper;
using Ecommerce.Application.DTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.AutoMapper.Profiles
{
    public class ConsultarPedidoMapper : Profile
    {
        public ConsultarPedidoMapper() 
        {
            CreateMap<ConsultarPedidoEntity, ConsultarPedidoModel>().ReverseMap();
        }
    }
}
