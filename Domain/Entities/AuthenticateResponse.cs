namespace Core.Domain.Entities;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Token { get; set; }

    public string RefreshToken { get; set; }

    public AuthenticateResponse(Person user, string token, RefreshTokenResponse refreshToken)
    {
        Id = user.Id;
        Token = token;
        RefreshToken = refreshToken.RefreshToken;
    }
}