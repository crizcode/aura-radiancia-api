using Core.Services.Abstractions;
using Infraestructure.Shared;
using Microsoft.AspNetCore.Mvc;

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

        try
            {
                var Categories = await _serviceManager.CategoryService.GetAllAsync(cancellationToken);
                return Ok(Categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al listar la categoria " + ex.Message });
            }
        }


        // List Category by Id

        [HttpGet("list/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        {
  
        try
            {
                var CategoryDto = await _serviceManager.CategoryService.GetByIdAsync(categoryId, cancellationToken);
            return Ok(CategoryDto);
    }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al listar la categoria " + ex.Message
});
            }
        }


        // Add Category
        [HttpPost("save")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto CategoryForCreationDto)
        {
            try
            {
                await _serviceManager.CategoryService.CreateAsync(CategoryForCreationDto);
                return Ok(new { message = "Categoria creado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al crear la categoria " + ex.Message });
            }
        }


        // Update Category
        [HttpPut("update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto CategoryForUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.CategoryService.UpdateAsync(categoryId, CategoryForUpdateDto, cancellationToken);
                return Ok(new { message = "Categoria actualizada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al actualizar la categoria " + ex.Message });
            }
        }


        // Delete Category
        [HttpDelete("delete/{categoryId}")] 
        public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.CategoryService.DeleteAsync(categoryId, cancellationToken);
                return Ok(new { message = "Categoria eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al eliminar la categoria " + ex.Message });
            }
        }
    }


}