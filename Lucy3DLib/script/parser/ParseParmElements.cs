using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class ParseParmElements
    {
        private Token token = null;
        private ParseExpression expression = null;
        private ParmElements parmElements = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public ParseParmElements()
        {
            this.expression = new ParseExpression();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        public ParmElements Translate(Parser parser)
        {
            token = parser.GetToken(); 

            if (token.IsLeftParen())
            {
                if (parser.IsPeekRightParen())
                {
                    parmElements = null;
                    token = parser.GetToken();  // Parser Right Paren
                    token = parser.GetToken();  // Parser Leading Token
                }
                else
                {
                    parmElements = new ParmElements();

                    while (!token.IsRightParen())
                    {
                        parmElements.Add(expression.Translate(parser));

                        token = expression.LastToken;
                    }

                    token = parser.GetToken();
                }
            }

            return (parmElements);
        }

        public Token LeadingToken()
        {
            return (token);
        }
    }
}
