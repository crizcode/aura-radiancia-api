using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class Departament
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Name es obligatorio.")]
        public string Name { get; set; } 
    }
}
