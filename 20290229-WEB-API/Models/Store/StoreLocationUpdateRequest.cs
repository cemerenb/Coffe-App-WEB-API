using System.ComponentModel.DataAnnotations;

namespace Models.Store
{
    public class StoreLocationUpdateRequest
    {




        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }




    }
}
