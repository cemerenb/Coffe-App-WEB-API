using System.ComponentModel.DataAnnotations;

namespace Models.Order
{
    public class Order
    {
        public int Id { get; set; }
        public string StoreEmail { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;
        public int OrderStatus { get; set; }
        public string OrderNote { get; set; } = string.Empty;

        public string OrderCreatingTime { get; set; } = string.Empty;
        public int ItemCount { get; set; }
        public double OrderTotalPrice { get; set; }
    }
}
