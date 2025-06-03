using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.DataAccessLayer.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; } // Primary key
        public Guid OrderId { get; set; } // Foreign key للـ Order
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? ImageUrl { get; set; }
        public int? StockQuantity { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; } = 1;
        public Order Order { get; set; } // Navigation property
    }
}
