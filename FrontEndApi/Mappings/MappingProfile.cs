using AutoMapper;
using OrderProcessingApi.Model;

namespace OrderProcessingApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderResponse, OrderDto>()
                .ForMember(dest => dest.CustomerName, source => source.MapFrom(source => source.CustomerName))
                .ForMember(dest => dest.ProductName, source => source.MapFrom(source => source.ProductName))
                .ForMember(dest => dest.OrderStatus, source => source.MapFrom(source => source.OrderStatus));
         }
    }
}
