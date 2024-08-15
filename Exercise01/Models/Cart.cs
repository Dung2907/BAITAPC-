using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("carts")]
    public class Cart : BaseEntity
    {
        [Key]
        [Column("cart_id")] // Xác định tên cụ thể của cột
        public int CartId { get; set; }

        [Column("user_id")] // Tên cột mới: user_id
        [ForeignKey("UserId")] // Đánh dấu khóa ngoại
        public int UserId { get; set; } // Lưu trữ khóa chính của bảng User

        [NotMapped] // Bỏ qua khi ánh xạ với bảng trong database
        public User User { get; set; } // Tạo thuộc tính điều hướng và không ánh xạ để tránh ràng buộc kép
    }
}
