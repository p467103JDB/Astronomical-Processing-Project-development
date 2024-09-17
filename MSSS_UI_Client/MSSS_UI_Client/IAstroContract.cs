using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


// Q7.1
namespace MSSS_UI_Client
{
    [ServiceContract]
    internal interface IAstroContract
    {
        [OperationContract]
        double StarVelocity(double observedWaveLength, double restWaveLength);

        [OperationContract]
        double StarDistance(double arcsecondsAngle);

        [OperationContract]
        double TemperatureInKelvin(double temperatureInCelsius);

        [OperationContract]
        double EventHorizon(double blackholeMass);
    }
}
