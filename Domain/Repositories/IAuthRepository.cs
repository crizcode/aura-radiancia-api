using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<Person> SearchPersonByCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default);

        Task InsertRefreshTokenAsync(RefreshTokenResponse token, CancellationToken cancellationToken = default);

        Task<RefreshTokenResponse> SearchTokensAsync(string refreshToken, CancellationToken cancellationToken = default);


    }
}