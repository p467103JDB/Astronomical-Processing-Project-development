using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AstroMath;

namespace MSSS_Console_app
{
    
    internal interface IAstroContract
    {
      
        IAstroMathFunctions AstroMathFunctions { get; }
        


    }
}
