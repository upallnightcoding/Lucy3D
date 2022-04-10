namespace Lucy3DLib
{
    /// <summary>
    /// ParserTools - This class contains tools that assist with parsing.
    /// </summary>
    class ParserTools
    {
        /// <summary>
        /// GetExpression() - 
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="LastExpToken"></param>
        /// <returns></returns>
        public Node GetExpression(Parser parser, out Token LastExpToken)
        {
            ParseExpression expression = new ParseExpression();

            Node node = expression.Translate(parser);

            LastExpToken = expression.LastToken;

            return (node);
        }

        /// <summary>
        /// GetArrayElements() - 
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="NextLeadingToken"></param>
        /// <returns></returns>
        public ArrayElements GetArrayElements(Parser parser, out Token NextLeadingToken)
        {
            ParseArrayElements parseArrayElements = new ParseArrayElements();

            ArrayElements arrayElements = parseArrayElements.Translate(parser);

            NextLeadingToken = parseArrayElements.LeadingToken();

            return (arrayElements);
        }

        public ParmElements GetParmElements(Parser parser, out Token NextLeadingToken)
        {
            ParseParmElements parseParmElements = new ParseParmElements();

            ParmElements parmElements = parseParmElements.Translate(parser);

            NextLeadingToken = parseParmElements.LeadingToken();

            return (parmElements);
        }
    }
}
