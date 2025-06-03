using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectAPI.BusinessLogicLayer.DTOs.OrderDTOs;
using ProjectAPI.DataAccessLayer.Data.Models;

namespace ProjectAPI.BusinessLogicLayer.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() 
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderRequestDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<OrderRequestDTO, Order>()
            .ForMember(dest => dest.orderItems, opt => opt.Ignore())
            .ReverseMap();
        }
    }
}
