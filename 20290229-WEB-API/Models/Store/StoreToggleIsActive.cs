using System.ComponentModel.DataAnnotations;

namespace Models.Store
{
    public class StoreToggleIsOn
    {


        [Required]
        public int StoreIsOn { get; set; }

        [Required]
        public string AccessToken { get; set; } = string.Empty;

         




    }
}
