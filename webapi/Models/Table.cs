using System.Security.Cryptography;

namespace MP7_progi.Models
{
    //sluzi kao poveznica izmedju ranije kreiranih klasa kako bi se mogla instancirati bilo koja od njih prolazom kroz tablicu
    public interface Table
    {
        public String returnTable();
    }
}
