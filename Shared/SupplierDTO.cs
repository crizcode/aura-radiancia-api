using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Shared
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }

        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string? Estado { get; set; }

        public SupplierDto()
        {
        }

        public SupplierDto(int supplierId, string name, string direccion, string telefono, string estado)
        {
            SupplierId = supplierId;
            Name = name;
            Direccion = direccion;
            Telefono = telefono;
            Estado = estado;
        }
    }
}