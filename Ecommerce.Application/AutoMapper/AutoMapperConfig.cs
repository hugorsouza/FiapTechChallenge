using AutoMapper;
using Ecommerce.Application.AutoMapper.Profiles;

namespace Ecommerce.Application.AutoMapper
{
    public static class AutoMapperConfig
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get 
            {
                if(_mapper == null)
                {
                    var config = new MapperConfiguration(conf =>
                    {
                        conf.AddProfile<FazerPedidoMapper>();
                        conf.AddProfile<ConsultarPedidoMapper>();
                    });
                    _mapper = config.CreateMapper();
                }
                return _mapper;
            }
        }
    }
}
