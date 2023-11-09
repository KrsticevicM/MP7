namespace MP7_progi.Models
{
    public class photoAd:Table
    { 
        // cak mozda ne treba userID?
        public int photoID { get; set; }
        public int adID { get; set; }
        public string? photo { get; set; }
        
    }
}
