namespace Lucy3DLib
{
    /// <summary>
    /// NodeValue - This class defines the computational values calculated
    /// during execution of the program node tree.  It does not execute 
    /// a command, but is used by other commands when values are needed.
    /// </summary>
    class NodeValue : Node
    {
        private readonly SymbolTableRecData constant = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public NodeValue(long iValue)
        {
            this.constant = new SymbolTableRecData(iValue);
        }

        public NodeValue(string sValue)
        {
            this.constant = new SymbolTableRecData(sValue, VariableType.STRING);
        }

        public NodeValue(double fValue)
        {
            this.constant = new SymbolTableRecData(fValue);
        }

        public NodeValue(char cValue)
        {
            this.constant = new SymbolTableRecData(cValue);
        }

        public NodeValue(bool bValue)
        {
            this.constant = new SymbolTableRecData(bValue);
        }

        /***************************/
        /*** Predicate Functions ***/
        /***************************/

        public bool IsFloat() => (constant.IsFloat());

        public bool IsInteger() => (constant.IsInteger());

        public bool IsString() => (constant.IsString());

        public bool IsChar() => (constant.IsChar());

        public bool IsBoolean() => (constant.IsBoolean());

        /************************/
        /*** Getter Functions ***/
        /************************/

        public char GetChar() => constant.GetChar();

        public bool GetBoolean() => constant.GetBoolean();

        public double GetFloat() => constant.GetFloat();

        public string GetString() => constant.GetString();

        public long GetInteger() => constant.GetInteger();

        /****************/
        /*** Evaluate ***/
        /****************/

        public override NodeValue Evaluate(Context context)
        {
            return (this);
        }
    }
}
