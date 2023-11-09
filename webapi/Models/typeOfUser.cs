namespace MP7_progi.Models
{
    public class typeOfUser : Table
    {
        private readonly string _id = "Shelter";
        public int userID { get; set; }
        public string? userType { get; set; }
        public String returnTable() { return _id; }
    }
}
