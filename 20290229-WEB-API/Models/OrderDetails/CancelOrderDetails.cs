using System.ComponentModel.DataAnnotations;

namespace Models.OrderDetail
{
    public class CancelOrderDetails
    {
        
        public string AccessToken { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string MenuItemId { get; set; } = string.Empty;
        public int ItemCanceled { get; set; }
        public string CancelNote { get; set; } = string.Empty;
    }
}
