using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Abstractions
{
    public interface IProductService
    {
    Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductDto> GetByIdAsync(int prductrId, CancellationToken cancellationToken = default);
    Task<ProductDto> CreateAsync(ProductDto productForCreationDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int productId, ProductDto productForUpdateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int productId, CancellationToken cancellationToken = default);
    }
}