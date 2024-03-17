using Infraestructure.Shared;

namespace Domain.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryDto> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<CategoryDto> CreateAsync(CategoryDto categoryForCreationDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int categorytId, CategoryDto categoryForUpdateDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int categoryId, CancellationToken cancellationToken = default);
    }
}