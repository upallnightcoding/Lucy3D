using System.Collections.Generic;

namespace Lucy3DLib
{
    class ScopeLevels
    {
        // Variables declared in the current scope
        //----------------------------------------
        private Dictionary<string, SymbolTableRec> variables = null;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public ScopeLevels()
        {
            variables = new Dictionary<string, SymbolTableRec>();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        /// <summary>
        /// Add() - 
        /// </summary>
        /// <param name="record"></param>
        public void Add(SymbolTableRec record)
        {
            if (record != null)
            {
                variables[record.Variable] = record;
            }
        }

        /// <summary>
        /// Find() - 
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public SymbolTableRec Find(string variable)
        {
            SymbolTableRec record = null;

            if(!variables.TryGetValue(variable, out record))
            {
                record = null;
            }

            return (record);
        }
    }
}
