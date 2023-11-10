using System.ComponentModel.DataAnnotations;

namespace cemerenbwebapi.Models
{
    public class DeleteMenuItemRequest
    {
        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;
       
        [Required]
        public string MenuItemId { get; set; } = string.Empty;

    }
}
