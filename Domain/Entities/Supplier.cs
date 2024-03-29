using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "El campo Name es obligatorio.")]
        public string Name { get; set; }

        public string Direccion{ get; set; }

        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        public string? Estado { get; set; }


    }
}