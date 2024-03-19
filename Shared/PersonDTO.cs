using System.ComponentModel.DataAnnotations;

namespace Infraestructure.Shared
{
    public class PersonDto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Pass { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int RoleId { get; set; }

        public PersonDto()
        {
        }

        public PersonDto(int id, string nombre, string apellido, string email, string user, string pass, int departamentoId, int rolId)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            UserName = user;
            Pass = pass;
            DepartmentId = departamentoId;
            RoleId = rolId;
        }
    }
}
