using Domain.Entities;
using Domain.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;


        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
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

            // Ejecutar el procedimiento almacenado para obtener el producto por su ID
            Product? product = await _connection.QueryFirstOrDefaultAsync<Product>(
                query,new { @ProductId = productId },commandType: CommandType.StoredProcedure
            );

            // Verificar si el producto fue encontrado
            if (product == null)
            {
                throw new ProductNotFoundExceptions(productId);
            }

            return product;
        }


        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            // Abrir la conexión
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

                    // Commit the transaction
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



        // Delete Product

        public async Task RemoveAsync(Product product, CancellationToken cancellationToken = default)
        {
            var query = "usp_deleteProduct";
            await _connection.ExecuteAsync(query, new { @ProductId = product.Id }, commandType: CommandType.StoredProcedure);
        }


    }
}
