using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Example07.Repositories;
using Example07.Models.Dtos;
using Example07.Services.Tokens;
using Example07.Services.Passwords;
using Example07.Models;

namespace Example07.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthController(ITokenService tokenService, IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        // Endpoint xử lý đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            // Tìm người dùng dựa trên email
            var user = await _userRepository.GetByEmailAsync(userLogin.Email);

            // Kiểm tra xem người dùng có tồn tại không
            if (user == null)
            {
                return BadRequest(new { message = "No registered user for this email." });
            }

            // Kiểm tra mật khẩu
            var passwordValid = _passwordHasher.VerifyPassword(userLogin.Password, user.Password);
            if (!passwordValid)
            {
                return BadRequest(new { message = "Invalid password" });
            }

            // Tạo token mới
            var token = _tokenService.CreateToken(user);

            // Cập nhật refresh token cho người dùng
            if (token.RefreshToken != null)
            {
                user.RefreshToken = token.RefreshToken;
            }
            else
            {
                return BadRequest(new { message = "Failed to generate refresh token." });
            }
            user.RefreshTokenEndDate = token.Expiration.AddMinutes(5);
            await _userRepository.CommitAsync();

            return Ok(token);
        }

        // Endpoint xử lý làm mới token
        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshToken refreshToken)
        {
            // Tìm người dùng dựa trên refresh token
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken.Token);

            // Kiểm tra xem refresh token có hợp lệ không
            if (user == null)
            {
                return BadRequest(new { message = "Refresh token is invalid." });
            }

            // Kiểm tra xem refresh token đã hết hạn chưa
            if (user.RefreshTokenEndDate < DateTime.Now)
            {
                return BadRequest(new { message = "Refresh token expired" });
            }

            // Tạo token mới
            var token = _tokenService.CreateToken(user);

            // Cập nhật refresh token cho người dùng
            if (token.RefreshToken != null)
            {
                user.RefreshToken = token.RefreshToken;
            }
            else
            {
                return BadRequest(new { message = "Failed to generate refresh token." });
            }
            user.RefreshTokenEndDate = token.Expiration.AddMinutes(5);
            await _userRepository.CommitAsync();

            return Ok(token);
        }
    }
}
