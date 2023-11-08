using System.ComponentModel.DataAnnotations;

namespace VerifyEmailForgotPasswordTutorial.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string StoreEmail { get; set; } = string.Empty;
        public string MenuItemName { get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;

        public int MenuItemCount { get; set; } = 0;

        public float MenuItemPrice { get; set; }

        public int MenuItemCategory {  get; set; }
    }
}
