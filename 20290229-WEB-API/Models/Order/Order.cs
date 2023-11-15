using System.ComponentModel.DataAnnotations;

namespace Models.Order
{
    public class Order
    {
        public int Id { get; set; }
        public string StoreEmail { get; set; } = string.Empty;
        public int OrderId { get; set; }

        public string UserEmail { get; set; } = string.Empty;
        public int OrderStatus { get; set; }
        public string OrderNote { get; set; } = string.Empty;

        public DateTime OrderCreatingTime { get; set; }

        public float OrderPrice { get; set; }
    }
}
