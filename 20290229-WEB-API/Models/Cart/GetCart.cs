using System.ComponentModel.DataAnnotations;

namespace Models.Order
{
    public class GetCart
    {
        [Required, EmailAddress]
        public string UserEmail { get; set; } = string.Empty;


    }
}
