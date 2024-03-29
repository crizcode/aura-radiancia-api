using Dapper;
using Core.Domain.Entities;
using System.Data;
using System.Security.Authentication;
using Tools;

namespace Infrastructure.Persistence
{
    public class AuthRepository : Core.Domain.Interfaces.IAuthRepository
    {
        private readonly IDbConnection _connection;


        public AuthRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Person> SearchPersonByCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            var parameters = new { UserName = userName, Password = Encryptor.Encrypt(password) };


            var result = await _connection.QueryFirstOrDefaultAsync<Person>("usp_selectPerson", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }


        public async Task<RefreshTokenResponse> SearchTokensAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            try
            {
                string query = "usp_validateRefreshToken";

                var refreshTokenResult = await _connection.QueryFirstOrDefaultAsync<RefreshTokenResponse>(query, new { @refreshToken = refreshToken }, commandType: CommandType.StoredProcedure);

                if (refreshTokenResult == null)
                {
                    throw new AuthenticationException("Refresh token not found");
                }

                // Devolver el resultado
                return refreshTokenResult;
            }
            catch (Exception ex)
            {
                throw new AuthenticationException("Error searching refresh token", ex);
            }
        }

        public async Task InsertRefreshTokenAsync(RefreshTokenResponse token, CancellationToken cancellationToken = default)
        {
            var parameters = new
            {
                @UserId = token.UserId,
                @Token = token.RefreshToken,
                @FechaExp = token.FechaExp,
            };

            await _connection.ExecuteAsync("usp_insertRefreshToken", parameters, commandType: CommandType.StoredProcedure);
        }

      
    }
}

