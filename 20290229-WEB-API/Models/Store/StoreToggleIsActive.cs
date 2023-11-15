using System.ComponentModel.DataAnnotations;

namespace Models.Store
{
    public class StoreToggleIsOn
    {


        [Required]
        public int StoreIsOn { get; set; }

        [Required, EmailAddress]
        public string StoreEmail { get; set; } = string.Empty;

         




    }
}
