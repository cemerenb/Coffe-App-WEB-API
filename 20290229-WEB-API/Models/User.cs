namespace VerifyEmailForgotPasswordTutorial.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FullName { get; set; }

        public string? UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
    }
}
