using System.ComponentModel.DataAnnotations;

namespace MP7_progi.Models
{
    public class Pet : Table
    {
        public enum names
        {
            petID,
            namePet,
            species,
            age,
            description,
            adID
        }
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"petID", "int"},
            {"namePet", "string"},
            {"species", "string"},
            {"age", "string"},
            {"description", "string"},
            {"adID", "int"}
        };
        private readonly string _id = "Pet";
        public int petID { get; set; }
        public string? namePet { get; set; }
        public string? species { get; set; }
        public string? age { get; set; }
        public string? description { get; set; }
        public int adID { get; set; }
        public Pet() { }

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
