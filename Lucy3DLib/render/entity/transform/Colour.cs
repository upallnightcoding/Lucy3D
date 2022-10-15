using System;
using System.Collections.Generic;
using System.Text;

namespace Lucy3DLib
{
    /// <summary>
    /// Colour - This class defines the RGB (red, green, blue) colors that 
    /// are used by the system. These are floating point between zero 
    /// and one.  These values cannot be negative.
    /// </summary>
    public class Colour
    {
        // Red Colour Component
        public float R { set; get; } = 0.0f;

        // Green Colour Component
        public float G { set; get; } = 0.0f;

        // Blue Colour Component
        public float B { set; get; } = 0.0f;

        /*******************/
        /*** Constructor ***/
        /*******************/

        public Colour(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
