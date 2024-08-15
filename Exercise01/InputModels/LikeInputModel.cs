using System;
using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class LikeInputModel
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }
    }
}
