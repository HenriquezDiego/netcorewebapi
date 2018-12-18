using AutoMapper;
using Netcorewebapi.Controllers;
using Netcorewebapi.DataAccess.Data.Entities;
using Netcorewebapi.ViewModels;

namespace Netcorewebapi.Data
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
