using System.Collections.Generic;

namespace Lucy3DLib
{
    /// <summary>
    /// ParseExpression - This class is used to evaluate expressions that are 
    /// in infix notation. This requires that operators are placed in between 
    /// all variables and constants. The expressions are evaluated one token 
    /// at a time. Each token is evaluated based on its type. If a token is 
    /// not recognized and cannot be processed then this is considered error.  
    /// All processing will discontinue, and a null is returned to the caller.
    /// </summary>
    class ParseExpression 
    {
        // Denotes that last token read that ended expresion parsing
        public Token LastToken { get; set; } = null;

        // Keeps track of parenthesis to make sure they are balanced
        private int balanceParen = 0;

        // Variable Stack
        private Stack<Node> varStack = null;

        // Operator Stack
        private Stack<Token> operStack = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public ParseExpression()
        {
            
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// Translate() - This function is used to parse expressions.  It 
        /// will read through an expression one token at a time and 
        /// depending on the token type will either process it through a 
        /// variable stack or operator stack. Constants, symbols, left and 
        /// right parentheses are all processed in the main while loop. If 
        /// a token is not recognized an error message is displayed.  If 
        /// there are no errors, a valid node is returned.  If there are 
        /// errors a null is returned.
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public Node Translate(Parser parser)
        {
            bool continueParsing = true;
            bool errors = false;
            Node expression = null;

            Initialize();

            Token token = parser.GetToken();

            while(!EndOfExpression(token) && continueParsing && !errors)
            {
                if (token.IsAConstant())
                {
                    token = PushConstStack(parser, token);
                } 
                else if (token.IsASymbol())
                {
                    token = PushVarStack(parser, token);
                }
                else if (token.IsBinaryOperator())
                {
                    token = PushOperStack(parser, token, false);
                }
                else if (token.IsLeftParen())
                {
                    token = ProcessLeftParen(parser, token);
                }
                else if (token.IsRightParen() && IsParenBalanced())
                {
                    continueParsing = false;
                }
                else if (token.IsRightParen())
                {
                    token = ProcessRightParen(parser);
                } else
                {
                    MsgSystem.GetInstance.Error(MsgCode.UNKNOW_EXPRESSION_TOKEN, parser, out errors);
                }
            }

            if (!errors)
            {
                EmptyOperStack();
                LastToken = token;
                expression = varStack.Peek();
            } else
            {
                expression = null;
            }

            return (expression);
        }

        /*************************/
        /*** Private Functions ***/
        /*************************/

        /// <summary>
        /// Initialize() - 
        /// </summary>
        private void Initialize()
        {
            balanceParen = 0;

            varStack = new Stack<Node>();
            operStack = new Stack<Token>();

            operStack.Push(Token.CreateOperStackMarker());
        }

        private Token ProcessLeftParen(Parser parser, Token token)
        {
            balanceParen++;

            return (PushOperStack(parser, token, true));
        }

        private Token ProcessRightParen(Parser parser)
        {
            balanceParen--;

            return (PopParenStack(parser));
        }

        /// <summary>
        /// IsParnAreBalance() - 
        /// </summary>
        /// <returns></returns>
        private bool IsParenBalanced()
        {
            return (balanceParen == 0);
        }

        /// <summary>
        /// EmptyOperStack() - Pops all of the operators off the operator stack
        /// as the last task to complete the expression translation.  The end 
        /// result of this function will have the final expression node as the
        /// only node left in the operator stack.
        /// </summary>
        private void EmptyOperStack()
        {
            while (!operStack.Peek().IsEndOperStack())
            {
                PopOperStack();
            }
        }

        /// <summary>
        /// PushOperStack() - Pushes an operator or a left paren on the operator
        /// stack.  If the token is a left paren, it is place on the stack
        /// without reguard to the current operator on top of the stack.  If
        /// the token is an operator, it is compared to the rank of the operator
        /// currently at the top of the stack.  
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="token"></param>
        /// <param name="leftParen"></param>
        /// <returns></returns>
        private Token PushOperStack(Parser parser, Token token, bool leftParen)
        {
            while (!leftParen && (token.Rank() <= operStack.Peek().Rank()))
            {
                PopOperStack();
            }

            operStack.Push(token);

            return (parser.GetToken());
        }

        /// <summary>
        /// PopParenStack() - 
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        private Token PopParenStack(Parser parser)
        {
            while(!operStack.Peek().IsLeftParen())
            {
                PopOperStack();
            }

            operStack.Pop();

            return (parser.GetToken());
        }

        /// <summary>
        /// PopOperStack() - 
        /// </summary>
        private void PopOperStack()
        {
            Node rValue = varStack.Pop();
            Node lValue = varStack.Pop();

            Token oper = operStack.Pop();

            varStack.Push(oper.CreateOperNode(lValue, rValue));
        }

        /// <summary>
        /// PushConstStack() - Places the constant token directly on the 
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private Token PushConstStack(Parser parser, Token token)
        {
            varStack.Push(token.CreateNodeValue());

            return (parser.GetToken()); // Return Next Lead Token
        }

        /// <summary>
        /// PushVarStack() - Pushes a variable/function on the variable stack.  
        /// A variable is considered a scalar, array or function call.  The
        /// NodeValue will contain all of the attributes and is pushed on 
        /// the variable stack.  The leading token is then returned to the
        /// caller.
        /// </summary>
        /// <param name="variable"></param>
        private Token PushVarStack(Parser parser, Token variable)
        {
            ParserTools parserTools = new ParserTools();

            Token token = null;

            Node node = null;

            if (parser.IsPeekLeftBracket())
            {
                NodeVariable value = variable.CreateNodeVariable();
                value.arrayElements = parserTools.GetArrayElements(parser, out Token leadingToken);
                token = leadingToken;
                node = value;
            } 
            else if (parser.IsPeekLeftParen())
            {
                NodeFunction value = variable.CreateNodeFunction();
                value.elements = parserTools.GetParmElements(parser, out Token leadingToken);
                token = leadingToken;
                node = value;
            } 
            else
            {
                token = parser.GetToken();
            }

            varStack.Push(node);

            return (token); // Return Next Lead Token
        }

        /// <summary>
        /// EndOfExpression() - Defines the token that will mark the end of an
        /// expression.  If the token is one of the end of expression markers,
        /// a true is retuned.  If it is not, a false is returned.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool EndOfExpression(Token token)
        {
            bool stopCharacter = 
                token.IsComma() || token.IsEOS() || token.IsRightBracket();

            return (stopCharacter);
        }
    }
}
