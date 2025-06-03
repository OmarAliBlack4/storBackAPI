using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectAPI.BusinessLogicLayer.DTOs.ProductDTOs;
using ProjectAPI.DataAccessLayer.Data.Models;

namespace ProjectAPI.BusinessLogicLayer.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {

            CreateMap<Product, ProductReadDto>();

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); 

            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        

        }
    }
}
