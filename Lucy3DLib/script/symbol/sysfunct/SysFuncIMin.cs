using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class SysFuncIMin : SysFunc
    {
        private readonly string SYSTEM_FUNCTION_NAME = "imin";

        public NodeValue Execute(ParmElements parmElements, Context context)
        {
            int count = parmElements.Count();
            long minValue = parmElements.Elements[0].Evaluate(context).GetInteger();

            for (int i = 1; i < count; i++)
            {
                long value = parmElements.Elements[i].Evaluate(context).GetInteger();

                minValue = (value < minValue) ? value : minValue;
            }

            return (new NodeValue(minValue));
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
