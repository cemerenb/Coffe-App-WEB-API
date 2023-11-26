using System.ComponentModel.DataAnnotations;

namespace Models.OrderDetail
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string StoreEmail { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        public int ItemCount { get; set; }
        public int ItemCanceled {  get; set; }
        public string CancelNote {  get; set; } = string.Empty;

    }
}
