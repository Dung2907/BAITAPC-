using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("verification_tokens")]
    public class VerificationToken : BaseEntity
    {
        [Key]
        [Column("verification_token_id")]
        public int TokenId { get; set; }

        [ForeignKey("CredentialId")]
        [Column("credential_id")]
        public int CredentialId { get; set; }

        [Column("verif_token")]
        public string VerifToken { get; set; }

        [Column("expire_date")]
        public DateTime ExpiredDate { get; set; }

        // Navigation property để tham chiếu đến Credential
        public virtual Credential Credential { get; set; }
    }
}
