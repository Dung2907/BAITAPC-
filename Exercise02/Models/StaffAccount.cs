using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise02.Models
{
    public class StaffAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } // Đổi từ int thành Guid

        public Guid RoleId { get; set; } // Đổi từ int thành Guid
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool Active { get; set; } = true;

        public string Image { get; set; }

        public string Placeholder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Guid? CreatedBy { get; set; } // Đổi từ int thành Guid

        public Guid? UpdatedBy { get; set; } // Đổi từ int thành Guid
    }
}
