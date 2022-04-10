using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    interface SysFunc
    {
        public string SysFuncName();

        public NodeValue Execute(ParmElements parmElements, Context context);

        public VariableType ReturnType();
    }
}
