using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class SysFuncISum : SysFunc
    {
        private readonly string SYSTEM_FUNCTION_NAME = "isum";

        public NodeValue Execute(ParmElements parmElements, Context context)
        {
            int count = parmElements.Count();
            long sum = 0;

            for (int i = 0; i < count; i++)
            {
                sum += parmElements.Elements[i].Evaluate(context).GetInteger();
            }

            return (new NodeValue(sum));
        }

        public VariableType ReturnType()
        {
            return (VariableType.INTEGER);
        }

        public string SysFuncName()
        {
            return (SYSTEM_FUNCTION_NAME);
        }
    }
}
