namespace MP7_progi.Models
{
    public class Ad : Table
    {
        private readonly string _id = "Ad";
        public int adID { get; set; }
        public string catAd { get; set; }
        public int userID { get; set; }
        public Ad() { }
        public String returnTable() { return _id; }
    }
}
