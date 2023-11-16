using System.ComponentModel.DataAnnotations;

namespace Models.Order
{
    public class DeleteFromCart
    {
        public string StoreEmail { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;

    }
}
