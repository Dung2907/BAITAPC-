using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace Example07.Services.Passwords
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly IPasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string actualPassword, string hashedPassword)
        {
            var verificationResult = _passwordHasher.VerifyHashedPassword(null, hashedPassword, actualPassword);
            return verificationResult == PasswordVerificationResult.Success;
        }
    }
}

