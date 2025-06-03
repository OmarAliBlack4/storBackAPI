using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.BusinessLogicLayer.DTOs.OrderDTOs
{
    public class OrderItemDTO
    {
        public int id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? ImageUrl { get; set; }
        public int? StockQuantity { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
