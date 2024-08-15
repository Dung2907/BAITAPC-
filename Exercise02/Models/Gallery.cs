using System;
using System.ComponentModel.DataAnnotations;
using Exercise02.Models; // Nếu lớp Product nằm trong namespace này

namespace Exercise02.Models
{
    public class Gallery
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; } // Lớp Product phải tồn tại và được khai báo đúng

        [Required]
        public string Image { get; set; }

        [Required]
        public string Placeholder { get; set; }

        public bool IsThumbnail { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
