using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class SysFuncFalse : SysFunc
    {
        private readonly string SYSTEM_FUNCTION_NAME = "false";

        public NodeValue Execute(ParmElements parmElements, Context context)
        {
            return (new NodeValue(false));
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
