namespace MP7_progi.Models
{
    public class photoAd : Table
    {
        enum names
        {
            photoID,
            adID,
            photo
        }
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"photoID", "int"},
            {"photo", "string"},
            {"adID", "int"}
        };
        private readonly string _id = "photoAd";
        public int photoID { get; set; }
        public int adID { get; set; }
        public string? photo { get; set; }
        public photoAd() { }

        override
        public String returnTable() { return _id; }

        override
        public String returnColumnType(string column)
        {
            return types[column];
        }
        override
        public Dictionary<string, string> returnColumnTypes()
        {
            return types;
        }
    }
}
