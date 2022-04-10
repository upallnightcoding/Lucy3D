using Lucy3DLib;

namespace Lucy3DLib
{
    /// <summary>
    /// Adding New Binary Operators - Update the following functions
    /// ------------------------------------------------------------
    /// CreateNodeOperator() - Create the node based on token type
    /// Rank() - Defines the token ranking for expression evaluation
    /// IsOperator() - Defines the token as an operator
    /// *Create the node class that will execute the action
    /// 
    /// </summary>
    class Token
    {
        // Default Token Type
        //-------------------
        private TokenType type = TokenType.UNKNOWN;

        // Containers for each of the possible token types
        //------------------------------------------------
        private long iValue = 0;
        private string sValue = null;
        private double fValue = 0;
        private char cValue = ' ';
        private bool bValue = false;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public Token()
        {
            this.type = TokenType.UNKNOWN;
        }

        public Token(long iValue)
        {
            this.type = TokenType.INTEGER;
            this.iValue = iValue;
        }

        public Token(double fValue)
        {
            this.type = TokenType.FLOAT;
            this.fValue = fValue;
        }

        public Token(TokenType type)
        {
            this.type = type;
        }

        private Token(TokenType type, string text)
        {
            this.type = type;
            this.sValue = text;
        }

        private Token(char cValue)
        {
            this.type = TokenType.CHARACTER;
            this.cValue = cValue;
        }

        /// <summary>
        /// GetKeyword() - Returns the string as a Script keyword.  Keywords
        /// are converted to upper case for comparison.  Keywords are not
        /// case sensitive.
        /// </summary>
        /// <returns></returns>
        public string GetKeyword()
        {
            return (sValue.ToUpper());
        }

        /// <summary>
        /// GetVariable() - Returns the string as a Script variable.  Since
        /// variables are case sensitive, they are not converted to uppercase
        /// while doing comparisons.  
        /// </summary>
        /// <returns></returns>
        public string GetVariable()
        {
            return (sValue);
        }

        /*******************************/
        /*** Public Static Functions ***/
        /*******************************/

        public static Token CreateEOCToken() => (new Token(TokenType.EOC));

        public static Token CreateStringToken(string text) => (new Token(TokenType.STRING, text));

        public static Token CreateSymbolToken(string symbolName) => (new Token(TokenType.SYMBOL, symbolName));

        public static Token CreateCharToken(char character) => (new Token(character));

        public static Token CreateOperStackMarker() => (new Token(TokenType.OPER_STACK_MARKER));

        public static Token CreateBeginExpMarker() => (new Token(TokenType.BEGIN_EXP_MARKER));

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// GetVariableType() - 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public VariableType GetVariableType()
        {
            VariableType type = VariableType.UNKNOWN;

            switch (sValue.ToUpper())
            {
                case "INTEGER":
                    type = VariableType.INTEGER;
                    break;
                case "FLOAT":
                    type = VariableType.FLOAT;
                    break;
                case "BOOLEAN":
                    type = VariableType.BOOLEAN;
                    break;
                case "STRING":
                    type = VariableType.STRING;
                    break;
                case "CHARACTER":
                    type = VariableType.CHARACTER;
                    break;
            }

            return (type);
        }

        /// <summary>
        /// CreateOperNode() - Based on the token type, a node is created to
        /// represent the action to be taken during execution.  This function 
        /// works for binary operators.  
        /// </summary>
        /// <param name="leftValue"></param>
        /// <param name="rightValue"></param>
        /// <returns></returns>
        public Node CreateOperNode(Node leftValue, Node rightValue)
        {
            Node node = null;

            switch(type)
            {
                case TokenType.ADD:
                    node = new NodeAdd(leftValue, rightValue);
                    break;
                case TokenType.SUBTRACT:
                    node = new NodeSubtract(leftValue, rightValue);
                    break;
                case TokenType.MULTIPLY:
                    node = new NodeMultiply(leftValue, rightValue);
                    break;
                case TokenType.DIVIDE:
                    node = new NodeDivide(leftValue, rightValue);
                    break;
                case TokenType.EQ:
                    node = new NodeEQ(leftValue, rightValue);
                    break;
                case TokenType.NE:
                    node = new NodeNE(leftValue, rightValue);
                    break;
                case TokenType.LT:
                    node = new NodeLT(leftValue, rightValue);
                    break;
                case TokenType.GT:
                    node = new NodeGT(leftValue, rightValue);
                    break;
                case TokenType.GE:
                    node = new NodeGE(leftValue, rightValue);
                    break;
                case TokenType.LE:
                    node = new NodeLE(leftValue, rightValue);
                    break;
            }

            return (node);
        }

