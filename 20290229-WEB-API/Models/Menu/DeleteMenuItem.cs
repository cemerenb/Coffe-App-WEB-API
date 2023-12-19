using System.ComponentModel.DataAnnotations;

namespace Models.Menu
{
    public class DeleteMenuItemRequest
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;

        [Required]
        public string MenuItemId { get; set; } = string.Empty;

    }
}
