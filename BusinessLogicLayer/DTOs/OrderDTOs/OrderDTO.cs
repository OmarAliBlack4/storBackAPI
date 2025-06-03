using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAPI.DataAccessLayer.Data.Models;

namespace ProjectAPI.BusinessLogicLayer.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string userPhone { get; set; }
        public string userAddress { get; set; }
        public ICollection<OrderItemDTO> orderItems { get; set; }
    }
}
