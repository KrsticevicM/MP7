namespace MP7_progi.Models
{
    public class Comment : Table
    {
        public readonly Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"textID", "string"},
            {"photoCom", "string"},
            {"textCom", "string"},
            {"locCon", "string"},
            {"adID", "int"},
            {"userID", "int"}
        };
        private readonly string _id = "Comment";
        public int textID { get; set; }
        public string? photoCom{ get; set; }
        public string? textCom { get; set; }
        public string? locCom { get; set; }
        public int adID { get; set; }
        public int userID { get; set; }
        public Comment() { }

        override
        public String returnTable() { return _id; }

        override
        public String returnColumnType(string column)
        {
            return types[column];
        }
    }
}
