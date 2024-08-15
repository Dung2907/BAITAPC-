using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("order_items")]
    public class OrderItem : BaseEntity
    {
        [Key]
        [Column("order_item_id")]
        public int OrderItemId { get; set; }

        [ForeignKey("ProductId")]
        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
