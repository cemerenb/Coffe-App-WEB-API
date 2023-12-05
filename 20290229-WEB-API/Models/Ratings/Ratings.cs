namespace Models.Rating
{
    public class Rating
    {
        public int Id { get; set; }
        public string RatingId { get; set; } = string.Empty;
        public string StoreEmail { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public int RatingPoint { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int IsRatingDisplayed { get; set; }
        public string RatingDisabledComment { get; set; } = string.Empty;
        public string RatingDate { get; set; } = string.Empty;
    }
}
