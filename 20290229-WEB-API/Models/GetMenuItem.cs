using System.ComponentModel.DataAnnotations;

namespace cemerenbwebapi.Models
{
    public class GetMenuItem
    {
        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;
        
    }
}
