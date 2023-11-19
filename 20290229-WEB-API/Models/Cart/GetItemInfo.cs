using System.ComponentModel.DataAnnotations;

namespace Models.Cart
{
    public class GetItemInfo
    {
        [Required, EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string MenuItemId { get; set; } = string.Empty;


    }
}