        /// <summary>
        /// CreateNodeValue() - 
        /// </summary>
        /// <returns></returns>
        public NodeValue CreateNodeValue()
        {
            NodeValue node = null;

            switch(type)
            {
                case TokenType.FLOAT:
                    node = new NodeValue(fValue);
                    break;
                case TokenType.INTEGER:
                    node = new NodeValue(iValue);
                    break;
                case TokenType.STRING:
                    node = new NodeValue(sValue);
                    break;
                case TokenType.CHARACTER:
                    node = new NodeValue(cValue);
                    break;
                case TokenType.BOOLEAN:
                    node = new NodeValue(bValue);
                    break;
            }

            return (node);
        }

        public NodeVariable CreateNodeVariable()
        {
            NodeVariable node = null;

            switch (type)
            {
                case TokenType.SYMBOL:
                    node = new NodeVariable(sValue);
                    break;
            }

            return (node);
        }

        public NodeFunction CreateNodeFunction()
        {
            NodeFunction node = null;

            switch (type)
            {
                case TokenType.SYMBOL:
                    node = new NodeFunction(sValue);
                    break;
            }

            return (node);
        }

        /// <summary>
        /// Rank() - Returns the ranking value that is used during the 
        /// postfix parsing of expressions.  This ranking defines the 
        /// order of operation of the different operators as the postfix
        /// is being created.  All operators must be represented in this
        /// function.
        /// </summary>
        /// <returns></returns>
        public int Rank()
        {
            int value = -1;

            switch(type)
            {
                case TokenType.MULTIPLY:
                case TokenType.DIVIDE:
                    value = 20;
                    break;
                case TokenType.ADD:
                case TokenType.SUBTRACT:
                    value = 10;
                    break;
                case TokenType.EQ:
                case TokenType.NE:
                case TokenType.LT:
                case TokenType.LE:
                case TokenType.GT:
                case TokenType.GE:
                    value = 5;
                    break;
                case TokenType.LEFT_PAREN:
                    value = 1;
                    break;
                case TokenType.OPER_STACK_MARKER:
                    value = 0;
                    break;
            }

            return (value);
        }

        /***************************/
        /*** Predicate Functions ***/
        /***************************/

        public bool IsEOC() => (type == TokenType.EOC);

        public bool IsEOS() => (type == TokenType.EOS);

        public bool IsComma() => (type == TokenType.FIELD_SEPARATOR);

        public bool IsEndOperStack() => (type == TokenType.OPER_STACK_MARKER);

        public bool IsRightBrace() => (type == TokenType.RIGHT_BRACE);

        public bool IsLeftBrace() => (type == TokenType.LEFT_BRACE);

        public bool IsRightParen() => (type == TokenType.RIGHT_PAREN);

        public bool IsASymbol() => (type == TokenType.SYMBOL);

        public bool IsAssign() => (type == TokenType.ASSIGN);

        public bool IsLeftBracket() => (type == TokenType.LEFT_BRACKET);

        public bool IsRightBracket() => (type == TokenType.RIGHT_BRACKET);

        public bool IsLeftParen() => (type == TokenType.LEFT_PAREN);

        /// <summary>
        /// IsOperator() - Returns a true if the token is a binary operator.  If
        /// is not, a false is returned.
        /// </summary>
        /// <returns></returns>
        public bool IsBinaryOperator()
        {
            bool value = false;

            switch(type)
            {
                case TokenType.MULTIPLY:
                case TokenType.DIVIDE:
                case TokenType.SUBTRACT:
                case TokenType.ADD:
                case TokenType.EQ:
                case TokenType.NE:
                case TokenType.LT:
                case TokenType.LE:
                case TokenType.GT:
                case TokenType.GE:
                    value = true;
                    break;
            }

            return (value);
        }

        /// <summary>
        /// IsAConstant() - 
        /// </summary>
        /// <returns></returns>
        public bool IsAConstant()
        {
            bool value = false;

            switch(type)
            {
                case TokenType.FLOAT:
                case TokenType.INTEGER:
                case TokenType.STRING:
                case TokenType.CHARACTER:
                    value = true;
                    break;
            }

            return (value);
        }
       
    }
}
