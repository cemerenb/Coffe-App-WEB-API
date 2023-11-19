﻿using System.ComponentModel.DataAnnotations;

namespace Models.Cart
{
    public class GetCart
    {
        [Required, EmailAddress]
        public string UserEmail { get; set; } = string.Empty;


    }
}
