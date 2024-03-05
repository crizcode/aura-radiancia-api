using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public DateTime CreationDate { get; set; }

        // Propiedades de navegación para las relaciones con Category y Supplier
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}