using Dapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using System.Data;
using Tools;

namespace Infrastructure.Persistence
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbConnection _connection;


        public PersonRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Person> SearchPersonByCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            var parameters = new { UserName = userName, Password = Encryptor.Encrypt(password) };


            var result = await _connection.QueryFirstOrDefaultAsync<Person>("usp_selectPerson", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }


        // List people
        public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var people = await _connection.QueryAsync<Person>(
                "usp_listPeople", commandType: CommandType.StoredProcedure
            );

            return people;
        }

        // List Person By Id

        public async Task<Person> GetByIdAsync(int personId, CancellationToken cancellationToken = default)
        {
            string query = "usp_selectPersonById";
            
            Person? person = await _connection.QueryFirstOrDefaultAsync<Person>(
                query, new { @Id = personId }, commandType: CommandType.StoredProcedure
            );

            // Verificar si la persona fue encontrado
            if (person == null)
            {
                throw new PersonNotFoundExceptions(personId);
            }

            return person;
        }

        public async Task AddAsync(Person person, CancellationToken cancellationToken = default)
        {
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    // Encriptar la contraseña
                    string contraseñaEncriptada = Encryptor.Encrypt(person.Pass);

                    var query = "usp_insertPerson";
                    var parameters = new
                    {
                        @UserName = person.UserName,
                        @Pass = contraseñaEncriptada,
                        @Nombre = person.Nombre,
                        @Apellido = person.Apellido,
                        @Email = person.Email,
                        @DepartmentId = person.DepartmentId,
                        @RoleId = person.RoleId
                    };

                    await _connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();

                    Console.WriteLine($"Error crear el persona: {ex.Message}");

                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public async Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
        {
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var query = "usp_updatePerson";
                    var parameters = new
                    {
                        @Id = person.Id,
                        @UserName = person.UserName,
                        @Pass = person.Pass,
                        @Nombre = person.Nombre,
                        @Apellido = person.Apellido,
                        @Email = person.Email,
                        @DepartmentId = person.DepartmentId,
                        @RoleId = person.RoleId

                    };

                    await _connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error de actualización: {ex.Message}");
                    throw;
                }
                finally
                {

                    _connection.Close();
                }
            }
        }

        // Delete Person
        public async Task RemoveAsync(Person person, CancellationToken cancellationToken = default)
        {
            var query = "usp_deletePerson";
            await _connection.ExecuteAsync(query, new { @Id = person.Id }, commandType: CommandType.StoredProcedure);
        }

    }
}
