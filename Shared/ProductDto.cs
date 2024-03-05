using System;

namespace Shared
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime CreationDate { get; set; }

        public string CategoryName { get; set; } // Propiedad para almacenar el nombre de la categoría
        public int CategoryId { get; set; } // Propiedad para almacenar el identificador de la categoría
        public string SupplierName { get; set; } // Propiedad para almacenar el nombre del proveedor
        public int SupplierId { get; set; } // Propiedad para almacenar el identificador del proveedor

        public ProductDto()
        {
        }

        public ProductDto(int id, string name, string descripcion, decimal precio, int stock, DateTime creationDate, string categoryName, int categoryId, string supplierName, int supplierId)
        {
            Id = id;
            Name = name;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            CreationDate = creationDate;
            CategoryName = categoryName;
            CategoryId = categoryId;
            SupplierName = supplierName;
            SupplierId = supplierId;
        }
    }
}
