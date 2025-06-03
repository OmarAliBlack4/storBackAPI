using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjectAPI.BusinessLogicLayer.DTOs.ProductDTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        //public IFormFile? ImageUrl { get; set; }
        public int? StockQuantity { get; set; }
    }
}
