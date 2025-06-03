using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectAPI.BusinessLogicLayer.DTOs.BasketDTOs;
using ProjectAPI.BusinessLogicLayer.Services.Interfaces;
using ProjectAPI.DataAccessLayer.Data.Models;
using ProjectAPI.DataAccessLayer.Repositories;

namespace ProjectAPI.BusinessLogicLayer.Services.Implementations
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
            => await basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDTO> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);

            return basket == null
                ? throw new KeyNotFoundException($"Basket with ID {id} not found")
                : mapper.Map<BasketDTO>(basket);

        }

        public async Task<BasketDTO?> UpdatedBasketAsync(BasketDTO basketDTO)
        {

            var custmerBasket = mapper.Map<Basket>(basketDTO);
            var updateBasket = await basketRepository.UpdateBasketAsync(custmerBasket);
            return updateBasket is null 
                ? throw new Exception("Can't Update now !!")
                : mapper.Map<BasketDTO>(updateBasket);
        }
    }
}
