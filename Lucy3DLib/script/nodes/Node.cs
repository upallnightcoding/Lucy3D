using System.Collections.Generic;

namespace Lucy3DLib
{
    /// <summary>
    ///  Node - Is the base class of all programming command nodes.  It contains
    ///  of list of nodes that are the children of all programming command nodes.
    ///  Each command will have its own means of evaluating a programming 
    ///  command node.  
    /// </summary>
    abstract class Node
    {
        // List of node objects, these are the children of a programming node
        private List<Node> nodeList = null;

        // Means of evaluating/executing a programming command node
        public abstract NodeValue Evaluate(Context context);

        /*******************/
        /*** Constructor ***/
        /*******************/

        public Node()
        {
            nodeList = new List<Node>();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// Add() - Adds a new node to the node list.
        /// </summary>
        /// <param name="node"></param>
        public void Add(Node node)
        {
            if (node != null)
            {
                nodeList.Add(node);
            }
        }

        /***************************/
        /*** Protected Functions ***/
        /***************************/

        /// <summary>
        /// GetNode() - Returns the ith node in the node list.  If the node
        /// does not exist, an exception is thrown to the caller.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected Node GetNode(int index)
        {
            return (nodeList[index]);
        }

        /// <summary>
        /// GetNodeList() - Return the entire node list, this is mainly 
        /// used for looping through each element in the node list.
        /// </summary>
        /// <returns></returns>
        protected List<Node> GetNodeList()
        {
            return (nodeList);
        }
    }
}
