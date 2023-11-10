namespace MP7_progi.Models
{
    public class Shelter : Table
    {
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"userID", "int"},
            {"nameShelter", "string"}
        };
        private readonly string _id = "Shelter";
        public int userID { get; set; }
        public string? nameShelter { get; set; }
        public Shelter() { }

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
