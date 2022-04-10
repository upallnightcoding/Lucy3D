using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class SysFuncTrue : SysFunc
    {
        private readonly string SYSTEM_FUNCTION_NAME = "true";

        public NodeValue Execute(ParmElements parmElements, Context context)
        {
            return (new NodeValue(true));
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
