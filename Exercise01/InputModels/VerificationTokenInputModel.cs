using System;
using System.ComponentModel.DataAnnotations;

namespace Exercise01.InputModels
{
    public class VerificationTokenInputModel
    {
        [Required(ErrorMessage = "CredentialId is required.")]
        public int CredentialId { get; set; }

        [Required(ErrorMessage = "VerificationToken is required.")]
        public string VerifToken { get; set; }

        [Required(ErrorMessage = "ExpiredDate is required.")]
        public DateTime ExpiredDate { get; set; }
    }
}
