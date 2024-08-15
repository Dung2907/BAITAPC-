using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class CartInputModel
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }
    }
}
