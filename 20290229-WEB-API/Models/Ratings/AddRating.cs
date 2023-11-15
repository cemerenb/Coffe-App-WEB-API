namespace Models.Store
{
    public class AddRatings
    {
        public string RatingId { get; set; } = string.Empty;
        public string StoreEmail { get; set; } = string.Empty;
        public string StoreName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public int RatingPoint { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string RatingDate { get; set; } = string.Empty;
    }
}
