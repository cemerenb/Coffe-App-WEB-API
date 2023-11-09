using System.ComponentModel.DataAnnotations;

namespace VerifyEmailForgotPasswordTutorial.Models
{
    public class GetMenuItem
    {
        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;
        
    }
}
