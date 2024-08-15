using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("orders")]
    public class Order : BaseEntity
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [ForeignKey("CartId")]
        [Column("cart_id")]
        public int CartId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("order_date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column("order_desc")]
        public string OrderDesc { get; set; }

        [Column("order_fee")]
        public decimal OrderFee { get; set; }

        // Navigation property
        public virtual Cart Cart { get; set; }

        // Navigation property để tham chiếu danh sách các thanh toán
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
