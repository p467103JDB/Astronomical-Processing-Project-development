using System;
using System.ServiceModel;
using AstroMath; // Implements .dll

namespace MSSS_Console_app 
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // ????
    internal class AstroServer : IAstroContract // Question 6.2 Create server file called "AstroServer.cs" - Implements IAstroContract + AstroMath.dll 
    {
        AstroMathFunctions AstroMath = new AstroMathFunctions();

        public double EventHorizon(double blackholeMass) // I'm assuming this is correct
        {
            return AstroMath.EventHorizon(blackholeMass);
        }

        public double StarDistance(double arcsecondsAngle)
        {
            return AstroMath.StarDistance(arcsecondsAngle);
        }

        public double StarVelocity(double observedWaveLength, double restWaveLength)
        {
            return AstroMath.StarVelocity(observedWaveLength, restWaveLength);
        }

        public double TemperatureInKelvin(double temperatureInCelsius)
        {
            return AstroMath.TemperatureInKelvin(temperatureInCelsius);
        }
    }
}
