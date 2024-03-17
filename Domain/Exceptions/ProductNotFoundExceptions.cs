namespace Domain.Exceptions
{
    public sealed class ProductNotFoundExceptions : NotFoundException
    {
        public ProductNotFoundExceptions(int productid) 
            : base($"EL producto con id {productid} no fue encontrado.")
        {
        }
    }
}
