using System;
using System.Collections.Generic;

namespace Lucy3DLib
{
    /// <summary>
    /// SourceCode - This class is used to contain the source code to be
    /// executed.  The source code is contained in a list of strings.
    /// Null lines are not allowed as source code.
    /// </summary>
    public class SourceCode
    {
        // Program source code
        private List<string> sourceCode = null;

        // Pointer to the active programming line
        public int LineNumber { get; private set; } = 0;

        // Pointer to the active programming character
        private int character = 0;

        /******************/
        /*** Contructor ***/
        /******************/

        public SourceCode()
        {
            sourceCode = new List<string>();

            Reset();
        }

        public SourceCode(string source)
        {
            sourceCode = new List<string>();

            foreach (string line in source.Split('\n'))
            {
                Add(line);
            }

            Reset();
        }

        /************************/
        /*** Public Functions ***/
        /************************/

        public string SourceLine
        {
            get
            {
                return(sourceCode[LineNumber]);
            }
        }

        /// <summary>
        /// Eos() - End of Code indicator set true if the line pointer
        /// is pointing past the last programming line.
        /// </summary>
        /// <returns></returns>
        public bool Eoc() => (LineNumber >= sourceCode.Count);

        /// <summary>
        /// PeekChar() - Returns the active programing character
        /// </summary>
        /// <returns></returns>
        public char PeekChar() => (sourceCode[LineNumber][character]);

        /// <summary>
        /// Reset() - Reset the source code pointers to the beginning of the 
        /// source code text.
        /// </summary>
        public void Reset()
        {
            LineNumber = 0;
            character = 0;
        }

        /// <summary>
        /// MoveNextChar() - Sets the next source code character as the
        /// active programming source code character.  There is no check
        /// in this code to test of Eoc.  This must be done by the caller.
        /// </summary>
        public void MoveNextChar()
        {
            if (++character >= sourceCode[LineNumber].Length)
            {
                character = 0;
                LineNumber++;

                while(!Eoc() && String.IsNullOrEmpty(sourceCode[LineNumber]))
                {
                    LineNumber++;
                }
            }
        }

        /// <summary>
        /// Add() - Adds a source code line to the source code lines list.
        /// If the source code line is null, it is not added to the the 
        /// source code line list.
        /// </summary>
        /// <param name="source"></param>
        public void Add(string source)
        {
            if (source != null)
            {
                sourceCode.Add(source);
            }
        }
    }
}
