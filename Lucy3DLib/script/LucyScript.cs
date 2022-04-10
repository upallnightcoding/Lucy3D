namespace Lucy3DLib
{
    public class LucyScript
    {
        private SourceCode source = null;

        public LucyScript(SourceCode source)
        {
            this.source = source;
        }

        public void Execute()
        {
            Parser parser = new Parser(source);

            Token program = parser.GetToken();

            Token programName = parser.GetToken();

            Cmd codeBlock = new CmdCodeBlock();

            Node node = codeBlock.Translate(parser);

            Context context = new Context();

            node.Evaluate(context);
        }
    }
}
