using System.ComponentModel.DataAnnotations;

namespace Models.Order
{
    public class DeleteFromCart
    {
        public string StoreEmail { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;

    }
}
