using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAPI.DataAccessLayer.Data.Models;

namespace ProjectAPI.BusinessLogicLayer.DTOs.BasketDTOs
{
    public class BasketDTO
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDTO> BasketItem { get; init; }
    }
}
