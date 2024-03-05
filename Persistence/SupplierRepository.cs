using Domain.Entities;
using Domain.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;


namespace Persistence
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IDbConnection _connection;


        public SupplierRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // List suppliers 
        public async Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var suppliers = await _connection.QueryAsync<Supplier>(
                "usp_listSuppliers", commandType: CommandType.StoredProcedure
            );

            return suppliers;
        }

        // List Supplier By Id

        public async Task<Supplier> GetByIdAsync(int supplierId, CancellationToken cancellationToken = default)
        {
            string query = "usp_selectSupplierById";

            // Ejecutar el procedimiento almacenado para obtener el proveedor por su ID
            Supplier? supplier = await _connection.QueryFirstOrDefaultAsync<Supplier>(
                query, new { @SupplierId = supplierId }, commandType: CommandType.StoredProcedure
            );

            // Verificar si el suppliero fue encontrado
            if (supplier == null)
            {
                throw new CategoryNotFoundExceptions(supplierId);
            }

            return supplier;
        }


        public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            // Abrir la conexión
            _connection.Open();

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var query = "usp_insertSupplier";
                    var parameters = new
                    {
                        @Name = supplier.Name,
                        @Estado = supplier.Estado,
                    };

                    await _connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure, transaction: transaction);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {

                    transaction.Rollback();

                    Console.WriteLine($"Error crear el suppliero: {ex.Message}");

                    throw;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }


        public async Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {

            _connection.Open();

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    var query = "usp_updateSupplier";
                    var parameters = new
                    {
                        @SupplierId = supplier.SupplierId,
                        @Name = supplier.Name,
                        @Estado = supplier.Estado,
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



        // Delete Supplier

        public async Task RemoveAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            var query = "usp_deleteSupplier";
            await _connection.ExecuteAsync(query, new { @SupplierId = supplier.SupplierId}, commandType: CommandType.StoredProcedure);
        }


    }
}
