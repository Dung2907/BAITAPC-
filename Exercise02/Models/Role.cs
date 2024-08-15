using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tạo giá trị cho khóa chính
        public Guid Id { get; set; } // Đổi từ int thành Guid

        [Required]
        [StringLength(255)]
        public string RoleName { get; set; } // Tên vai trò, kiểu varchar(255), không cho phép null

        public string? Privileges { get; set; } // Quyền hạn, kiểu text, có thể null
    }
}
