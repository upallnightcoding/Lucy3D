using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class NodeVariable : Node
    {
        public ArrayElements arrayElements { get; set; } = null;

        private string variable = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public NodeVariable(string variable)
        {
            this.variable = variable;
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        public override NodeValue Evaluate(Context context)
        {
            SymbolTable symbolTable = context.GetSymbolTable();

            SymbolTableRecData data = symbolTable.GetValue(variable, arrayElements, context);

            return (data.GetNodeValue());
        }
    }
}
