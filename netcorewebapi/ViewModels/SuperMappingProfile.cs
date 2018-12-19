using AutoMapper;
using Netcorewebapi.DataAccess.Data.Entities;

namespace Netcorewebapi.Api.ViewModels
{
    public class SuperMappingProfile : Profile
    {
        public SuperMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o=> o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();

            CreateMap<Product, ProductViewModel>()
                .ReverseMap();
        }
    }
}
