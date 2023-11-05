namespace MP7_progi.Models
{
    public class Poruka:Table
    {
        public int porID { get; set; }
        public string? lokPor{ get; set; }
        public string? slikaPor { get; set; }
        public string? tekstPor { get; set; }
        public int oglasID { get; set; }
        
    }
}
