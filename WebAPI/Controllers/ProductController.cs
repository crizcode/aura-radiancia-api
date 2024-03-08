using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace WebAPI.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ProductController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // List Products
        [HttpGet("list")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var products = await _serviceManager.ProductService.GetAllAsync(cancellationToken);
            return Ok(products);
        }


        // List Products by Id

        [HttpGet("list/{ProductId}")]
        public async Task<IActionResult> GetProductById(int productId, CancellationToken cancellationToken)
        {
            var productDto = await _serviceManager.ProductService.GetByIdAsync(productId, cancellationToken);
            return Ok(productDto);
        }

        // Add Product
        [HttpPost("save")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto ProductForCreationDto)
        {
            try
            {
                // Crear el Producto utilizando el servicio
                await _serviceManager.ProductService.CreateAsync(ProductForCreationDto);

                // Devolver un mensaje de éxito
                return Ok("Producto creado exitosamente.");
            }
            catch (Exception ex)
            {
                // Devolver un mensaje de error en caso de excepción
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear : " + ex.Message);
            }
        }


        // Update Product
        [HttpPut("update/{ProductId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDto ProductForUpdateDto, CancellationToken cancellationToken)
        {
            await _serviceManager.ProductService.UpdateAsync(productId, ProductForUpdateDto, cancellationToken);
            return Ok("Producto actualizada.");
        }


        // Delete Product
        [HttpDelete("dalete/{ProductId}")]
        public async Task<IActionResult> DeleteProduct(int productId, CancellationToken cancellationToken)
        {
            await _serviceManager.ProductService.DeleteAsync(productId, cancellationToken);

            // Devolver un mensaje de éxito
            return Ok("Producto eliminado exitosamente.");
        }
    }


}