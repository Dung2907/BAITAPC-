using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class ProductInputModel
    {
        [Required(ErrorMessage = "ProductTitle is required.")]
        // [StringLength(100, MinimumLength = 3, ErrorMessage = "ProductTitle must be between 3 and 100 characters.")]
        public string ProductTitle { get; set; }

        [Required(ErrorMessage = "ImageUrl is required.")]
        // [Url(ErrorMessage = "Invalid Url format.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Sku is required.")]
        public string Sku { get; set; }

        [Required(ErrorMessage = "PriceUnit is required.")]
        // [Range(0.01, double.MaxValue, ErrorMessage = "PriceUnit must be greater than 0.")]
        public decimal PriceUnit { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        // [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        // [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0.")]
        public int CategoryId { get; set; }
    }
}
