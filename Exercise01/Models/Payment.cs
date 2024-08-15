using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("payments")]
    public class Payment : BaseEntity
    {
        [Key]
        [Column("payment_id")]
        public int PaymentId { get; set; }

        [ForeignKey("OrderId")]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("id_payed")]
        public int IdPayed { get; set; }

        [Column("payment_status")]
        public int PaymentStatus { get; set; }

        // Navigation property để tham chiếu đến đơn đặt hàng
        public virtual Order Order { get; set; }
    }
}
