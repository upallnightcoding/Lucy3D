using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class NodeFunction : Node
    {
        public ParmElements elements { get; set; } = null;

        private string variable = null;

        public NodeFunction(string variable)
        {
            this.variable = variable;
        }

        public override NodeValue Evaluate(Context context)
        {
            return (context.GetSymbolTable().GetValue(variable, elements, context));
        }
    }
}
