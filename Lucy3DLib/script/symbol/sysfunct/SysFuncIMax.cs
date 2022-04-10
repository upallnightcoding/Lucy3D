using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    class SysFuncIMax : SysFunc
    {
        private readonly string SYSTEM_FUNCTION_NAME = "imax";

        public NodeValue Execute(ParmElements parmElements, Context context)
        {
            int count = parmElements.Count();
            long maxValue = parmElements.Elements[0].Evaluate(context).GetInteger();

            for (int i = 1; i < count; i++)
            {
                long value = parmElements.Elements[i].Evaluate(context).GetInteger();

                maxValue = (value > maxValue) ? value : maxValue;
            }

            return (new NodeValue(maxValue));
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
