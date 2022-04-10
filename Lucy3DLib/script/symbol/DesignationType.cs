using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy3DLib
{
    enum DesignationType
    {
        SCALAR,   // User defined scalar variable
        ARRAY,      // User defined multi-dimensional array
        CONSTANT,   // User defined constant example: 123, 3.145 ...

        SYSTEM_FUNCTION, // System Functions

        UNKNOWN
    }
}
