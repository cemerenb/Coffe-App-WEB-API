namespace cemerenbwebapi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Owner { get; set; } = string.Empty;
        public string Store { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
