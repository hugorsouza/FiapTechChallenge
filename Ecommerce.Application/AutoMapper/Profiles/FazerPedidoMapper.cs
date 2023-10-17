using AutoMapper;
using Ecommerce.Application.DTO;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.AutoMapper.Profiles
{
    public class FazerPedidoMapper : Profile
    {
        public FazerPedidoMapper() 
        {
            CreateMap<FazerPedidoEntity, FazerPedidoDTO>().ReverseMap();
        }
    }
}
