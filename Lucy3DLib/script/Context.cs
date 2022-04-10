namespace Lucy3DLib
{
    class Context
    {
        private SymbolTable symbolTable = null;

        public Context()
        {
            symbolTable = new SymbolTable();
        }

        public SymbolTable GetSymbolTable()
        {
            return (symbolTable);
        }
    }
}
