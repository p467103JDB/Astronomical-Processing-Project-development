using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //double parallaxAngle = (1/3600)*arcsecondsAngle; // wtf does this mean
            //double parsec = 3.0857*Math.Pow(10, 16); // parsec
            //double distance = parsec / parallaxAngle;
            return 1.0 / arcsecondsAngle;
        }


        public double TemperatureInKelvin(double temperatureInCelsius)
        {
            double kelvin = temperatureInCelsius + 273.15; // The Actual zero kelvin
            return kelvin;
        }

        public double EventHorizon(double blackholeMass)
        {
            double gravitationalConstant = 6.6720e-08; //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const - easier to use the shorthand form.
            double c = 299792458; // SoL
            double schwarzschildRadius = (2 * gravitationalConstant * blackholeMass) / Math.Pow(c, 2.0); // R = 2 x G x M / c^2
            return schwarzschildRadius; // R in Meters
        }
    }
}

/*
public double StarVelocity(double observedWaveLength, double atRestWavelength)
{
    double num = 2.99792458 * Math.Pow(10.0, 8.0);
    double num2 = observedWaveLength - atRestWavelength;
    return num * (num2 / atRestWavelength);
}

public double StarDistance(double angle)
{
    return 1.0 / angle;
}

public double Kelvin(double celsius)
{
    return celsius + 273.0;
}

public double EventHorizon(double blackHoleMass)
{
    double num = 6.6743 * Math.Pow(10.0, -11.0);
    double num2 = 2.99792458 * Math.Pow(10.0, 8.0);
    return 2.0 * num * blackHoleMass / (num2 * num2);
}*/