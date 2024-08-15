using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class UserInputModel
    {
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "ImageUrl is required.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        // [EmailAddress(ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        // [Phone(ErrorMessage = "Invalid Phone format.")]
        public string Phone { get; set; }
    }
}
