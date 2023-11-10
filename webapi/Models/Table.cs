using System.Security.Cryptography;

namespace MP7_progi.Models
{
    //sluzi kao poveznica izmedju ranije kreiranih klasa kako bi se mogla instancirati bilo koja od njih prolazom kroz tablicu
    public abstract class Table : ITable
    {
        public readonly Dictionary<string, string> types;

        abstract public String returnTable();
        abstract public String returnColumnType(string column);
    }
    
    public interface ITable
    {
        public String returnTable();
        public String returnColumnType(string column);
    }
}
