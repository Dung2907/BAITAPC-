using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        // Navigation property để tham chiếu danh sách các Credential
        public virtual ICollection<Credential> Credentials { get; set; }
    }
}
