namespace Infraestructure.Shared
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public SupplierDto()
        {
        }

        public SupplierDto(int supplierId, string name, string estado)
        {
            SupplierId = supplierId;
            Name = name;
            Estado = estado;
        }
    }
}