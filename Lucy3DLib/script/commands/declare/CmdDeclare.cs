namespace Lucy3DLib
{
    /// <summary>
    /// CmdDeclare - 
    /// </summary>
    class CmdDeclare : Cmd
    {
        private VariableType type = VariableType.UNKNOWN;

        private ParserTools parserTools = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public CmdDeclare(string command) : base(command)
        {
            Token token = Token.CreateSymbolToken(command);

            this.type = token.GetVariableType();
            this.parserTools = new ParserTools();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        public override Node Translate(Parser parser)
        {
            NodeDeclare nodeDeclare = new NodeDeclare(type);

            Token variable = new Token();
            Token token = null;

            while (!variable.IsEOS())
            {
                Node initialize = null;

                variable = parser.GetToken();

                ArrayElements arrayElements = parserTools.GetArrayElements(parser, out token);

                if (token.IsAssign())
                {
                    initialize = parserTools.GetExpression(parser, out token);
                } 
                
                nodeDeclare.Add(new NodeDeclareVar(type, variable, arrayElements, initialize));

                variable = token;
            }

            return (nodeDeclare);
        }
    }
}
