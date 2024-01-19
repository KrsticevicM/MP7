namespace MP7_progi.Models
{
    public class Comment : Table
    {
        enum names
        {
            textID,
            photoCom,
            textCom,
            latCom,
            lonCom,
            adID,
            userID
        }
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"textID", "int"},
            {"photoCom", "string"},
            {"textCom", "string"},
            {"latCom", "string"},
            {"lonCom", "string"},
            {"adID", "int"},
            {"userID", "int"}
        };
        private readonly string _id = "Communication";
        public int textID { get; set; }
        public string? photoCom{ get; set; }
        public string? textCom { get; set; }
        public string? latCom { get; set; }
        public string? lonCom { get; set; }
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
        override
        public Dictionary<string, string> returnColumnTypes()
        {
            return types;
        }
    }
}
