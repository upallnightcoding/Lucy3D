using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    /// <summary>
    /// ParmElements - This class
    /// </summary>
    class ParmElements
    {
        // List of parameter elements as nodes
        public List<Node> Elements { get; private set; } = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public ParmElements()
        {
            Elements = new List<Node>();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// Count() - Returns the number of parameters that are in the element
        /// list.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return (Elements.Count);
        }

        /// <summary>
        /// Add() - 
        /// </summary>
        /// <param name="node"></param>
        public void Add(Node node)
        {
            if (node != null)
            {
                Elements.Add(node);
            }
        }
    }
}
