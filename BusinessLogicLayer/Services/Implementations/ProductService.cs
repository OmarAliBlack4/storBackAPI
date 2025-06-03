using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using ProjectAPI.BusinessLogicLayer.DTOs.ProductDTOs;
using ProjectAPI.BusinessLogicLayer.Services.Interfaces;
using ProjectAPI.DataAccessLayer.Data.Models;
using ProjectAPI.DataAccessLayer.UnitOfWorks;

namespace ProjectAPI.BusinessLogicLayer.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ProductReadDto> CreateProductAsync(ProductCreateDto productCreateDto, IFormFile formFile)
        {

            if (productCreateDto == null)
                throw new ArgumentNullException(nameof(productCreateDto), "No product Found , Please Add product");

            var productImageFolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\prducts");
            if (!Directory.Exists(productImageFolder))
                Directory.CreateDirectory(productImageFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            var filePath = Path.Combine(productImageFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var product = _mapper.Map<Product>(productCreateDto);
            product.ImageUrl = Path.Combine("files", "product", fileName);

            var productRepository = _unitOfWork.GetRepository<Product,int>();
            await productRepository.AddAsync(product);
            await _unitOfWork.SaveCheangesAsync();
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var productRepository = _unitOfWork.GetRepository<Product ,int>();
            var product = await productRepository.GetAsync(id);

            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product not found");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\prducts", product.ImageUrl);

            if(File.Exists(filePath))
                File.Delete(filePath);

            productRepository.Delete(product);
            await _unitOfWork.SaveCheangesAsync();

        }

        public async Task<IEnumerable<ProductReadDto>> GetAllProduuctsAsync()
        {
            var productRepository =  _unitOfWork.GetRepository<Product , int>();
            var produucts = await productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductReadDto>>(produucts);
        }

        public async Task<ProductReadDto> GetProductByIdAsync(int id)
        {
            var productRepository = _unitOfWork.GetRepository<Product, int>();
            var produuct = await productRepository.GetAsync(id);
            return _mapper.Map<ProductReadDto>(produuct);
        }

        public async Task<ProductReadDto> UpdateProductAsync(int id, ProductUpdateDto productUpdateDto, IFormFile formFile)
        {
            var productRepository = _unitOfWork.GetRepository<Product, int>();
            var existingProduct = await productRepository.GetAsync(id);

            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            if (formFile != null && formFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",existingProduct.ImageUrl.Replace('/', Path.DirectorySeparatorChar));

                    if (File.Exists(oldImagePath))
                        File.Delete(oldImagePath);
                }

                var productImageFolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\products");
                if (!Directory.Exists(productImageFolder))
                    Directory.CreateDirectory(productImageFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                var filePath = Path.Combine(productImageFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                existingProduct.ImageUrl = Path.Combine("images", "products", fileName);
            }

            _mapper.Map(productUpdateDto, existingProduct);
            productRepository.Update(existingProduct);
            await _unitOfWork.SaveCheangesAsync();
            return _mapper.Map<ProductReadDto>(existingProduct);
        }

    }
}
