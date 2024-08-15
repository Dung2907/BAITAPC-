using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("addresses")]
    public class Address : BaseEntity
    {
        [Key]
        [Column("address_id")] // Xác định tên cụ thể của cột
        public int AddressId { get; set; }

        [Column("full_address")] // Tên cột mới: full_address
        public string FullAddress { get; set; }

        [Column("postal_code")] // Tên cột mới: postal_code
        public string PostalCode { get; set; }

        [Column("city")] // Tên cột mới: city
        public string City { get; set; }

        [Column("user_id")] // Tên cột mới: user_id
        [ForeignKey("UserId")] // Đánh dấu khóa ngoại
        public int UserId { get; set; } // Lưu trữ khóa chính của bảng User

        [NotMapped] // Bỏ qua khi ánh xạ với bảng trong database
        public User User { get; set; } // Tạo thuộc tính điều hướng và không ánh xạ để tránh ràng buộc kép
    }
}
