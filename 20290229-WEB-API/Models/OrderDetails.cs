namespace cemerenbwebapi.Models
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int ProductQuantity { get; set; }
        public decimal Price { get; set; }
    }
}
