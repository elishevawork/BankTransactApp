using AutoMapper;
using TransactionsAPI.Data.Entities;
using TransactionsAPI.Models.DTOs.Order;

namespace TransactionsAPI.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<OrderCreateDto, Order>()
                .ForMember(x => x.OrderType, g => g.MapFrom(src => src.OrderType));          
            
            CreateMap<Order, OrderReadDto>()
                .ForMember(x => x.OrderType, g => g.MapFrom(src => src.OrderType))
                .ForMember(x => x.Status, g => g.MapFrom(src => src.Status));

            CreateMap<OrderUpdateDto, Order>();                

        }
    }
}
