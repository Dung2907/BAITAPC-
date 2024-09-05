using System.ComponentModel.DataAnnotations;

namespace Example07.Models
{
    public class User
    {
        [Key] // Thuộc tính này đánh dấu đây là khóa chính
        public long Id { get; set; } // Khóa chính

        public string Username { get; set; }

        public string PasswordHash { get; set; }

    // Các thuộc tính khác
    }

}
