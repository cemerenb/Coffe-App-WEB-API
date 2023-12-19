using System.ComponentModel.DataAnnotations;

namespace Models.Token
{
    public class UpdateAccessToken
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
        

    }
}
