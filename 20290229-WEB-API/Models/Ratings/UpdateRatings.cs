namespace Models.Store
{
    public class UpdateRatings
    {
        public string RatingId { get; set; }
        public string StoreEmail { get; set; } = string.Empty;
        public int IsRatingDisplayed { get; set; }
        public string RatingDisabledComment { get; set; } = string.Empty;
    }
}
