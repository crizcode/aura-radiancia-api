namespace Domain.Services.Abstractions
{
    public interface IServiceManager
{
        IProductService ProductService { get; }
        ICategoryService CategoryService { get; }
        ISupplierService SupplierService { get; }

        IPersonService PersonService { get; }

    }
}