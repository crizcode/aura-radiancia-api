namespace Domain.Repositories
{
    public class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPersonRepository _personRepository;

        public IProductRepository Products => _productRepository;
        public ICategoryRepository Categories => _categoryRepository;
        public ISupplierRepository Suppliers => _supplierRepository;
        public IPersonRepository People => _personRepository;

        public RepositoryManager(IProductRepository productRepository, ICategoryRepository categoryRepository, ISupplierRepository supplierRepository, IPersonRepository personRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public void Save()
        {
            // Implementación para guardar cambios en los repositorios
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

        }
    }
}
