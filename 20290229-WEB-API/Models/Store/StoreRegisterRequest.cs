using System.ComponentModel.DataAnnotations;

namespace Models.Store
{
    public class StoreRegisterRequest
    {
        [Required]
        public string StoreName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;

        [Required]
        public string StoreTaxId { get; set; } = string.Empty;

        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters, dude!")]

        public string StorePassword { get; set; } = string.Empty;
        [Required, Compare("StorePassword")]
        public string StoreConfirmPassword { get; set; } = string.Empty;
    }
}
