using System;

namespace Lucy3DLib
{
    /// <summary>
    /// NodePrint - This class is used to print data. The data is listed in 
    /// the print command as comma separated values ending with the end of 
    /// statement character. If there is an error evaluating any of the 
    /// values that value is skipped and processing continues with the next 
    /// value. A carriage return is placed at the end of every line.
    /// </summary>
    class NodePrint : Node
    {
        /// <summary>
        /// Execute() - 
        /// </summary>
        public override NodeValue Evaluate(Context context)
        {

            foreach(Node node in GetNodeList())
            {
                Node value = node.Evaluate(context);

                Console.Write(((NodeValue)value).GetString());
            }

            Console.WriteLine("");

            return (null);
        }
    }
}
