using Domain.Entities;
using Domain.Services;
using Domain.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IServiceManager _serviceManager;
        public AuthController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _serviceManager.AuthService.AuthenticateAsync(model);


            if (response == null)
            {
                return BadRequest(new { message = "Usuario o contraseña incorrecto" });
            }

            return Ok(response);
        }


        [HttpPost("renew")]
        public async Task<IActionResult> Renew([FromBody] string refreshToken)
        {
            try
            {
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest(new { message = "El token de actualización no puede estar vacío" });
                }

                var refreshTokenResponse = await _serviceManager.AuthService.RefreshTokenAsync(refreshToken, CancellationToken.None);

                if (refreshTokenResponse == null)
                {
                    return BadRequest(new { message = "Error al renovar el token" });
                }

                return Ok(refreshTokenResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud" });
            }
        }
    }
}