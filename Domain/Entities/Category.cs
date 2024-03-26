using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{

    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El campo Name es obligatorio.")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        public string Estado { get; set; } 
    }
}

