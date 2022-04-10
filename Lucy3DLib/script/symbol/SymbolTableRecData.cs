using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    /// <summary>
    /// SymbolTableRecData - This class acts as a data transfer
    /// </summary>
    class SymbolTableRecData
    {
        // Containers for each Tilde primitive type
        //-----------------------------------------
        public long IValue { get; private set; } = 0;
        public string SValue { get; private set; } = null;
        public double FValue { get; private set; } = 0;
        public char CValue { get; private set; } = ' ';
        public bool BValue { get; private set; } = false;

        // Default NodeValue Type
        //-----------------------
        public VariableType Type { get; private set; } = VariableType.UNKNOWN;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public SymbolTableRecData(long iValue)
        {
            this.IValue = iValue;
            this.Type = VariableType.INTEGER;
        }

        public SymbolTableRecData(string sValue, VariableType type)
        {
            this.SValue = sValue;

            switch (type)
            {
                case VariableType.STRING:
                    this.Type = type;
                    break;
                case VariableType.SYMBOL:
                    this.Type = type;
                    break;
            }
        }

        public SymbolTableRecData(double fValue)
        {
            this.FValue = fValue;
            this.Type = VariableType.FLOAT;
        }

        public SymbolTableRecData(char cValue)
        {
            this.CValue = cValue;
            this.Type = VariableType.CHARACTER;
        }

        public SymbolTableRecData(bool bValue)
        {
            this.BValue = bValue;
            this.Type = VariableType.BOOLEAN;
        }

        /***************************/
        /*** Predicate Functions ***/
        /***************************/

        public bool IsFloat() => (Type == VariableType.FLOAT);

        public bool IsInteger() => (Type == VariableType.INTEGER);

        public bool IsString() => (Type == VariableType.STRING);

        public bool IsChar() => (Type == VariableType.CHARACTER);

        public bool IsBoolean() => (Type == VariableType.BOOLEAN);

        public NodeValue GetNodeValue()
        {
            NodeValue value = null;

            switch (Type)
            {
                case VariableType.CHARACTER:
                    value = new NodeValue(CValue);
                    break;
                case VariableType.STRING:
                    value = new NodeValue(SValue);
                    break;
                case VariableType.INTEGER:
                    value = new NodeValue(IValue);
                    break;
                case VariableType.FLOAT:
                    value = new NodeValue(FValue);
                    break;
                case VariableType.BOOLEAN:
                    value = new NodeValue(BValue);
                    break;
            }

            return (value);
        }

        /// <summary>
        /// GetString() - Returns the value of th NodeValue as a Tilde String.
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string value = null;

            // Determine the type of the NodeValue
            //------------------------------------
            switch (Type)
            {
                case VariableType.CHARACTER:
                    value = char.ToString(CValue);
                    break;
                case VariableType.STRING:
                    value = SValue;
                    break;
                case VariableType.INTEGER:
                    value = IValue.ToString();
                    break;
                case VariableType.FLOAT:
                    value = FValue.ToString();
                    break;
                case VariableType.BOOLEAN:
                    value = (BValue) ? "true" : "false";
                    break;
            }

            return (value);
        }

        /// <summary>
        /// GetFloat() - Returns the value of the NodeValue as a Tilde Float.
        /// </summary>
        /// <returns></returns>
        public double GetFloat()
        {
            double value = 0;

            // Determine the type of the NodeValue
            //------------------------------------
            switch (Type)
            {
                case VariableType.FLOAT:
                    value = FValue;
                    break;
                case VariableType.INTEGER:
                    value = IValue;
                    break;
            }

            return (value);
        }

        /// <summary>
        /// GetIntger() - Returns the value of NodeValue as a Tilde Integer.
        /// </summary>
        /// <returns></returns>
        public long GetInteger()
        {
            long value = 0;

            // Determine the type of the NodeValue
            //------------------------------------
            switch (Type)
            {
                case VariableType.FLOAT:
                    value = (long)FValue;
                    break;
                case VariableType.INTEGER:
                    value = IValue;
                    break;
            }

            return (value);
        }

        /// <summary>
        /// GetChar() - 
        /// </summary>
        /// <returns></returns>
        public char GetChar()
        {
            return (CValue);
        }

        /// <summary>
        /// GetBoolean() - 
        /// </summary>
        /// <returns></returns>
        public bool GetBoolean()
        {
            return (BValue);
        }
    }
}
