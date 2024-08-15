using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("products")]
    public class Product : BaseEntity
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("product_title")]
        public string ProductTitle { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("sku")]
        public string Sku { get; set; }

        [Column("price_unit")]
        public decimal PriceUnit { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [ForeignKey("CategoryId")]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [NotMapped]
        public virtual Category Category { get; set; }
    }
}
