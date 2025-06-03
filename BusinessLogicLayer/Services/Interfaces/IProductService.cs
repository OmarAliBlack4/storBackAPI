using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectAPI.BusinessLogicLayer.DTOs.ProductDTOs;

namespace ProjectAPI.BusinessLogicLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllProduuctsAsync();
        Task<ProductReadDto> GetProductByIdAsync(int id);
        Task<ProductReadDto> CreateProductAsync(ProductCreateDto productCreateDto , IFormFile formFile);
        Task<ProductReadDto> UpdateProductAsync(int id ,ProductUpdateDto productUpdateDto , IFormFile formFile);
        Task DeleteProductAsync(int id);
    }
}
