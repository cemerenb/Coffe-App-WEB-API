using System.ComponentModel.DataAnnotations;

namespace cemerenbwebapi.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string StoreEmail { get; set; } = string.Empty;
        public string MenuItemName { get; set; } = string.Empty;

        public string MenuItemDescription { get; set; } = string.Empty;

        public string MenuItemImageLink {  get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;

        public int MenuItemIsAvaliable { get; set; } = 0;

        public float MenuItemPrice { get; set; }

        public int MenuItemCategory {  get; set; }
    }
}
