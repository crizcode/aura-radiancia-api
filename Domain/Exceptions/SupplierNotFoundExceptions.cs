namespace Domain.Exceptions
{
    public sealed class SupplierNotFoundExceptions : NotFoundException
    {
        public SupplierNotFoundExceptions(int supplierId)
            : base($"La proveedor con id {supplierId} no fue encontrado.")
        {
        }
    }
}