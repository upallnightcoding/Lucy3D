using System;
using System.Collections.Generic;

namespace Lucy3DLib
{
    class MsgSystem
    {
        private static MsgSystem INSTANCE = null;

        private Dictionary<MsgCode, string> message = null;

        private MsgSystem()
        {
            message = new Dictionary<MsgCode, string>()
            {
                [MsgCode.UNKNOW_EXPRESSION_TOKEN] = "An unknown token was found during expression parsing."
            };
        }

        public static MsgSystem GetInstance
        {
            get {
                if (INSTANCE == null)
                {
                    INSTANCE = new MsgSystem();
                }

                return (INSTANCE);
            }
        }

        public void Error(MsgCode code, Parser parser, out bool error)
        {
            string text = message[code];

            error = true;

            int lineNumber;
            string sourceLine;
            parser.GetParserInfo(out lineNumber, out sourceLine);

            Console.WriteLine((lineNumber+1) + ":" + sourceLine);
            Console.WriteLine(text);

            Token token = parser.GetToken();
            while (!token.IsEOS())
            {
                token = parser.GetToken();
            }
        }
    }
}
