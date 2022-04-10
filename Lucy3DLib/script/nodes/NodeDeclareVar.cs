namespace Lucy3DLib
{
    /// <summary>
    /// NodeDeclareVar - 
    /// </summary>
    class NodeDeclareVar : Node
    {
        private string variable = null;

        private VariableType type = VariableType.UNKNOWN;

        private Node initialize = null;

        private ArrayElements arrayElements = null;

        /********************/
        /*** Constructors ***/
        /********************/

        public NodeDeclareVar(VariableType type, Token variable, ArrayElements arrayElements, Node initialize)
        {
            this.type = type;
            this.variable = variable.GetVariable();
            this.initialize = initialize;
            this.arrayElements = arrayElements;
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// Execute() - Declares a variable in the symbol table.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override NodeValue Evaluate(Context context)
        {
            context.GetSymbolTable().Declare(type, variable, arrayElements, context);

            if (initialize != null)
            {
                NodeValue value = initialize.Evaluate(context);

                context.GetSymbolTable().Assign(variable, null, value, context);
            }

            return (null);
        }
    }
}
