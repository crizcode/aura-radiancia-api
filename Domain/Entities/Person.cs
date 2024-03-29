﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; } 

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string Apellido { get; set; } 

        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress]
        public string Email { get; set; } 
        [Required(ErrorMessage = "El campo UserName es obligatorio.")]
        public string UserName {get; set;} 


        [JsonIgnore]

        [Required(ErrorMessage = "El campo Pass es obligatorio.")]
        public string Pass { get; set;}

        [Required]
        public int DepartmentId { get; set; }
        public Departament Departament { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public string? Estado { get; set; } // 0 o 1

    

}
}
