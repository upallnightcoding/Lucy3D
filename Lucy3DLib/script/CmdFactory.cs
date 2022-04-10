using System.Collections.Generic;

namespace Lucy3DLib
{
    /// <summary>
    /// CmdFactory - This class is used as a command factory for all commands
    /// in Tilde.  The GetCommand function is used to return the command
    /// object.  If the command does not exist, it is assumed that the command
    /// is an assignment statement and the assignment command is return by 
    /// the GetCommand invoke.
    /// 
    /// The commands that are retuned by the command factory are reused.  Each
    /// command must be self-contained and initialize itself before each use.
    /// Failure to do so will mean residual values wil be retained from one
    /// use to another.
    /// </summary>
    class CmdFactory
    {
        // Singleton instance of the CmdFactory class
        public static CmdFactory INSTANCE = new CmdFactory();

        private CmdAssign assignCmd = new CmdAssign();

        // Dictionary of all commands
        private Dictionary<string, Cmd> commandMap = null;

        /***************************/
        /*** Private Constructor ***/
        /***************************/

        private CmdFactory()
        {
            commandMap = new Dictionary<string, Cmd>();

            InstallCmd();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// GetCommand() - 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Cmd GetCommand(Token keyword)
        {
            Cmd command = null;

            if (!commandMap.TryGetValue(keyword.GetKeyword().ToUpper(), out command))
            {
                assignCmd.Variable = keyword;

                command = assignCmd;
            }

            return (command);
        }

        /*************************/
        /*** Private Functions ***/
        /*************************/

        /// <summary>
        /// InstallCmd() - Install all command objects into the command 
        /// dictionary.  This must be called once before any command can
        /// be retrieved.
        /// </summary>
        private void InstallCmd()
        {
            InstallCmd(new CmdPrint());
            InstallCmd(new CmdInteger());
            InstallCmd(new CmdBoolean());
            InstallCmd(new CmdCharacter());
            InstallCmd(new CmdFloat());
            InstallCmd(new CmdString());
        }

        /// <summary>
        /// InstallCmd() - Install a command into the command dictionary.  If 
        /// the command is null, it is not added to the dictionary.  The 
        /// key of the dictionary is the name of the command.  The value in
        /// the diction is the command object.
        /// </summary>
        /// <param name="command"></param>
        private void InstallCmd(Cmd command)
        {
            if (command != null)
            {
                commandMap.Add(command.GetCommand(), command);
            }
        }


    }
}
