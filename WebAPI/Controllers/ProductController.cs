using Domain.Services.Abstractions;
using Infraestructure.Shared;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var products = await _serviceManager.ProductService.GetAllAsync(cancellationToken);
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al listar los productos" + ex.Message });
            }
        }


        // List Products by Id

        [HttpGet("list/{productId}")]
        public async Task<IActionResult> GetProductById(int productId, CancellationToken cancellationToken)
        {

            try
            {
                var productDto = await _serviceManager.ProductService.GetByIdAsync(productId, cancellationToken);
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al listar el producto" + ex.Message });
            }
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
                return Ok(new { message = "Producto creado exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al crear el producto " + ex.Message });
            }
        }



        // Update Product
        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDto ProductForUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.ProductService.UpdateAsync(productId, ProductForUpdateDto, cancellationToken);
       
                // Devolver un mensaje de éxito
                return Ok(new { message = "Producto actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al actualizar el producto " + ex.Message });
            }
        }

        // Delete Product
        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.ProductService.DeleteAsync(productId, cancellationToken);

                // Devolver un mensaje de éxito
                return Ok(new { message = "Producto eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al eliminar el producto " + ex.Message });
            }
        }


    }
}