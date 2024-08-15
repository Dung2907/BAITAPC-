using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }  // Sử dụng GUID cho Id

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string Sku { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        public decimal? ComparePrice { get; set; }

        public decimal? BuyingPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [StringLength(165)]
        public string ShortDescription { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public string ProductType { get; set; }

        public bool Published { get; set; }

        public bool DisableOutOfStock { get; set; } = true;
        public string Note { get; set; }

        public Guid CreatedBy { get; set; }  // Sử dụng GUID cho CreatedBy
        public Guid UpdatedBy { get; set; }  // Sử dụng GUID cho UpdatedBy
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } // Thuộc tính điều hướng
        public virtual ICollection<ProductShippingInfo> ProductShippingInfos { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public ICollection<Variant> Variants { get; set; }
    }
}
