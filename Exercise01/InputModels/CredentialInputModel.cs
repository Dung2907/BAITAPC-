using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class CredentialInputModel
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "IsEnabled is required.")]
        public bool IsEnabled { get; set; }

        [Required(ErrorMessage = "IsAccountNonExpired is required.")]
        public bool IsAccountNonExpired { get; set; }

        [Required(ErrorMessage = "IsAccountNonLocked is required.")]
        public bool IsAccountNonLocked { get; set; }

        [Required(ErrorMessage = "IsCredentialsNonExpired is required.")]
        public bool IsCredentialsNonExpired { get; set; }
    }
}
