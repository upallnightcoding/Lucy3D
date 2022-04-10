using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class SysFuncNot : SysFunc
    {
        private readonly string SYSTEM_FUNCTION_NAME = "not";

        public NodeValue Execute(ParmElements parmElements, Context context)
        {
            bool value = parmElements.Elements[0].Evaluate(context).GetBoolean();

            return (new NodeValue(!value));
        }

        public VariableType ReturnType()
        {
            return (VariableType.BOOLEAN);
        }

        public string SysFuncName()
        {
            return (SYSTEM_FUNCTION_NAME);
        }
    }
}
