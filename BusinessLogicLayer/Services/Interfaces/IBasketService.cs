using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectAPI.BusinessLogicLayer.DTOs.BasketDTOs;

namespace ProjectAPI.BusinessLogicLayer.Services.Interfaces
{
    public interface IBasketService
    {
        public Task<BasketDTO> GetBasketAsync(string id);
        public Task<BasketDTO?> UpdatedBasketAsync(BasketDTO basketDTO); 
        public Task<bool> DeleteBasketAsync(string id);
    }
}
