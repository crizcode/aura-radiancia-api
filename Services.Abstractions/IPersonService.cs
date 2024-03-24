using Domain.Entities;
using Infraestructure.Shared;

namespace Domain.Services.Abstractions
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PersonDto> GetByIdAsync(int personId, CancellationToken cancellationToken = default);
        Task<PersonDto> CreateAsync(PersonDto personForCreationDto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int persontId, PersonDto personForUpdateDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int personId, CancellationToken cancellationToken = default);
    }
}