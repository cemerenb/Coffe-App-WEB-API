using System.ComponentModel.DataAnnotations;

namespace Models.Store
{
    public class StoreCheckToken
    {
        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;
        [Required]
        public string StorePasswordResetToken { get; set; } = string.Empty;
    }
}
