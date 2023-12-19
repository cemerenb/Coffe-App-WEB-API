using System.ComponentModel.DataAnnotations;

namespace Models.Cart
{
    public class GetCart
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;


    }
}
