using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.BusinessLogicLayer.DTOs.OrderDTOs;
using ProjectAPI.BusinessLogicLayer.Services.Interfaces;
using ProjectAPI.DataAccessLayer.Data.Models;
using ProjectAPI.DataAccessLayer.Repositories;
using ProjectAPI.DataAccessLayer.UnitOfWorks;

namespace ProjectAPI.BusinessLogicLayer.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _basketRepository = basketRepository;
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderRequestDTO orderRequestDTO, string userEmail)
        {
            var basket = await _basketRepository.GetBasketAsync(orderRequestDTO.BasketID);
            if (basket == null)
            {
                throw new KeyNotFoundException($"Basket with ID {orderRequestDTO.BasketID} not found");
            }

            foreach (var item in basket.BasketItem)
            {
                if (item.Quantity > item.StockQuantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for item {item.Name}");
                }
            }

            var order = _mapper.Map<Order>(orderRequestDTO);
            order.Id = Guid.NewGuid();
            order.UserEmail = userEmail;
            order.orderItems = basket.BasketItem.Select(item => new OrderItem
            {
                OrderId = order.Id,
                ProductId = item.id,
                Name = item.Name,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
                StockQuantity = item.StockQuantity,
                Description = item.Description,
                Quantity = item.Quantity
            }).ToList();

            var orderRepository = _unitOfWork.GetRepository<Order, Guid>();
            await orderRepository.AddAsync(order);
            await _unitOfWork.SaveCheangesAsync();

            await _basketRepository.DeleteBasketAsync(orderRequestDTO.BasketID);

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> GetOrderById(Guid id)
        {
            var orderRepository = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepository.GetQueryable()
                .Include(o => o.orderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found");
            }

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrderByEmail(string email)
        {
            var orderRepository = _unitOfWork.GetRepository<Order, Guid>();
            var orders = await orderRepository.GetQueryable()
                .Where(o => o.UserEmail == email)
                .Include(o => o.orderItems)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orderRepository = _unitOfWork.GetRepository<Order, Guid>();
            var orders = await orderRepository.GetQueryable()
                .Include(o => o.orderItems)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var orderRepository = _unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepository.GetQueryable()
                .Include(o => o.orderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found");
            }

            orderRepository.Delete(order);
            await _unitOfWork.SaveCheangesAsync();
        }
    }
}