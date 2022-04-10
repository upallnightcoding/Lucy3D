namespace Lucy3DLib
{
    /// <summary>
    /// NodeAdd - This class is used to execute the binary 'divide' operation.  It 
    /// uses the values that are on the left and right side of the operator
    /// and returns the calcuated value.  If the Evaluate function returns a 
    /// null, this means the operands were not of a type that could be 
    /// managed by this function.
    /// </summary>
    class NodeDivide : Node
    {
        /*******************/
        /*** Constructor ***/
        /*******************/

        public NodeDivide(Node vLeft, Node vRight)
        {
            Add(vLeft);
            Add(vRight);
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// Evaluate() - Executes an 'divide' operation on the values to the left
        /// and right of the operator.  If both of the values are integers an
        /// integer operation is executed against the values.  If either one
        /// of the values are float, then both values are converted to float
        /// and a float operation is executed.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override NodeValue Evaluate(Context context)
        {
            NodeValue value = null;

            // Evaluate the left and right side of the operator
            //-------------------------------------------------
            NodeValue vLeft = GetNode(0).Evaluate(context);
            NodeValue vRight = GetNode(1).Evaluate(context);

            // Execute the operator operation
            //-------------------------------
            if (vLeft.IsInteger() && vRight.IsInteger())
            {
                value = new NodeValue((long) (vLeft.GetInteger() / vRight.GetInteger()));
            }
            else if (vLeft.IsFloat() || vRight.IsFloat())
            {
                value = new NodeValue(vLeft.GetFloat() / vRight.GetFloat());
            }

            return (value);
        }
    }
}
