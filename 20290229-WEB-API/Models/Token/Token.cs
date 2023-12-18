namespace Models.Token
{
    public class Token
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTime? AccessTokenExpires { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpires { get; set; }
        
    }
}
