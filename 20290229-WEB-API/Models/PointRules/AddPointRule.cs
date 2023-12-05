using System.ComponentModel.DataAnnotations;

namespace Models.PointRule
{
    public class AddPointRule
{
        [Required]
        public int IsPointsEnabled { get; set; }
        [Required, EmailAddress]
        public String StoreEmail { get; set; } = String.Empty;
        [Required]
        public int PointsToGain { get; set; }
        [Required]
        public int Category1Gain { get; set; }
        [Required]
        public int Category2Gain { get; set; }
        [Required]
        public int Category3Gain { get; set; }
        [Required]
        public int Category4Gain { get; set; }
    }
}
