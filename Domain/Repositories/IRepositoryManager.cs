using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepositoryManager : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }

        ISupplierRepository Suppliers { get; }
    
    }
}