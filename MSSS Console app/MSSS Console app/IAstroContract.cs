using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AstroMath;

// Student Activity 10 + 11
// https://learn.microsoft.com/en-us/dotnet/api/system.servicemodel.operationcontractattribute?view=net-8.0
// https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-create-a-service-with-a-contract-interface#example
namespace MSSS_Console_app
{
    [ServiceContract]
    public interface IAstroContract
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
