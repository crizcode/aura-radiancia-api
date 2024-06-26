﻿using Core.Domain.Entities;
using Core.Helpers;
using Core.Domain.Interfaces;
using Core.Services.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repositoryManager;
        // private readonly AppSettings _appSettings;
        private readonly string _jwtSecret;

        public AuthService(IRepositoryManager repositoryManager, IOptions<AppSettings> appSettings)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            // _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));

            // Acceder al valor de la variable de entorno JWT_SECRET
            _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, CancellationToken cancellationToken = default)
        {
            var user = await _repositoryManager.Auths.SearchPersonByCredentialsAsync(model.UserName, model.Pass, cancellationToken);

            if (user == null || user.Estado == "false")
                throw new AuthenticationException("Invalid username or password");

            var role = user.RoleId;

            // Generar token de acceso
            var accessToken = GenerateJwtToken(user, role);

            // Generar token de actualización (refresh token)
            var refreshToken = GenerateRefreshToken(user);

            // Guardar el token de actualización en la base de datos
            await InsertRefreshTokenAsync(refreshToken, cancellationToken);

            // Crear la respuesta combinada
            var response = new AuthenticateResponse(user, accessToken, refreshToken);

            return response;

        }


        private string GenerateJwtToken(Person user, int roleId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var key = Encoding.ASCII.GetBytes(_jwtSecret); // Secreto JWT de la variable de entorno


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("role_id", roleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static RefreshTokenResponse GenerateRefreshToken(Person user)
        {
            // Generar un token aleatorio
            var refreshToken = GenerateRandomToken();

            // Calcular la fecha de expiración 
            var expirationDate = DateTime.UtcNow.AddMinutes(8);
            // Convertir en horario local
            var localExpirationDate = TimeZoneInfo.ConvertTimeFromUtc(expirationDate, TimeZoneInfo.Local);
            // Almacenar refreshToken y expirationDate en la base de datos

            return new RefreshTokenResponse(user, refreshToken, localExpirationDate);
        }

        private static string GenerateRandomToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[32];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }


        private async Task InsertRefreshTokenAsync(RefreshTokenResponse token, CancellationToken cancellationToken = default)
        {
            await _repositoryManager.Auths.InsertRefreshTokenAsync(token, cancellationToken);
        }


        public async Task<RefreshTokenResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            // Buscar el token de actualización en la base de datos
            var existingToken = await _repositoryManager.Auths.SearchTokensAsync(refreshToken, cancellationToken);

            // Verificar si el token de actualización existe y está activo
            if (existingToken != null && existingToken.IsActive)
            {
                // Generar un nuevo token de acceso
                var user = await _repositoryManager.People.GetByIdAsync(existingToken.UserId, cancellationToken);
                var role = user.RoleId;
                var token = GenerateJwtToken(user, role);

                // Devolver el nuevo token de acceso encapsulado en un RefreshTokenResponse
                var response = new RefreshTokenResponse(user, token, refreshToken);

                return response;
            }
            else
            {
                // Si el token no existe o no está activo, generar ambos tokens y guardar el token de actualización nuevo
                var user = await _repositoryManager.People.GetByIdAsync(existingToken?.UserId ?? 0, cancellationToken);
                var role = user.RoleId;

                // Generar nuevos tokens
                var token = GenerateJwtToken(user, role);
                var newRefreshToken = GenerateRefreshToken(user);

                // Guardar el nuevo token de actualización en la base de datos
                await InsertRefreshTokenAsync(newRefreshToken, cancellationToken);

                // Crear la respuesta combinada
                var response = new RefreshTokenResponse(user,token, newRefreshToken);

                return response;
            }
        }
    }

}