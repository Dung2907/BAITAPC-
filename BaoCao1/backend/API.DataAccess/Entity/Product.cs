using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranAnhDung.API.DataAccess.Entity
{
    [Table("Product", Schema = "dbo")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "BIGINT")]
        public long ProductId { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Title { get; set; } 
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Slug { get; set; } 

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Summary { get; set; } 

        [Column(TypeName = "text")]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Photo { get; set; } 

        [Required]
        [Column(TypeName = "INT")]
        public int Stock { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Size { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Condition { get; set; } 

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; } 

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Discount { get; set; }

        [Required]
        [Column(TypeName = "SMALLINT")]
        public short IsFeatured { get; set; }

        [Column(TypeName = "BIGINT")]
        public long? CatId { get; set; }

        [Column(TypeName = "BIGINT")]
        public long? ChildCatId { get; set; }

        [Column(TypeName = "BIGINT")]
        public long? BrandId { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        [ForeignKey("CatId")]
        public virtual Category? Cat { get; set; }
    }
}
