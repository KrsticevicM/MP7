namespace MP7_progi.Models
{
    public class Regular : Table
    {
        private readonly string _id = "Regular";
        public int userID { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public Regular() { }
        public String returnTable() { return _id; }
    }
}
