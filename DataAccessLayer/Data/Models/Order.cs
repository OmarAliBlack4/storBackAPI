using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.DataAccessLayer.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string userPhone { get; set; }
        public string userAddress { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }
    }
}
    