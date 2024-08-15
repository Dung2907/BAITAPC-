using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class AddressInputModel
    {
        [Required(ErrorMessage = "FullAddress is required.")]
        public string FullAddress { get; set; }

        [Required(ErrorMessage = "PostalCode is required.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
