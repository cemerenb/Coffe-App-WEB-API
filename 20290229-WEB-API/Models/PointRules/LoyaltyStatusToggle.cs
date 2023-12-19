using System.ComponentModel.DataAnnotations;

namespace Models.PointRule
{
    public class ToggleLoyaltyStatus
    {
        [Required]
        public int IsPointsEnabled { get; set; }
        [Required]
        public String AccessToken { get; set; } = String.Empty;
        
    }
}
