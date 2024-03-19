using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Name es obligatorio.")]
        public string RoleName { get; set; } 
    }
}
