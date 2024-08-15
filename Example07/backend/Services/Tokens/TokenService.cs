using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Example07.Models;

namespace Example07.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateToken(User user)
        {
            var tokenInstance = new Token();

            // Cấu hình khóa bảo mật và chứng chỉ ký
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Thiết lập thời gian hết hạn của token
            tokenInstance.Expiration = DateTime.UtcNow.AddMinutes(30); // Điều chỉnh theo nhu cầu

            // Tạo JWT token
            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: tokenInstance.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenInstance.AccessToken = tokenHandler.WriteToken(jwtToken);

            // Tạo và thiết lập refresh token
            tokenInstance.RefreshToken = CreateRefreshToken();

            return tokenInstance;
        }

        public string CreateRefreshToken()
        {
            var number = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
