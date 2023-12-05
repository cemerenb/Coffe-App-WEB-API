namespace Models.PointRule
{
    public class PointRule
    {
        public int Id { get; set; }
        public int IsPointsEnabled { get; set; }
        public String StoreEmail { get; set; } = String.Empty;
        public int PointsToGain { get; set; }
        public int Category1Gain { get; set; }
        public int Category2Gain { get; set;}
        public int Category3Gain { get; set;}
        public int Category4Gain { get; set;}
    }
}
