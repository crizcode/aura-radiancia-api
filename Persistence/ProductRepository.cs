using Dapper;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System.Data;
using Core.Domain.Exceptions;

namespace Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        // List Products 
        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _connection.QueryAsync<Product>(
                "usp_listProducts",
                commandType: CommandType.StoredProcedure
            );

            return products;
        }

        // List Product By Id
        public async Task<Product> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            string query = "usp_selectProductById";

            Product? product = await _connection.QueryFirstOrDefaultAsync<Product>(
                query, new { @ProductId = productId }, commandType: CommandType.StoredProcedure
            );

            if (product == null)
            {
                throw new ProductNotFoundExceptions(productId);
            }

            return product;
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var query = "usp_insertProduct";
                    var parameters = new
                    {
                        @Name = product.Name,
                        @Description = product.Descripcion,
                        @Price = product.Precio,
                        @Stock = product.Stock,
                        @CategoryId = product.CategoryId,
                        @SupplierId = product.SupplierId
                    };

                    await _connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();

                    Console.WriteLine($"Error crear el producto: {ex.Message}");

                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _connection.Open();
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var query = "usp_updateProduct";
                    var parameters = new
                    {
                        @ProductId = product.Id,
                        @Name = product.Name,
                        @Description = product.Descripcion,
                        @Price = product.Precio,
                        @Stock = product.Stock,
                        @CreationDate = product.CreationDate,
                        @CategoryId = product.CategoryId,
                        @SupplierId = product.SupplierId
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

        // Delete Product
        public async Task RemoveAsync(Product product, CancellationToken cancellationToken = default)
        {
            var query = "usp_deleteProduct";
            await _connection.ExecuteAsync(query, new { @ProductId = product.Id }, commandType: CommandType.StoredProcedure);
        }

    }
}
