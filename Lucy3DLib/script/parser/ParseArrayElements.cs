namespace Lucy3DLib
{
    class ParseArrayElements
    {
        private Token token = null;
        private ArrayElements arrayElements = null;
        private ParseExpression expression = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public ParseArrayElements()
        {
            this.expression = new ParseExpression();
        }

        /************************/
        /*** Public FUnctions ***/
        /************************/

        /// <summary>
        /// Translate() - Read the leading token and determines if it is a
        /// left bracket.  If it is, then this is an array definition.
        /// Each element of the array definition is translated into a node
        /// and kept as an array element.  Parsing continues until a right
        /// brace is parsed marking the end of the array definition.
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public ArrayElements Translate(Parser parser)
        {
            token = parser.GetToken();  

            if (token.IsLeftBracket())
            {
                arrayElements = new ArrayElements();

                while (!token.IsRightBracket())
                {
                    arrayElements.Add(expression.Translate(parser));

                    token = expression.LastToken;
                }

                token = parser.GetToken();
            }

            return (arrayElements);
        }

        /// <summary>
        /// LeadingToken() - Returns the token that ended the array element
        /// parsing.  
        /// </summary>
        /// <returns></returns>
        public Token LeadingToken()
        {
            return (token);
        }
    }
}
