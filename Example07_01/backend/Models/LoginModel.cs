using System.ComponentModel.DataAnnotations;

namespace Example07.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Remember { get; set; } = false;
    }
}