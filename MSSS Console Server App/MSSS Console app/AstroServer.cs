using System;
using System.ServiceModel;
using AstroMathLib;

namespace MSSS_Console_app 
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class AstroServer : IAstroContract 
    {
        readonly AstroMathFunctions AstroMath = new AstroMathFunctions();
        public double EventHorizon(double blackholeMass) 
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
