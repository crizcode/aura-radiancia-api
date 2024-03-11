using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace WebAPI.Controllers
{
    [Route("api/v1/supplier")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public SupplierController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // List Suppliers
        [HttpGet("list")]
        public async Task<IActionResult> GetSuppliers(CancellationToken cancellationToken)
        {
            var Suppliers = await _serviceManager.SupplierService.GetAllAsync(cancellationToken);
            return Ok(Suppliers);
        }


        // List Suppliers by Id

        [HttpGet("list/{supplierId}")]
        public async Task<IActionResult> GetSupplierById(int supplierId, CancellationToken cancellationToken)
        {
            var supplierDto = await _serviceManager.SupplierService.GetByIdAsync(supplierId, cancellationToken);
            return Ok(supplierDto);
        }

        // Add Supplier
        [HttpPost("save")]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierDto SupplierForCreationDto)
        {
            try
            {
                // Crear el Categoria utilizando el servicio
                await _serviceManager.SupplierService.CreateAsync(SupplierForCreationDto);

                // Devolver un mensaje de éxito
                return Ok(new { message = "Proveedor creado exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al crear el proveedor" });
            }
        }

        // Update Supplier
        [HttpPut("update/{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] SupplierDto SupplierForUpdateDto, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.SupplierService.UpdateAsync(supplierId, SupplierForUpdateDto, cancellationToken);

                // Devolver un mensaje de éxito
                return Ok(new { message = "Proveedor actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al actualizar el proveedor" });
            }
        }


        // Delete Supplier
        [HttpDelete("delete/{supplierId}")] 
        public async Task<IActionResult> DeleteSupplier(int supplierId, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceManager.SupplierService.DeleteAsync(supplierId, cancellationToken);

                // Devolver un mensaje de éxito
                return Ok(new { message = "Proveedor eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, new { error = "Error al eliminar el proveedor" });
            }
        }
    }


}