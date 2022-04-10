namespace Lucy3DLib
{
    /// <summary>
    /// CmdPrint - 
    /// </summary>
    class CmdPrint : Cmd
    {
        private const string COMMAND = "PRINT";

        private ParserTools parserTools = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public CmdPrint() : base(COMMAND)
        {
            parserTools = new ParserTools();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        public override Node Translate(Parser parser)
        {
            Token lastExpToken = Token.CreateBeginExpMarker();  //TODO : Change this for a new Token();

            NodePrint printNode = new NodePrint();

            while (!lastExpToken.IsEOS())
            {
                printNode.Add(parserTools.GetExpression(parser, out lastExpToken));
            }

            return (printNode);
        }
    }
}
