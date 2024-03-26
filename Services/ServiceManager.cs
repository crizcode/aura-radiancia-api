using AutoMapper;
using Core.Helpers;
using Core.Domain.Interfaces;
using Core.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace Core.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService;
        private readonly Lazy<ICategoryService> _lazyCategoryService;
        private readonly Lazy<ISupplierService> _lazySupplierService;
        private readonly Lazy<IPersonService> _lazyPersonService;
        private readonly Lazy<IRoleService> _lazyRoleService;
        private readonly Lazy<IAuthService> _lazyAuthService;

        public ServiceManager(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryManager, unitOfWork, mapper));
            _lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, unitOfWork, mapper));
            _lazySupplierService = new Lazy<ISupplierService>(() => new SupplierService(repositoryManager, unitOfWork, mapper));
            _lazyPersonService = new Lazy<IPersonService>(() => new PersonService(repositoryManager, unitOfWork, mapper, appSettings));
            _lazyRoleService = new Lazy<IRoleService>(() => new RoleService(repositoryManager, unitOfWork, mapper));
            _lazyAuthService = new Lazy<IAuthService>(() => new AuthService(repositoryManager, appSettings));
        }

        public IProductService ProductService
        {
            get { return _lazyProductService.Value; }
        }

        public ICategoryService CategoryService
        {
            get { return _lazyCategoryService.Value; }
        }

        public ISupplierService SupplierService
        {
            get { return _lazySupplierService.Value; }
        }

        public IPersonService PersonService
        {
            get { return _lazyPersonService.Value; }
        }
        public IRoleService RoleService
        {
            get { return _lazyRoleService.Value; }
        }

        public IAuthService AuthService
        {
            get { return _lazyAuthService.Value; }
        }
    }
}