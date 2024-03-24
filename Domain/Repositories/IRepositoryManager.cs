namespace Domain.Repositories
{
    public interface IRepositoryManager : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        ISupplierRepository Suppliers { get; }
        IPersonRepository People { get; }

        IRoleRepository Roles { get; }
        IAuthRepository Auths { get; }

    }
}