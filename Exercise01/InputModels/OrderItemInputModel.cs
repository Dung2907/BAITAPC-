using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class OrderItemInputModel
    {
        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "OrderId is required.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }
}
