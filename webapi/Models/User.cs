using System.ComponentModel.DataAnnotations;

namespace MP7_progi.Models
{
    public class User:Table
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }

        public string phoneNum { get; set; }
        public string pws { get; set; }
    }
}
