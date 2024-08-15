using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models
{
    public class ProductShippingInfo
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public decimal Weight { get; set; }
        [Required]
        [StringLength(10)]
        public string WeightUnit { get; set; }

        [Required]
        public decimal Volume { get; set; }
        [Required]
        [StringLength(10)]
        public string VolumeUnit { get; set; }

        [Required]
        public decimal DimensionWidth { get; set; }
        [Required]
        public decimal DimensionHeight { get; set; }
        [Required]
        public decimal DimensionDepth { get; set; }
        [Required]
        [StringLength(10)]
        public string DimensionUnit { get; set; }
    }
}
