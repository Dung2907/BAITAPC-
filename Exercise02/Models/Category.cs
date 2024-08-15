using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } // Đổi từ int thành Guid

        public Guid? ParentId { get; set; } // Đổi từ int thành Guid
        [ForeignKey("ParentId")]
        public Category? Parent { get; set; } // Thuộc tính điều hướng

        [Required]
        [MaxLength(255)]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }
        public string? Icon { get; set; } // Cho phép giá trị null
        public string? Image { get; set; } // Cho phép giá trị null
        public string? Placeholder { get; set; } // Cho phép giá trị null
        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid? CreatedBy { get; set; } // Đổi từ int thành Guid
        [ForeignKey("CreatedBy")]
        public Guid? UpdatedBy { get; set; } // Đổi từ int thành Guid
        [ForeignKey("UpdatedBy")]
        public ICollection<Category> Children { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } // Thuộc tính điều hướng
    }
}
