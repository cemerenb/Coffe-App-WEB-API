using System.ComponentModel.DataAnnotations;

namespace Models.PointRule
{
    public class ToggleLoyaltyStatus
    {
        [Required]
        public int IsPointsEnabled { get; set; }
        [Required, EmailAddress]
        public String StoreEmail { get; set; } = String.Empty;
        
    }
}
