using  Core.Domain.Entities;

namespace  Core.Services.Abstractions
{
    public interface IAuthService
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, CancellationToken cancellationToken = default);
        Task<RefreshTokenResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}

