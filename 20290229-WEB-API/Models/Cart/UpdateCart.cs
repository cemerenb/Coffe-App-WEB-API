﻿using System.ComponentModel.DataAnnotations;

namespace Models.Cart
{
    public class UpdateCart
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;
        [Required]
        public string MenuItemId { get; set; } = string.Empty;
        [Required]
        public int ItemCount { get; set; }

    }
}
