using System.Collections.Generic;

namespace Lucy3DLib
{
    /// <summary>
    /// ArrayElements - This class is used as a base class to house all 
    /// array elements that are used to declare an array, return the
    /// value of an array or assign a value to an array.
    /// </summary>
    class ArrayElements
    {
        // List of array elements as nodes
        public List<Node> Elements { get; private set; } = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public ArrayElements()
        {
            Elements = new List<Node>();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// Count() - Returns the number of expression that are in the 
        /// element list.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return (Elements.Count);
        }

        /// <summary>
        /// Add() - Adds an array element as a node in the array element list.
        /// The elements of the array are translated from left to right.  The
        /// left most element is list position 0, the second element is list
        /// position 2 and so on.  If the node is null, it is not added to the
        /// array elements.
        /// 
        /// Example:
        ///     Array[n0, n1, n2] => List ... n0, n1, n2
        /// 
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
