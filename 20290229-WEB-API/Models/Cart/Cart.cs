using System.ComponentModel.DataAnnotations;

namespace Models.Cart
{
    public class Cart
    {
        public int Id { get; set; }
        public string StoreEmail { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;
        public int ItemCount { get; set; }

    }
}
