using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstroMath; // Implements .dll

namespace MSSS_Console_app 
{
    internal class AstroServer : IAstroContract // Question 6.2 Create server file called "AstroServer.cs" - Implements IAstroContract + AstroMath.dll 
    {
        public AstroMathFunctions AstroMath = new AstroMathFunctions();

        double IAstroContract.EventHorizon(double blackholeMass) // I'm assuming this is correct
        {
            return AstroMath.EventHorizon(blackholeMass);
        }

        double IAstroContract.StarDistance(double arcsecondsAngle)
        {
            return AstroMath.StarDistance(arcsecondsAngle);
        }

        double IAstroContract.StarVelocity(double observedWaveLength, double restWaveLength)
        {
            return AstroMath.StarVelocity(observedWaveLength, restWaveLength);
        }

        double IAstroContract.TemperatureInKelvin(double temperatureInCelsius)
        {
            return AstroMath.TemperatureInKelvin(temperatureInCelsius);
        }
    }
}
