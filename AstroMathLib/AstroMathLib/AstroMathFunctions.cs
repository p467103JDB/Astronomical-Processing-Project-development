using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// JACK DU BOULAY
// P467103
// 17/09/2024

namespace AstroMathLib
{
    public class AstroMathFunctions
    {
        public double StarVelocity(double observedWavelength, double restWaveLength)
        {
            double changeInWavelength = observedWavelength - restWaveLength;
            double c = 299792458; //  Speed of light / Metres per second
            double starVelocity = c * (changeInWavelength / restWaveLength);
            return starVelocity;
        }

        public double StarDistance(double arcsecondsAngle)
        {
            return 1.0 / arcsecondsAngle;
        }


        public double TemperatureInKelvin(double temperatureInCelsius)
        {
            double kelvin = temperatureInCelsius + 273.15; // The Actual zero kelvin
            return kelvin;
        }

        public double EventHorizon(double blackholeMass)
        {
            double gravitationalConstant = 6.67430e-11; //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const - easier to use the shorthand form.
            double c = 299792458; // SoL
            double schwarzschildRadius = (2 * gravitationalConstant * blackholeMass) / Math.Pow(c, 2.0); // R = 2 x G x M / c^2
            return schwarzschildRadius; // R in Meters
        }
    }
}