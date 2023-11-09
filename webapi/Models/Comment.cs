namespace MP7_progi.Models
{
    public class Comment:Table
    {
        public int textID { get; set; }
        public string? photoCom{ get; set; }
        public string? textCom { get; set; }
        public string? locCom { get; set; }
        public int adID { get; set; }
        public int userID { get; set; }
        
    }
}
