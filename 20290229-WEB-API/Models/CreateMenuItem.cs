﻿using System.ComponentModel.DataAnnotations;

namespace VerifyEmailForgotPasswordTutorial.Models
{
    public class CreateMenuItemRequest
    {
        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;
        [Required]
        public string MenuItemName { get; set; } = string.Empty;
        [Required]
        public string MenuItemId { get; set; } = string.Empty;

        [Required]
        public int MenuItemCount { get; set; } = 0;

        [Required]
        public float MenuItemPrice { get; set; }

        [Required]
        public int MenuItemCategory {  get; set; }
    }
}
