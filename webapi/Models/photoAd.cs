namespace MP7_progi.Models
{
    public class photoAd : Table
    {
        private readonly string _id = "photoAd";
        public int photoID { get; set; }
        public int adID { get; set; }
        public string? photo { get; set; }
        public photoAd() { }
        public String returnTable() { return _id; }
    }
}
