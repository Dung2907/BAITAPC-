namespace backend.Models
{
    public class GetTokenRequest
    {
        public string UserName { get; set; } = Consts.UserName;
        public string Password { get; set; } = Consts.Password;
    }

    public class AuthorizationResponse
    {
        public string UserId { get; set; }
        public string AuthorizationToken { get; set; }
        public string RefreshToken { get; set; }
    }

    
}
