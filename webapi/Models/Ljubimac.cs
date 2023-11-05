using System.ComponentModel.DataAnnotations;

namespace MP7_progi.Models
{
    public class Ljubimac:Table
    {
        public int petID { get; set; }
        public string? imeLjub { get; set; }
        public string? vrsta { get; set; }

        [DataType(DataType.Date)]
        public DateTime datSatNest { get; set; }
        public string lokacija { get; set; }

        public string? starost { get; set; }
        public string? boja { get; set; }
        public string? tekstOpis { get; set; }

        public int oglasID { get; set; }
    }
}
