using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class PaymentInputModel
    {
        [Required(ErrorMessage = "OrderId is required.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "IdPayed is required.")]
        public int IdPayed { get; set; }

        [Required(ErrorMessage = "PaymentStatus is required.")]
        public int PaymentStatus { get; set; }
    }
}
