using System.ComponentModel.DataAnnotations;

namespace Models.Order
{
    public class UpdateOrderStatus
    {

        [Required]
        public string OrderId { get; set; } = string.Empty;

        [Required]
        public int OrderStatus { get; set; }
    }
}
