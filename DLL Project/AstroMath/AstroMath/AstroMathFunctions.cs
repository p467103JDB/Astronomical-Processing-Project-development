﻿namespace AstroMath
{
    partial class AstroMathFunctions
    {
        public static double StarVelocity(double observedWavelength, double restWaveLength)
        {
            double changeInWavelength = observedWavelength - restWaveLength;
            double c = 299792458; // Speed of light
            double starVelocity = c / changeInWavelength; // is this right?
            return starVelocity;
        }

        public static double StarDistance(double arcsecondsAngle)
        {
            double parallaxAngle = (1/3600)*arcsecondsAngle; // wtf does this mean
            double parsec = 3.0857*Math.Pow(10, 16); // parsec
            double distance = parsec / parallaxAngle;
            return distance;
        }


        public static double TemperatureInKelvin(double temperatureIncelsius)
        {
            double kelvin = temperatureIncelsius + 273.15;
            return kelvin;
        }

        public static double EventHorizon(double blackholeMass)
        {
            double gravitationalConstant = 6.6720e-08; //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const - easier to use the shorthand form.
            double c = 299792458; // SoL
            double schwarzschildRadius = (2 * gravitationalConstant * blackholeMass) / Math.Pow(c, 2.0); // R = 2 x G x M / c^2
            return schwarzschildRadius; // R in Meters
        }
    }
}