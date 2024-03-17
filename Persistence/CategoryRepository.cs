using Dapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using System.Data;

namespace Infrastructure.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection _connection;


        public CategoryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // List categorys 
        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _connection.QueryAsync<Category>(
                "usp_listCategories", commandType: CommandType.StoredProcedure
            );

            return categories;
        }

        // List Category By Id

        public async Task<Category> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            string query = "usp_selectCategoryById";

            // Ejecutar el procedimiento almacenado para obtener la categoria por su ID
            Category? category = await _connection.QueryFirstOrDefaultAsync<Category>(
                query, new { @CategoryId = categoryId }, commandType: CommandType.StoredProcedure
            );

            // Verificar si la categoria fue encontrado
            if (category == null)
            {
                throw new CategoryNotFoundExceptions(categoryId);
            }

            return category;
        }


        public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            // Abrir la conexión
            _connection.Open();

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var query = "usp_insertCategory";
                    var parameters = new
                    {
                        @Name = category.Name,
                        @Estado = category.Estado,
                    };

                    await _connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();

                    Console.WriteLine($"Error crear el categoryo: {ex.Message}");

                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }


        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {

            _connection.Open();

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var query = "usp_updateCategory";
                    var parameters = new
                    {
                        @categoryId = category.CategoryId,
                        @Name = category.Name,
                        @Estado = category.Estado
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



        // Delete Category

        public async Task RemoveAsync(Category category, CancellationToken cancellationToken = default)
        {
            var query = "usp_deleteCategory";
            await _connection.ExecuteAsync(query, new { @categoryId = category.CategoryId }, commandType: CommandType.StoredProcedure);
        }


    }
}
