using System.Security.Cryptography;

namespace MP7_progi.Models
{
    //sluzi kao poveznica izmedju ranije kreiranih klasa kako bi se mogla instancirati bilo koja od njih prolazom kroz tablicu
    public abstract class Table : ITable
    {
        abstract public String returnTable();
        abstract public String returnColumnType(string column);
        abstract public Dictionary<string, string> returnColumnTypes();
    }
    
    public interface ITable
    {
        public String returnTable();
        public String returnColumnType(string column);

        public Dictionary<string, string> returnColumnTypes();
    }
}
