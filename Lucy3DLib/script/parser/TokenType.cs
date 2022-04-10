namespace Lucy3DLib
{
    public enum TokenType
    {
        // Variable Types
        //---------------
        FLOAT,
        INTEGER,
        STRING,
        BOOLEAN,
        CHARACTER,

        OPER_STACK_MARKER,          // Used to mark the end of the expression operator stack
        BEGIN_EXP_MARKER,

        EOE,

        // Binary Operator Types
        //----------------------
        MULTIPLY,   // '*'
        DIVIDE,     // '/'
        SUBTRACT,   // '-'
        ADD,        // '+'

        POWER,      // '^'
        MOD,        // '%'

        // Logical Operator Types
        //-----------------------
        GT, // Logical Greater Than
        GE, // Logical Greater Than or Equal To
        LT, // Logical Less Than
        LE, // Logical Less Then or Equal To
        EQ, // Logical Equal
        NE, // Logical Not Equal

        AND,
        OR,
        NOT,
        UNARY_MINUS,

        NO_TOKEN,

        ASSIGN,

        SYMBOL,

        LEFT_PAREN,
        RIGHT_PAREN,
        LEFT_BRACE,
        RIGHT_BRACE,
        LEFT_BRACKET,
        RIGHT_BRACKET,
        FIELD_SEPARATOR,    // Field separator with expressions

        EOS,                // End of Statement token

        EOC,                // End of Code token

        // Token type has not yet been determined
        UNKNOWN
    }
}
