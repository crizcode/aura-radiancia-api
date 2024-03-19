using Dapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using System.Data;

namespace Infrastructure.Persistence
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbConnection _connection;
        public RoleRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        // List roles
        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var roles = await _connection.QueryAsync<Role>(
                "usp_listRoles", commandType: CommandType.StoredProcedure
            );

            return roles;
        }

        // List Role By Id

        public async Task<Role> GetByIdAsync(int roleId, CancellationToken cancellationToken = default)
        {
            string query = "usp_selectRoleById";

            // Ejecutar el procedimiento almacenado para obtener la rolea por su ID
            Role? role = await _connection.QueryFirstOrDefaultAsync<Role>(
                query, new { @Id = roleId }, commandType: CommandType.StoredProcedure
            );

            // Verificar si la rolea fue encontrado
            if (role == null)
            {
                throw new RoleNotFoundExceptions(roleId);
            }

            return role;
        }

        public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
        {
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {

                    var query = "usp_insertRole";
                    var parameters = new
                    {
                        @RoleName = role.RoleName,
                    };

                    await _connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();

                    Console.WriteLine($"Error crear el rol: {ex.Message}");

                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public async Task UpdateAsync(Role role, CancellationToken cancellationToken = default)
        {
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var query = "usp_updateRole";
                    var parameters = new
                    {
                        @RoleName = role.RoleName,
                        @Id = role.Id

                    };

                    await _connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log error
                    Console.WriteLine($"Error de actualización: {ex.Message}");
                    throw;
                }
                finally
                {

                    _connection.Close();
                }
            }
        }

        // Delete Role
        public async Task RemoveAsync(Role role, CancellationToken cancellationToken = default)
        {
            var query = "usp_deleteRole";
            await _connection.ExecuteAsync(query, new { @Id = role.Id }, commandType: CommandType.StoredProcedure);
        }

    }
}
