﻿using System.ComponentModel.DataAnnotations;

namespace VerifyEmailForgotPasswordTutorial.Models
{
    public class StoreResetPasswordRequest
    {
        [Required]
        public string StoreToken { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters, dude!")]
        public string StorePassword { get; set; } = string.Empty;
        [Required, Compare("StorePassword")]
        public string StoreConfirmPassword { get; set; } = string.Empty;
    }
}