using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        // Implementación del método SaveChangesAsync de la interfaz IUnitOfWork
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Aquí puedes incluir la lógica para guardar los cambios en la base de datos de forma asincrónica
            // Por ejemplo:
            // 

            // Como ejemplo, simplemente devolvemos una tarea completada
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            // Aquí puedes realizar tareas de limpieza si es necesario
            // Ejm. liberar recursos no administrados.
            GC.SuppressFinalize(this);
        }
    }
}
