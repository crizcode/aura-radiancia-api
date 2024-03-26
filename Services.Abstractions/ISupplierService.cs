using Infraestructure.Shared;

namespace  Core.Services.Abstractions
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SupplierDto> GetByIdAsync(int supplierId, CancellationToken cancellationToken = default);
        Task<SupplierDto> CreateAsync(SupplierDto supplierForCreationDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int suppliertId, SupplierDto supplierForUpdateDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int supplierId, CancellationToken cancellationToken = default);
    }
}