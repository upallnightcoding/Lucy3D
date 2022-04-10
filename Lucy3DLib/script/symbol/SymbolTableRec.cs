using System.Collections.Generic;

namespace Lucy3DLib
{
    /// <summary>
    /// SymbolTableRec - This class represents an instance of a variable that
    /// has been declared in the symbol table.  This variable can be a user
    /// define variable, function, constant etc.
    /// </summary>
    class SymbolTableRec
    {
        // Variable Name, all entries require a name
        public string Variable { get; private set; } = null;

        // Variable Type
        public VariableType VarType { get; private set; } = VariableType.UNKNOWN;

        // Variable Designation Type
        public DesignationType DesigType { get; private set; } = DesignationType.UNKNOWN;

        // Array elements size defined during declaration
        private ArrayElements arraySizes = null;

        // Calculated size of each array element if defined
        private List<int> arrayElementSize = null;

        // Size of data buffer used to hold variable values
        private int dataBufferSize = -1;

        // Reference to a System Function
        private SysFunc sysFunc = null;

        // Data buffers to hold variable values
        private long[] iValue = null;
        private string[] sValue = null;
        private double[] fValue = null;
        private char[] cValue = null;
        private bool[] bValue = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        /// <summary>
        /// SymbolTableRec - 
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="varType"></param>
        /// <param name="paraElements"></param>
        public SymbolTableRec(SysFunc sysFunc)
        {
            this.Variable = sysFunc.SysFuncName();
            this.VarType = sysFunc.ReturnType();
            this.DesigType = DesignationType.SYSTEM_FUNCTION;
            this.sysFunc = sysFunc; 
        }

        /// <summary>
        /// SymbolTableRec - This constructor is used to define a scalar or 
        /// array user variable.  The array size is determined on the fly.
        /// All arrays 1, 2 and 3 dimensional are flattened to one 
        /// demension.
        /// </summary>
        /// <param name="varType"></param>
        /// <param name="variable"></param>
        /// <param name="arraySize"></param>
        /// <param name="context"></param>
        public SymbolTableRec(
            VariableType varType, 
            string variable, 
            ArrayElements arraySize, 
            Context context
        )
        {
            // Define the variable type and name
            //----------------------------------
            this.VarType = varType;
            this.Variable = variable;

            this.arraySizes = arraySize;

            // Define the variable as either a scalar or array.
            //-------------------------------------------------
            if (IsVariableScalar())
            {
                this.DesigType = DesignationType.SCALAR;
                this.dataBufferSize = 1;
            } else
            {
                this.DesigType = DesignationType.ARRAY;
                CalcArraySize(context);
            }

            // Setup buffers for whatever type of data is being declared.
            //-----------------------------------------------------------
            switch (VarType)
            {
                case VariableType.INTEGER:
                    iValue = new long[dataBufferSize];
                    break;
                case VariableType.STRING:
                    sValue = new string[dataBufferSize];
                    break;
                case VariableType.FLOAT:
                    fValue = new double[dataBufferSize];
                    break;
                case VariableType.CHARACTER:
                    cValue = new char[dataBufferSize];
                    break;
                case VariableType.BOOLEAN:
                    bValue = new bool[dataBufferSize];
                    break;
            }
        }

        /// <summary>
        /// Assign() - Uses the array elements to determine the position to 
        /// assign the node value.  The context object is needed to calculate
        /// the actual element values.
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="value"></param>
        /// <param name="context"></param>
        public void Assign(ArrayElements elements, NodeValue value, Context context)
        {
            int index = CalcArrayIndex(elements, context);

            switch (VarType)
            {
                case VariableType.INTEGER:
                    iValue[index] = value.GetInteger();
                    break;
                case VariableType.STRING:
                    sValue[index] = value.GetString();
                    break;
                case VariableType.FLOAT:
                    fValue[index] = value.GetFloat();
                    break;
                case VariableType.CHARACTER:
                    cValue[index] = value.GetChar();
                    break;
                case VariableType.BOOLEAN:
                    bValue[index] = value.GetBoolean();
                    break;
            }
        }

