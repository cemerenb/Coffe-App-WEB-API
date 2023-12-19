using System.ComponentModel.DataAnnotations;

namespace Models.Point
{
    public class CreateUserPointData
{
        [Required]
        public string AccessToken { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        public int TotalPoint { get; set; }
        [Required]
        public int TotalGained { get; set; }
    }
}
