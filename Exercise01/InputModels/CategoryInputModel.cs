using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class CategoryInputModel
    {
        // [Required(ErrorMessage = "SubCategoryId is required.")]
        // [Range(1, int.MaxValue, ErrorMessage = "SubCategoryId must be greater than 0.")]
        public int? SubCategoryId { get; set; }

        [Required(ErrorMessage = "CategoryTitle is required.")]
        // [StringLength(100, MinimumLength = 3, ErrorMessage = "CategoryTitle must be between 3 and 100 characters.")]
        public string CategoryTitle { get; set; }

        [Required(ErrorMessage = "ImageUrl is required.")]
        // [Url(ErrorMessage = "Invalid Url format.")]
        public string ImageUrl { get; set; }
    }
}
