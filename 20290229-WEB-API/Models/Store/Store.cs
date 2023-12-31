﻿namespace Models.Store
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreEmail { get; set; } = string.Empty;
        public string StoreName { get; set; } = string.Empty;
        public string StoreLogoLink { get; set; } = string.Empty;
        public string StoreCoverImageLink { get; set; } = string.Empty;
        public string StoreOpeningTime { get; set; } = string.Empty;
        public string StoreClosingTime { get; set; } = string.Empty;
        public int StoreIsOn { get; set; }
        public string StoreTaxId { get; set; } = string.Empty;
        public byte[] StorePasswordHash { get; set; } = new byte[32];
        public byte[] StorePasswordSalt { get; set; } = new byte[32];
        public string? StorePasswordResetToken { get; set; }
        public DateTime? StoreResetTokenExpires { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
