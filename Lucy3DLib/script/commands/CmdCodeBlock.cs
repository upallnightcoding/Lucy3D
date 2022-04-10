namespace Lucy3DLib
{
    /// <summary>
    /// CmdCodeBlock - This class manages the parsing of a codeblock. A
    /// code block is any set of commands that are between a left and
    /// right brace.  Code blocks can be indented with each indentation
    /// creating a nested code block node.
    /// 
    /// BNF:
    /// <CodeBlock> = { <Commands> }
    /// 
    /// </summary>
    class CmdCodeBlock : Cmd
    {
        public CmdCodeBlock() : base(null)
        {

        }

        /// <summary>
        /// Translate() - Starts reading a code block by parsing the first
        /// left brace and then recursively reading each code block.  This
        /// function only ready one code block at the current level.
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public override Node Translate(Parser parser)
        { 
            Token leftBrace = parser.GetToken();

            return (ParseCodeBlock(parser));
        }

        /// <summary>
        /// ParseCodeBlock() - Translates a code block that starts with a "{"
        /// and ends with a "}".  Each command inside the coding block is 
        /// translated as long as it is part of the command factory.  If the
        /// code block are nested the code block nodes are also nested.
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        private Node ParseCodeBlock(Parser parser)
        {
            // Create a code block for this level of indentation
            //--------------------------------------------------
            NodeCodeBlock codeBlock = new NodeCodeBlock();

            Token token = parser.GetToken();

            Cmd command = CmdFactory.INSTANCE.GetCommand(token);

            // Loop until the end of the code block
            //-------------------------------------
            while (!(EndOfCodeBlock(token)))
            {
                Node node = command.Translate(parser);

                codeBlock.Add(node);

                token = parser.GetToken();

                // Create a new code block for a new level of indentation
                //-------------------------------------------------------
                if (token.IsLeftBrace())
                {
                    codeBlock.Add(ParseCodeBlock(parser));
                }   

                // If not the end of code block continue reading commands
                //-------------------------------------------------------
                else if (!EndOfCodeBlock(token))
                {
                    command = CmdFactory.INSTANCE.GetCommand(token);
                }
            }

            return (codeBlock);
        }

        /// <summary>
        /// EndOfCodeBlock() - Returns true if the token has reached the end of
        /// the source code or has found a right brace.  The EOC would single 
        /// that there are no more source code lineto parse.  And the 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool EndOfCodeBlock(Token token)
        {
            return (token.IsEOC() || token.IsRightBrace());
        }
    }

}
