using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Pass { get; set; }
    }
}