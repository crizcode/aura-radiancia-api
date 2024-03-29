using Core.Helpers;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly AppSettings _appSettings;
    private readonly string _jwtSecret;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        // _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));

        // Acceder al valor de la variable de entorno JWT_SECRET
        _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
    }

    public async Task Invoke(HttpContext context, IPersonService userService)
    {
        var path = context.Request.Path;

        // Verifica si la solicitud es para autenticar y genera tokens
        if (path.HasValue && (path.Value.EndsWith("/authenticate") || path.Value.EndsWith("/authorize")))
        {
            await _next(context);
            return;
        }

        // Verificar si la solicitud es para renovar el token
        if (path.HasValue && path.Value.EndsWith("/api/v1/auth/renew"))
        {

            // Después de renovar el token, continuar con el siguiente middleware en la cadena
            await _next(context);
            return;
        }


        // Verifica si la solicitud es para crear una persona y permite el acceso sin autenticación
        if (path.HasValue && path.Value.EndsWith("/api/v1/person/save"))
        {
            // Permitir acceso sin autenticación al método de creación de persona
            await _next(context);
            return;
        }

        // Se obtiene el token del encabezado de autorización
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            await HandleUnauthorizedRequest(context);
            return;
        }

        try
        {
            // Se adjunta el usuario al contexto
            await AttachPersonToContextAsync(context, userService, token);



            // Llamar al siguiente middleware en la cadena
            await _next(context);
        }
        catch (Exception ex)
        {
            // Manejar el error adecuadamente, como registrar o lanzar una excepción
            Console.WriteLine($"Error attaching user to context: {ex.Message}");
            await HandleUnauthorizedRequest(context);
        }
    }

  
    private async Task AttachPersonToContextAsync(HttpContext context, IPersonService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret); // Secreto JWT del archivo de config de C#
            var key = Encoding.ASCII.GetBytes(_jwtSecret); // Secreto JWT de la variable de entorno
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            var roleId = int.Parse(jwtToken.Claims.First(x => x.Type == "role_id").Value);

            // Adjuntar usuario al contexto después de la validación exitosa del JWT
            context.Items["Person"] = userId;
            context.Items["RoleId"] = roleId;
        }
        catch (Exception ex)
        {
            // Manejar el error adecuadamente, como registrar o lanzar una excepción
            Console.WriteLine($"Error attaching user to context: {ex.Message}");
            await HandleUnauthorizedRequest(context);
            throw; 
        }
    }

    private async Task HandleUnauthorizedRequest(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"No autorizado: no tiene permiso para realizar esta operación.\"}");
    }

}
