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

        // List Categorys
        [HttpGet("list")]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var Categories = await _serviceManager.CategoryService.GetAllAsync(cancellationToken);
            return Ok(Categories);
        }


        // List Categorys by Id

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
                // Crear el Categoryo utilizando el servicio
                await _serviceManager.CategoryService.CreateAsync(CategoryForCreationDto);

                // Devolver un mensaje de éxito
                return Ok("Categoria creada exitosamente.");
            }
            catch (Exception ex)
            {
                // Devolver un mensaje de error en caso de excepción
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear : " + ex.Message);
            }
        }


        // Update Category
        [HttpPut("update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto CategoryForUpdateDto, CancellationToken cancellationToken)
        {
            await _serviceManager.CategoryService.UpdateAsync(categoryId, CategoryForUpdateDto, cancellationToken);
            return Ok("Cateogoria actualizada.");
        }


        // Delete Category
        [HttpDelete("delete/{categoryId}")] 
        public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken cancellationToken)
        {
            await _serviceManager.CategoryService.DeleteAsync(categoryId, cancellationToken);

            // Devolver un mensaje de éxito
            return Ok(categoryId);
        }
    }


}