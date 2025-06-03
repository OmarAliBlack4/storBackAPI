using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.DataAccessLayer.Data.Models
{
    public class Basket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> BasketItem { get; set; }
    }
}
