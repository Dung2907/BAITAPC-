using Example07.Models; 

namespace Example07.Services.Tokens
{
    public interface ITokenService
    {
        Token CreateToken(User user);
        string CreateRefreshToken();
    }
}
