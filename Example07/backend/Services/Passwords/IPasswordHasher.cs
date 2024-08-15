namespace Example07.Services.Passwords
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string actualPassword, string hashedPassword);
    }
}
