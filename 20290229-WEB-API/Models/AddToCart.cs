﻿using System.ComponentModel.DataAnnotations;

namespace Models.Cart
{
    public class AddToCart
    {
        
        public string StoreEmail { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;

    }
}
