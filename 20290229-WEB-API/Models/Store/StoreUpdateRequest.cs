using System.ComponentModel.DataAnnotations;

namespace Models.Store
{
    public class StoreUpdateRequest
    {


       

        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;

        [Required]
        public string StoreLogoLink { get; set; } = string.Empty;

        [Required]
        public string StoreCoverImageLink { get; set; } = string.Empty;

        [Required]
        public string StoreOpeningTime { get; set; } = string.Empty;

        [Required]
        public string StoreClosingTime { get; set; } = string.Empty;
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }




    }
}
