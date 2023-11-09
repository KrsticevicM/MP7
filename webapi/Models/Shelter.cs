namespace MP7_progi.Models
{
    public class Shelter : Table
    {
        private readonly string _id = "Shelter";
        public int userID { get; set; }
        public string? nameShelter { get; set; }
        public Shelter() { }
        public String returnTable() { return _id; }
    }
}
