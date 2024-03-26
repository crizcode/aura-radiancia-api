using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<Person> SearchPersonByCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default);

        Task InsertRefreshTokenAsync(RefreshTokenResponse token, CancellationToken cancellationToken = default);

        Task<RefreshTokenResponse> SearchTokensAsync(string refreshToken, CancellationToken cancellationToken = default);


    }
}