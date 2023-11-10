namespace MP7_progi.Models
{
    public class photoAd : Table
    {
        public readonly Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"photoID", "int"},
            {"adID", "int"},
            {"photo", "string"}
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
    }
}
