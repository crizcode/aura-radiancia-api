using Infraestructure.Shared;

namespace Domain.Services.Abstractions
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RoleDto> GetByIdAsync(int roleId, CancellationToken cancellationToken = default);
        Task<RoleDto> CreateAsync(RoleDto roleForCreationDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int roletId, RoleDto roleForUpdateDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int roleId, CancellationToken cancellationToken = default);
    }
}