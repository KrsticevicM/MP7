

namespace MP7_progi.Models
{
    public class ColorPet : Table
    {
        private readonly string _id = "ColorPet";
        public int colorID { get; set; }
        public string color { get; set; }
        public ColorPet() { }
        public String returnTable() { return _id; }
    }
}
