namespace Example07.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
        // Các thuộc tính khác...
    }
}
