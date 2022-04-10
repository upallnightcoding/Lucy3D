using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class Parser
    {
        private static readonly char FLOATING_POINT_MARK = '.';
        private static readonly char DOUBLE_QUOTE = '\"';
        private static readonly char BACK_TICK = '`';
        private static readonly char ZERO_CHAR = '0';
        private static readonly char LEFT_BRACKET = '[';
        private static readonly char LEFT_PAREN = '(';
        private static readonly char RIGHT_PAREN = ')';

        // Reference to source code object
        private SourceCode source = null;

        private Dictionary<char, SimpleToken> simpleTokens = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public Parser(SourceCode source)
        {
            this.source = source;
            this.source.Reset();
            this.simpleTokens = new Dictionary<char, SimpleToken>();

            CreateSimpleTokens();

            SkipBlanks();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// GetToken() - 
        /// </summary>
        /// <returns></returns>
        public Token GetToken()
        {
            Token token = null;

            if (NotEoc())
            {
                char character = PeekChar();

                if (IsDigit(character) || (character == FLOATING_POINT_MARK))
                {
                    token = GetNumber();
                } 
                else if (IsLetter(character))
                {
                    token = GetKeyWord();
                } 
                else if (IsSimpleToken(character))
                {
                    token = GetSimpleToken();
                } 
                else if (IsString(character))
                {
                    token = GetString(character);
                } 
                else if (character == '\'')
                {
                    token = GetChar();
                }

                SkipBlanks();
            } else
            {
                token = Token.CreateEOCToken();
            }

            return (token);
        }

        public bool IsPeekLeftBracket() => (PeekChar() == LEFT_BRACKET);

        public bool IsPeekLeftParen() => (PeekChar() == LEFT_PAREN);

        public bool IsPeekRightParen() => (PeekChar() == RIGHT_PAREN);

        public void GetParserInfo(out int lineNumber, out string sourceLine)
        {
            lineNumber = source.LineNumber;
            sourceLine = source.SourceLine;
        }

        /*************************/
        /*** Private Functions ***/
        /*************************/

        /// <summary>
        /// GetChar() - Returns the single character that is determine
        /// between single quotes.  This function then moves off of the
        /// active character and then moves off of the ending single 
        /// quote.
        /// </summary>
        /// <returns></returns>
        private Token GetChar()
        {
            // Return the single character
            char character = GetNextChar();

            MoveNextChar();     // Move off of active character
            MoveNextChar();     // Move off of single quote

            return (Token.CreateCharToken(character));
        }

        /// <summary>
        /// CreateSimpleTokens() - Parses the one/two character tokens that
        /// are used by the scripting language.  
        /// </summary>
        private void CreateSimpleTokens()
        {
            CreateSimpleTokens(',', ' ', TokenType.FIELD_SEPARATOR, TokenType.NO_TOKEN);
            CreateSimpleTokens(';', ' ', TokenType.EOS,             TokenType.NO_TOKEN);
            CreateSimpleTokens('(', ' ', TokenType.LEFT_PAREN,      TokenType.NO_TOKEN);
            CreateSimpleTokens(')', ' ', TokenType.RIGHT_PAREN,     TokenType.NO_TOKEN);
            CreateSimpleTokens('{', ' ', TokenType.LEFT_BRACE,      TokenType.NO_TOKEN);
            CreateSimpleTokens('}', ' ', TokenType.RIGHT_BRACE,     TokenType.NO_TOKEN);
            CreateSimpleTokens('[', ' ', TokenType.LEFT_BRACKET,    TokenType.NO_TOKEN);
            CreateSimpleTokens(']', ' ', TokenType.RIGHT_BRACKET,   TokenType.NO_TOKEN);

            CreateSimpleTokens('+', ' ', TokenType.ADD,             TokenType.NO_TOKEN);
            CreateSimpleTokens('-', ' ', TokenType.SUBTRACT,        TokenType.NO_TOKEN);
            CreateSimpleTokens('*', ' ', TokenType.MULTIPLY,        TokenType.NO_TOKEN);
            CreateSimpleTokens('/', ' ', TokenType.DIVIDE,          TokenType.NO_TOKEN);
            CreateSimpleTokens('^', ' ', TokenType.POWER,           TokenType.NO_TOKEN);
            CreateSimpleTokens('%', ' ', TokenType.MOD,             TokenType.NO_TOKEN);

            CreateSimpleTokens('<', '=', TokenType.LT,              TokenType.LE);
            CreateSimpleTokens('>', '=', TokenType.GT,              TokenType.GE);
            CreateSimpleTokens('&', '&', TokenType.NO_TOKEN,        TokenType.AND);
            CreateSimpleTokens('|', '|', TokenType.NO_TOKEN,        TokenType.OR);
            CreateSimpleTokens('=', '=', TokenType.ASSIGN,          TokenType.EQ);
            CreateSimpleTokens('!', '=', TokenType.NOT,             TokenType.NE);

            CreateSimpleTokens('~', ' ', TokenType.UNARY_MINUS,     TokenType.NO_TOKEN);
        }

        /// <summary>
        /// CreateSimpleTokens() - 
        /// </summary>
        /// <param name="firstToken"></param>
        /// <param name="secondToken"></param>
        /// <param name="firstTokenType"></param>
        /// <param name="secondTokenType"></param>
        private void CreateSimpleTokens(
            char firstToken, 
            char secondToken, 
            TokenType firstTokenType, 
            TokenType secondTokenType
        )
        {
            simpleTokens[firstToken] = 
                new SimpleToken(firstToken, secondToken, firstTokenType, secondTokenType);
        }

        /******************************/
        /*** Simple Token Functions ***/
        /******************************/

        private bool IsSimpleToken(char character)
        {
            return (simpleTokens.ContainsKey(character));
        }

        private Token GetSimpleToken()
        {
            SimpleToken type;
            Token token = null;
            char character = PeekChar();

            if (simpleTokens.TryGetValue(character, out type))
            {
                if (type.IsFirstNoToken())
                {
                    character = GetNextChar();

                    if (type.IsEqualSecondToken(character))
                    {
                        token = new Token(type.SecondTokenType);

                        MoveNextChar();
                    }
                    else
                    {
                        token = new Token();
                    }
                }
                else if (type.IsSecondNoToken())
                {
                    token = new Token(type.FirstTokenType);

                    MoveNextChar();
                }
                else
                {
                    character = GetNextChar();

                    if (type.IsEqualSecondToken(character))
                    {
                        token = new Token(type.SecondTokenType);

                        MoveNextChar();
                    } else
                    {
                        token = new Token(type.FirstTokenType);
                    }

                }
            } else
            {
                token = new Token();
            }

            return (token);
        }

        /// <summary>
        /// GetKeyWord() - Parses a keyword token.  A Keyword token begins 
        /// with a letter and follows with any combination of letter or 
        /// digits.  A "_" can be used to define a variable name, but it
        /// can not be the first character of the name.
        /// </summary>
        /// <returns></returns>
        private Token GetKeyWord()
        {
            StringBuilder keyword = new StringBuilder();

            char character = PeekChar();

            while (NotEoc() && (IsAlphaNumeric(character))) 
            {
                keyword.Append(character);

                character = GetNextChar();
            }

            return (Token.CreateSymbolToken(keyword.ToString()));
        }

        /// <summary>
        /// GetInteger() - 
        /// </summary>
        /// <returns></returns>
        private Token GetNumber()
        {
            Token token = null;

            long lValue = GetInteger();

            if (NotEoc() && IsPeek(FLOATING_POINT_MARK))
            {
                token = new Token(GetFloat(lValue));
            } else
            {
                token = new Token(lValue);
            }

            return (token);
        }

        private Token GetString(char stringStop)
        {
            StringBuilder value = new StringBuilder();

            char character = GetNextChar();

            while (NotEoc() && (character != stringStop))
            {
                value.Append(character);

                character = GetNextChar();
            }

            MoveNextChar();

            return (Token.CreateStringToken(value.ToString()));
        }

        /// <summary>
        /// GetInteger() - Reads the next integer values from the active source
        /// code.  The actual calculaed value of the integer is returned.  The 
        /// first non-digit character will stop the parsing. 
        /// </summary>
        /// <returns></returns>
        private long GetInteger()
        {
            long value = 0;
            char character = PeekChar();

            while (NotEoc() && IsDigit(character))
            {
                value = value * 10 + (character - ZERO_CHAR);

                character = GetNextChar();
            }

            return (value);
        }

        /// <summary>
        /// GetFloat() - Reads the floating point value of a 
        /// </summary>
        /// <param name="whole"></param>
        /// <returns></returns>
        private double GetFloat(long whole)
        {
            double value = whole;
            double factor = 0.1;
            char character = PeekChar();

            while (NotEoc() && (character == ZERO_CHAR))
            {
                factor /= 10.0;

                character = GetNextChar();
            }

            while (NotEoc() && IsDigit(character))
            {
                value = value + factor * (character - ZERO_CHAR);
                factor /= 10.0;

                character = GetNextChar();
            }

            return (value);
        }

        /// <summary>
        /// SkipBlanks() - Skips all blanks
        /// </summary>
        private void SkipBlanks()
        {
            bool skipping = true;

            while(NotEoc() && skipping)
            {
                char character = PeekChar();

                if (IsWhiteSpace(character))
                {
                    MoveNextChar();
                } else
                {
                    skipping = false;
                }
            }
        }

        /// <summary>
        /// IsPeek() - Investigates the active source code character to
        /// determine its value.  If the value is a specific character, the
        /// character is skipped and a true is returned.  If the character
        /// is not specific, this function returns false.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private bool IsPeek(char character)
        {
            // Check the value of the current active source character
            //-------------------------------------------------------
            bool skip = (character == PeekChar());

            // Skip the charater if it exists
            //-------------------------------
            if (skip)
            {
                MoveNextChar();
            }

            return (skip);
        }

        /// <summary>
        /// PeekChar() - Returns the active source character.  If it is end of 
        /// code then a default character is return upon invoke.
        /// </summary>
        /// <returns></returns>
        private char PeekChar()
        {
            char character = (char) 200;

            if (NotEoc())
            {
                character = source.PeekChar();
            }

            return (character);
        }

        /// <summary>
        /// MoveNextChar() - Moves to the next active character but checks to
        /// make sure the EOC has not been reached.  If the EOC is true, the 
        /// pointer is not moved.
        /// </summary>
        private void MoveNextChar()
        {
            if (NotEoc())
            {
                source.MoveNextChar();
            }
        }

        /// <summary>
        /// GetNextChar() - 
        /// </summary>
        /// <returns></returns>
        private char GetNextChar()
        {
            MoveNextChar();

            return (PeekChar());
        }

        private bool IsWhiteSpace(char character) => char.IsWhiteSpace(character);

        private bool IsDigit(char character) => char.IsDigit(character);

        private bool IsLetter(char character) => char.IsLetter(character);

        private bool IsAlphaNumeric(char character) => char.IsLetterOrDigit(character);

        private bool NotEoc() => !source.Eoc();

        private bool IsString(char character) => (character == DOUBLE_QUOTE) || (character == BACK_TICK);

    }
}
