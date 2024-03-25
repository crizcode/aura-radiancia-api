using System.Text.Json.Serialization;

namespace Domain.Entities;

public class RefreshTokenResponse
{
    [JsonIgnore]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string RefreshToken { get; set; }
    public string Token { get; set; }

    [JsonIgnore]
    public DateTime FechaExp { get; set; }
    [JsonIgnore]
    public DateTime FechaCrea { get; set; }
    [JsonIgnore]
    public bool IsActive { get; set; }


    public RefreshTokenResponse() 
    {
    }

    public RefreshTokenResponse(Person user, string refreshtoken, DateTime fechaExp)
        {
            UserId = user.Id;
            RefreshToken = refreshtoken;
            FechaExp = fechaExp;
        }


    public RefreshTokenResponse(Person user,string token, RefreshTokenResponse refreshToken)
    {
        UserId = user.Id;
        Token = token;
        RefreshToken = refreshToken.RefreshToken;
    }

    public RefreshTokenResponse(Person user, string token, string refreshToken){
        UserId = user.Id;
        Token = token;
        RefreshToken = refreshToken;
    }

}

 