using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Example07.Models;

namespace TranAnhDung.API.DataAccess.Entity
{
    [Table("Category", Schema = "dbo")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "BIGINT")]
        public long CategoryId { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string? Title { get; set; } 

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string? Slug { get; set; } 

        [Column(TypeName = "varchar(255)")]
        public string? Summary { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? Photo { get; set; }

        [Required]
        [Column(TypeName = "SMALLINT")]
        public short IsParent { get; set; } 

        [Column(TypeName = "BIGINT")]
        public long? ParentId { get; set; }

        [Column(TypeName = "BIGINT")]
        public long? AddedBy { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Status { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("AddedBy")]
        public virtual User? AddedByNavigation { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category? ParentCategory { get; set; }

        // New Navigation Property
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
