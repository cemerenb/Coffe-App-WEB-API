using System.ComponentModel.DataAnnotations;

namespace Models.Cart
{
    public class AddToCart
    {
        
        public string StoreEmail { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;

    }
}
