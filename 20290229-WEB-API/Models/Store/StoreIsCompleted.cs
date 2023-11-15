using System.ComponentModel.DataAnnotations;

namespace Models.Store
{
    public class StoreIsCompleted
    {
        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;
        
    }
}
