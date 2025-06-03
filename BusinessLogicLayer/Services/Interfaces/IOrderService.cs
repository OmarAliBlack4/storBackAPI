using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAPI.BusinessLogicLayer.DTOs.OrderDTOs;

namespace ProjectAPI.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrderAsync(OrderRequestDTO orderRequestDTO, string userEmail);
        Task<OrderDTO> GetOrderById(Guid id);
        Task<IEnumerable<OrderDTO>> GetOrderByEmail(string email);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task DeleteOrderAsync(Guid id); // دالة جديدة لحذف الطلب
    }
}