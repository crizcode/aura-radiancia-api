﻿using  Core.Domain.Entities;

namespace  Core.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Role> GetByIdAsync(int personId, CancellationToken cancellationToken = default);
        Task AddAsync(Role person, CancellationToken cancellationToken = default);
        Task UpdateAsync(Role person, CancellationToken cancellationToken = default);
        Task RemoveAsync(Role person, CancellationToken cancellationToken = default);

    }
}