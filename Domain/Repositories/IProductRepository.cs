using Domain.Entities;

namespace Domain.Repositories

{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(int productId, CancellationToken cancellationToken = default);
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task RemoveAsync(Product product, CancellationToken cancellationToken = default);
    }
}