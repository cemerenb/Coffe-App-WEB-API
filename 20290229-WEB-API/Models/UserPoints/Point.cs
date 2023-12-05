namespace Models.Point
{
    public class Point
{
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string StoreEmail { get; set; } = string.Empty;

        public int TotalPoint {  get; set; }
        public int TotalGained {  get; set; }
}
}
