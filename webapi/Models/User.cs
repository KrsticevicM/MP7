using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MP7_progi.Models
{
    public class User : Table
    {
        private readonly string _id = "User";
        public int userID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }

        public string phoneNum { get; set; }
        public string pws { get; set; }

        public List<Object> rows { get; }

        public User()
        {
            rows = new List<Object>();
        }

        public String returnTable() { return _id; }
    }
}
