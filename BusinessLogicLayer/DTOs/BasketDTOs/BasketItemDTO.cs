using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.BusinessLogicLayer.DTOs.BasketDTOs
{
    public class BasketItemDTO
    {
        public int id { get; init; }
        public string Name { get; init; }
        public int Price { get; init; }
        public string? ImageUrl { get; init; }
        public int? StockQuantity { get; init; }
        public string? Description { get; set; }
        public int Quantity { get; init; } 
    }
}
