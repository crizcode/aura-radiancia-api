using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace WebAPI.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public CategoryController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // List Categories
        [HttpGet("list")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var Categories = await _serviceManager.CategoryService.GetAllAsync(cancellationToken);
            return Ok(Categories);
        }


        // List Category by Id

        [HttpGet("list/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        {
            var CategoryDto = await _serviceManager.CategoryService.GetByIdAsync(categoryId, cancellationToken);
            return Ok(CategoryDto);
        }

        // Add Category
        [HttpPost("save")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto CategoryForCreationDto)
        {
            try
            {
                // Crear el Categoria utilizando el servicio
                await _serviceManager.CategoryService.CreateAsync(CategoryForCreationDto);

                // Devolver un mensaje de éxito
                return Ok(new { message = "Categoria creado exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al crear el producto" });
            }
        }



        // Update Category
        [HttpPut("update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto CategoryForUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.CategoryService.UpdateAsync(categoryId, CategoryForUpdateDto, cancellationToken);

                // Devolver un mensaje de éxito
                return Ok(new { message = "Categoria actualizada exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al actualizar el categoria" });
            }
        }


        // Delete Category
        [HttpDelete("delete/{categoryId}")] 
        public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.CategoryService.DeleteAsync(categoryId, cancellationToken);

                // Devolver un mensaje de éxito
                return Ok(new { message = "Categoria eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al eliminar el categoria" });
            }
        }
    }


}