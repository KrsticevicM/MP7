namespace MP7_progi.Models
{
    public class typeOfUser : Table
    {
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"userID", "int"},
            {"userType", "string"}
        };
        private readonly string _id = "typeOfUser";
        public int userID { get; set; }
        public string? userType { get; set; }

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
