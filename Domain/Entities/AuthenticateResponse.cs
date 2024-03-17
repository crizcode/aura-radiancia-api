namespace Domain.Entities;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(Person user, string token)
        {
            Id = user.Id;
            FirstName = user.Nombre;
            LastName = user.Apellido;
            Username = user.UserName;
            Token = token;
        }
    }