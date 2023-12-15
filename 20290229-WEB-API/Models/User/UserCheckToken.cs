using System.ComponentModel.DataAnnotations;

namespace Models.User
{
    public class UserCheckToken
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordResetToken { get; set; } = string.Empty;
    }
}
