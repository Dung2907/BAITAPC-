using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models
{
    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } // Khóa chính, kiểu Guid

        [Required]
        public Guid ProductId { get; set; } // Khóa ngoại đến Product

        [Required]
        public Guid CategoryId { get; set; } // Khóa ngoại đến Category

        // Định nghĩa quan hệ
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
