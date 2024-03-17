using Domain.Helpers;
using Domain.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
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

        // Se obtiene el token del encabezado de autorización
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null)
        {
            // No se proporcionó ningún token en el encabezado de autorización
            await HandleUnauthorizedRequest(context);
            return;
        }

        try
        {
            // Se adjunta el usuario al contexto
            await AttachPersonToContextAsync(context, userService, token);

            // Se llama al siguiente middleware en la cadena
            await _next(context);
        }
        catch (Exception ex)
        {
            // Manejar el error adecuadamente, como registrar o lanzar una excepción
            Console.WriteLine($"Error attaching user to context: {ex.Message}");
            await HandleUnauthorizedRequest(context);
            // No se llama a _next(context) aquí para evitar continuar con el siguiente middleware
        }
    }

    private async Task AttachPersonToContextAsync(HttpContext context, IPersonService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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
            throw; // Propagar la excepción para manejo posterior si es necesario
        }
    }

    private async Task HandleUnauthorizedRequest(HttpContext context)
    {
        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync("El encabezado de autorización es requerido.");
    }
}
