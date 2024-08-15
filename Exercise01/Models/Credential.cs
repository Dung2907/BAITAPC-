using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("credentials")]
    public class Credential : BaseEntity
    {
        [Key]
        [Column("credential_id")]
        public int CredentialId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("is_enabled")]
        public bool IsEnabled { get; set; }

        [Column("is_account_non_expired")]
        public bool IsAccountNonExpired { get; set; }

        [Column("is_account_non_locked")]
        public bool IsAccountNonLocked { get; set; }

        [Column("is_credentials_non_expired")]
        public bool IsCredentialsNonExpired { get; set; }

        // Navigation property để tham chiếu đến người dùng
        public virtual User User { get; set; }

        // Navigation property để tham chiếu danh sách các VerificationToken
        public virtual ICollection<VerificationToken> VerificationTokens { get; set; }
    }
}
