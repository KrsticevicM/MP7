namespace MP7_progi.Models
{
    public class Regular : Table
    {
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"userID", "int"},
            {"firstName", "string"},
            {"lastName", "string"}
        };
        private readonly string _id = "Regular";
        public int userID { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public Regular() { }

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