        public SymbolTableRecData GetValue(ArrayElements elements, Context context)
        {
            SymbolTableRecData data = null;

            switch (DesigType)
            {
                case DesignationType.ARRAY:
                case DesignationType.SCALAR:
                    data = CalculateByArray(elements, context);
                    break;
                case DesignationType.SYSTEM_FUNCTION:
                    break;
            }

            return (data);
        }

        public NodeValue GetValue(ParmElements elements, Context context)
        {
            return (sysFunc.Execute(elements, context));
        }

        private SymbolTableRecData CalculateByArray(ArrayElements elements, Context context)
        {
            SymbolTableRecData data = null;

            int index = CalcArrayIndex(elements, context);

            switch (VarType) // TODO We do not want news here for each symbol table extraction
            {
                case VariableType.CHARACTER:
                    data = new SymbolTableRecData(GetChar(index));
                    break;
                case VariableType.STRING:
                    data = new SymbolTableRecData(GetString(index), VariableType.STRING);
                    break;
                case VariableType.INTEGER:
                    data = new SymbolTableRecData(GetInteger(index));
                    break;
                case VariableType.FLOAT:
                    data = new SymbolTableRecData(GetFloat(index));
                    break;
                case VariableType.BOOLEAN:
                    data = new SymbolTableRecData(GetBool(index));
                    break;
            }

            return (data);
        }

        /*********************/
        /*** Get Functions ***/
        /*********************/

        public long GetInteger(int offset) => iValue[offset];

        public string GetString(int offset) => sValue[offset];

        public double GetFloat(int offset) => fValue[offset];

        public char GetChar(int offset) => cValue[offset];

        public bool GetBool(int offset) => bValue[offset];

        /*************************/
        /*** Private Functions ***/
        /*************************/

        /// <summary>
        /// CalcIndex() - 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private int CalcArrayIndex(ArrayElements elements, Context context)
        {
            int index = -1;

            if (IsVariableScalar())
            {
                index = 0;
            }
            else
            {
                switch (elements.Count())
                {
                    case 1:
                        {
                            index = (int)elements.Elements[0].Evaluate(context).GetInteger();
                        }
                        break;
                    case 2:
                        {
                            int n = arrayElementSize[1];
                            int i = (int)elements.Elements[0].Evaluate(context).GetInteger();
                            int j = (int)elements.Elements[1].Evaluate(context).GetInteger();
                            index = i * n + j;
                        }
                        break;
                    case 3:
                        {
                            int p = arrayElementSize[2];
                            int n = arrayElementSize[1];
                            int i = (int)elements.Elements[0].Evaluate(context).GetInteger();
                            int j = (int)elements.Elements[1].Evaluate(context).GetInteger();
                            int k = (int)elements.Elements[2].Evaluate(context).GetInteger();
                            index = i * n * p + j * p + k;
                        }
                        break;
                }
            }

            return (index);
        }

        /// <summary>
        /// IsVariableScalar() - 
        /// </summary>
        /// <returns></returns>
        private bool IsVariableScalar()
        {
            return (arraySizes == null);
        }

        /// <summary>
        /// CalcArraySize() - Calculates the size of the array based on the elements
        /// of the array declaration.  If the variable is a scalar, this 
        /// function should never be invoked.  The dataBufferSize will contain the
        /// total size of the array and the arrayElementSize list will contain the
        /// size of each element.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void CalcArraySize(Context context)
        {
            dataBufferSize = 1;

            arrayElementSize = new List<int>();

            foreach (Node element in arraySizes.Elements)
            {
                int elementSize = (int)element.Evaluate(context).GetInteger();

                arrayElementSize.Add(elementSize);

                dataBufferSize *= elementSize;
            }
        }
    }
}
