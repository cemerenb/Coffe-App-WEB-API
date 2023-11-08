using System.ComponentModel.DataAnnotations;

namespace VerifyEmailForgotPasswordTutorial.Models
{
    public class StoreUpdateRequest
    {
        

        [Required]
        public int StoreIsOn { get; set; }

        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;

        [Required]
        public string StoreLogoLink { get; set; } = string.Empty;

        [Required]
        public string StoreOpeningTime { get; set; } = string.Empty;

        [Required]
        public string StoreClosingTime { get; set; } = string.Empty;

       


    }
}
