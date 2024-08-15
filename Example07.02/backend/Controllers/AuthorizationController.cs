using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthorizationController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] GetTokenRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordValid)
            {
                return Unauthorized();
            }

            var resp = GenerateAuthorizationToken(user.Id, user.UserName);
            return Ok(resp);
        }

        private AuthorizationResponse GenerateAuthorizationToken(string userId, string userName)
        {
            var now = DateTime.UtcNow;
            var secret = _configuration.GetValue<string>("Secret");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var userClaims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimTypes.NameIdentifier, userId),
            };

            var expires = now.AddMinutes(60);

            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: userClaims,
                expires: expires,
                audience: "https://localhost:7000/",
                issuer: "https://localhost:7000/",
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var resp = new AuthorizationResponse
            {
                UserId = userId,
                AuthorizationToken = encodedJwt,
                RefreshToken = string.Empty
            };

            return resp;
        }
    }
}