using NSwag.Annotations;
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


        public int CategoryId { get; set; }

        [SwaggerIgnore]
        public string? CategoryName { get; private set; }


        public int SupplierId { get; set; }

        [SwaggerIgnore]

        public string? SupplierName { get; private set; }

        // Resto de las propiedades sin cambios

        public ProductDto()
        {
        }

        public ProductDto(int id, string name, string descripcion, decimal precio, int stock, DateTime creationDate, int categoryId, int supplierId)
        {
            Id = id;
            Name = name;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            CreationDate = creationDate;
            CategoryId = categoryId;
            SupplierId = supplierId;
        }
    }
}