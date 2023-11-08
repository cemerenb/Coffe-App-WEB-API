using System.ComponentModel.DataAnnotations;

namespace VerifyEmailForgotPasswordTutorial.Models
{
    public class StoreLoginRequest
    {
        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;
        [Required]
        public string StorePassword { get; set; } = string.Empty;
    }
}
