using Services.Abstractions;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService;
        private readonly Lazy<ICategoryService> _lazyCategoryService;
        private readonly Lazy<ISupplierService> _lazySupplierService;

        public ServiceManager(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryManager, unitOfWork, mapper));
            _lazyCategoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, unitOfWork, mapper));
            _lazySupplierService = new Lazy<ISupplierService>(() => new SupplierService(repositoryManager, unitOfWork, mapper));
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
    }
}