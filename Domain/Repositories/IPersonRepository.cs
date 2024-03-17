using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPersonRepository
    {


        Task<Person> SearchPersonByCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default);

        Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Person> GetByIdAsync(int personId, CancellationToken cancellationToken = default);
        Task AddAsync(Person person, CancellationToken cancellationToken = default);
        Task UpdateAsync(Person person, CancellationToken cancellationToken = default);
        Task RemoveAsync(Person person, CancellationToken cancellationToken = default);

    }
}