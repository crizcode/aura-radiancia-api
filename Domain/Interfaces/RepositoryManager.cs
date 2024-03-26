namespace  Core.Domain.Interfaces
{
    public class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthRepository _authRepository;

        public IProductRepository Products => _productRepository;
        public ICategoryRepository Categories => _categoryRepository;
        public ISupplierRepository Suppliers => _supplierRepository;
        public IPersonRepository People => _personRepository;
        public IRoleRepository Roles => _roleRepository;

        public IAuthRepository Auths => _authRepository;


        public RepositoryManager(IProductRepository productRepository, ICategoryRepository categoryRepository, 
                                 ISupplierRepository supplierRepository, IPersonRepository personRepository,
                                 IRoleRepository roleRepository, IAuthRepository authRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
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
