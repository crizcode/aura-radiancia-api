﻿using Domain.Entities;

namespace Domain.Repositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Supplier> GetByIdAsync(int supplierId, CancellationToken cancellationToken = default);
        Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default);
        Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken = default);
        Task RemoveAsync(Supplier supplier, CancellationToken cancellationToken = default);

    }
}