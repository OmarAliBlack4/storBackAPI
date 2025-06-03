using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.BusinessLogicLayer.DTOs.BasketDTOs;
using ProjectAPI.DataAccessLayer.Data.Models;
using ProjectAPI.DataAccessLayer.Repositories;

namespace ProjectAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("GetBasket/{id}")]
        public async Task<ActionResult<BasketDTO>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket);
        }
        [HttpPost("AddOrUpdateBasket")]
        public async Task<ActionResult<BasketDTO>> UpdateBasket(BasketDTO basketDTO)
        {
            var basketModel = _mapper.Map<Basket>(basketDTO);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basketModel);
            if (updatedBasket == null) return BadRequest("Failed to update basket");
            return Ok(_mapper.Map<BasketDTO>(updatedBasket));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
            return NoContent();
        }
        
    }
}
