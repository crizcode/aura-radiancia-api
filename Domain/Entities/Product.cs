using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Name es obligatorio.")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
        public string Descripcion { get; set; } 

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        public decimal Precio { get; set; } 

        [Required(ErrorMessage = "El campo Stock es obligatorio.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El campo CreationDate es obligatorio.")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "El campo CategoryId es obligatorio.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } 

        [Required(ErrorMessage = "El campo SupplierId es obligatorio.")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}