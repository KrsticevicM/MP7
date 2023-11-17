namespace MP7_progi.Models
{
    public class Expression
    {
        public enum OP
        {
            None,
            AND,
            OR,
            EQUAL,
            GE,
            LESS,
            LE,
            NOT,
            OPEN_B,
            CLOSE_B
        }

        Dictionary<OP, string> parms = new()
        {
            {OP.AND, "AND"},
            {OP.OR, "OR" },
            {OP.EQUAL, "="},
            {OP.GE, ">="},
            {OP.LESS, "<"},
            {OP.LE, "<="},
            {OP.NOT, "<>"},
            {OP.OPEN_B, "("},
            {OP.CLOSE_B, ")"}
        };

        private string expression = "";
        public Expression() { expression = ""; }

        public void addElement(Object? name, OP o)
        {
            if (name != null) expression += name.ToString() + " ";
            if (o != OP.None) expression += parms[o] + " ";
        }

        public string returnExpression()
        {
            return expression;
        }
    }
}
