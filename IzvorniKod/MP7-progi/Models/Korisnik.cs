using System.ComponentModel.DataAnnotations;

namespace MP7_progi.Models
{
    public class Korisnik
    {
        public int userID { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }

        public string email { get; set; }
        public string telBroj { get; set; }

        public string lozinka { get; set; }
        public string korIme { get; set; }
        public string? nazSklon { get; set; }
    }
}
