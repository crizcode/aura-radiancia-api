using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public SupplierController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // List Suppliers
        [HttpGet]
        public async Task<IActionResult> GetSuppliers(CancellationToken cancellationToken)
        {
            var Suppliers = await _serviceManager.SupplierService.GetAllAsync(cancellationToken);
            return Ok(Suppliers);
        }


        // List Suppliers by Id

        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSupplierById(int supplierId, CancellationToken cancellationToken)
        {
            var supplierDto = await _serviceManager.SupplierService.GetByIdAsync(supplierId, cancellationToken);
            return Ok(supplierDto);
        }

        // Add Supplier
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierDto SupplierForCreationDto)
        {
            try
            {
                // Crear el Suppliero utilizando el servicio
                await _serviceManager.SupplierService.CreateAsync(SupplierForCreationDto);

                // Devolver un mensaje de éxito
                return Ok("Proeevedor creado exitosamente.");
            }
            catch (Exception ex)
            {
                // Devolver un mensaje de error en caso de excepción
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el proveedor: " + ex.Message);
            }
        }


        // Update Supplier
        [HttpPut("{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] SupplierDto SupplierForUpdateDto, CancellationToken cancellationToken)
        {
            await _serviceManager.SupplierService.UpdateAsync(supplierId, SupplierForUpdateDto, cancellationToken);
            return Ok("Proveedor actualizado exitosamente.");
        }


        // Delete Supplier
        [HttpDelete("{supplierId}")] 
        public async Task<IActionResult> DeleteSupplier(int supplierId, CancellationToken cancellationToken)
        {
            await _serviceManager.SupplierService.DeleteAsync(supplierId, cancellationToken);

            // Devolver un mensaje de éxito
            return Ok("Proveedor eliminado exitosamente.");
        }
    }


}