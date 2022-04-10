namespace Lucy3DLib
{
    abstract class Cmd
    {
        private string command = null;

        public abstract Node Translate(Parser parser);

        /*******************/
        /*** Constructor ***/
        /*******************/

        public Cmd(string command)
        {
            this.command = command;
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        public string GetCommand()
        {
            return (command);
        }
    }
}
