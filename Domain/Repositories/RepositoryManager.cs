using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }

        public ISupplierRepository Suppliers { get; private set; }

        public RepositoryManager(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository suppliersRepository)
        {
            Products = productRepository;
            Categories = categoryRepository;
            Suppliers = suppliersRepository;
        }

        public void Save()
        {
            // Implementación para guardar cambios en los repositorios
        }

        public void Dispose()
        {
            // Implementación para liberar recursos
        }
    }
}