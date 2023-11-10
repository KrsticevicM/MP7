

using System.Drawing;

namespace MP7_progi.Models
{
    public class ColorPet : Table
    {
        public readonly Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"colorID", "int"},
            {"color", "string"}
        };

        private readonly string _id = "ColorPet";
        public int colorID { get; set; }
        public string color { get; set; }
        public ColorPet() { }

        override
        public String returnTable() { return _id; }

        override
        public String returnColumnType(string column)
        {
            return types[column];
        }
    }
}
