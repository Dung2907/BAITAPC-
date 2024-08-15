using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("likes")]
    public class Like : BaseEntity
    {
        [Key]
        [Column("user_id", Order = 0)]
        public int UserId { get; set; }

        [Key]
        [Column("product_id", Order = 1)]
        public int ProductId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("liked_at")]
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
