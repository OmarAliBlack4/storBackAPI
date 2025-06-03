using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.BusinessLogicLayer.DTOs.ProductDTOs;
using ProjectAPI.BusinessLogicLayer.Services.Interfaces;

namespace ProjectAPI.PresentationLayer.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // get all product
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProduuctsAsync();
            return Ok(products);
        }
        //Get Product By id
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // Create Product
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto productCreateDto, IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
                return BadRequest("Image file is required.");
            var product = await _productService.CreateProductAsync(productCreateDto, formFile);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        //Delete Product
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }

        //update Product
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateDto productUpdateDto, IFormFile? formFile)  
        {
            try
            {
                var existingProduct = await _productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                if (productUpdateDto == null && formFile == null)
                {
                    return BadRequest("No data provided for update");
                }

                //if (formFile != null && formFile.Length > 0)
                //{


                //}


                var updatedProduct = await _productService.UpdateProductAsync(id,productUpdateDto,formFile);

                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the product");
            }
        }
    }
}
