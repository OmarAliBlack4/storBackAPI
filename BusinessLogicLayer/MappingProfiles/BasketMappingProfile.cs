using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectAPI.BusinessLogicLayer.DTOs.BasketDTOs;
using ProjectAPI.DataAccessLayer.Data.Models;

namespace ProjectAPI.BusinessLogicLayer.MappingProfiles
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile() 
        {

            CreateMap<Basket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();

        }
    }
}
