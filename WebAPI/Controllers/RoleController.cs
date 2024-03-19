using Domain.Services.Abstractions;
using Infraestructure.Shared;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public RoleController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // List Roles
        [HttpGet("list")]
        public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
        {

        try
            {
                var Roles = await _serviceManager.RoleService.GetAllAsync(cancellationToken);
                return Ok(Roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al listar el rol" + ex.Message });
            }
        }


        // List Role by Id

        [HttpGet("list/{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId, CancellationToken cancellationToken)
        {
  
        try
            {
                var RoleDto = await _serviceManager.RoleService.GetByIdAsync(roleId, cancellationToken);
            return Ok(RoleDto);
    }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al listar el rol" + ex.Message
});
            }
        }


        // Add Role
        [HttpPost("save")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto RoleForCreationDto)
        {
            try
            {
                await _serviceManager.RoleService.CreateAsync(RoleForCreationDto);

                return Ok(new { message = "Rol creado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al crear el rol" + ex.Message });
            }
        }



        // Update Role
        [HttpPut("update/{roleId}")]
        public async Task<IActionResult> UpdateRole(int roleId, [FromBody] RoleDto RoleForUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.RoleService.UpdateAsync(roleId, RoleForUpdateDto, cancellationToken);

                return Ok(new { message = "Rol actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al actualizar el rol" + ex.Message });
            }
        }


        // Delete Role
        [HttpDelete("delete/{roleId}")] 
        public async Task<IActionResult> DeleteRole(int roleId, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.RoleService.DeleteAsync(roleId, cancellationToken);

                return Ok(new { message = "Rol eliminado exitosamente" });
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, new { error = "Error al eliminar el rol" + ex.Message });
            }
        }
    }


}