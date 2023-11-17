using System.Security.Cryptography;

namespace MP7_progi.Models
{
    public class hasColor : Table
    {
        enum names
        {
            petID,
            colorID
        }
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"petID", "int"},
            {"colorID", "int" }
        };
        private readonly string _id = "has";
        public int petID { get; set; }
        public int colorID { get; set; }

        public hasColor() { }

        override
        public String returnTable()
        { return _id; }

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
