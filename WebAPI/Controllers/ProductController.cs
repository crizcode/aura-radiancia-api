using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ProductController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // List Products
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _serviceManager.ProductService.GetAllAsync(cancellationToken);
            return Ok(products);
        }


        // List Products by Id

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId, CancellationToken cancellationToken)
        {
            var productDto = await _serviceManager.ProductService.GetByIdAsync(productId, cancellationToken);
            return Ok(productDto);
        }

        // Add Product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productForCreationDto)
        {
            try
            {
                // Crear el producto utilizando el servicio
                await _serviceManager.ProductService.CreateAsync(productForCreationDto);

                // Devolver un mensaje de éxito
                return Ok("Producto creado exitosamente.");
            }
            catch (Exception ex)
            {
                // Devolver un mensaje de error en caso de excepción
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto: " + ex.Message);
            }
        }


        // Update Product
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDto productForUpdateDto, CancellationToken cancellationToken)
        {
            await _serviceManager.ProductService.UpdateAsync(productId, productForUpdateDto, cancellationToken);
            return NoContent();
        }


        // Delete Product
        [HttpDelete("{productId}")] 
        public async Task<IActionResult> DeleteProduct(int productId, CancellationToken cancellationToken)
        {
            await _serviceManager.ProductService.DeleteAsync(productId, cancellationToken);

            // Devolver un mensaje de éxito
            return Ok("Producto eliminado exitosamente.");
        }
    }


}