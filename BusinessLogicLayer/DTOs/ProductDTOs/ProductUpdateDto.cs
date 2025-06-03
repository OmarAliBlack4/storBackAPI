using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.BusinessLogicLayer.DTOs.ProductDTOs
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int? StockQuantity { get; set; }
    }
}
