using System;
using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class OrderInputModel
    {
        [Required(ErrorMessage = "CartId is required.")]
        public int CartId { get; set; }

        [Required(ErrorMessage = "OrderDesc is required.")]
        public string OrderDesc { get; set; }

        [Required(ErrorMessage = "OrderFee is required.")]
        public decimal OrderFee { get; set; }

    }
}
