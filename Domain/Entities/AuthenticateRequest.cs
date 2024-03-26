using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Pass { get; set; }
    }
}