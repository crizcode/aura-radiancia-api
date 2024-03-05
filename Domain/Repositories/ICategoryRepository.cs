using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Category> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default);
        Task AddAsync(Category category, CancellationToken cancellationToken = default);
        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);
        Task RemoveAsync(Category category, CancellationToken cancellationToken = default);

    }
}