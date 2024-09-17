using System;
using System.ServiceModel;

namespace MSSS_Console_app
{
    // JACK DU BOULAY
    // P467103
    // 17/09/2024

    internal class ServerProgram
    {
        static void Main(string[] args)
        {
            string address = "net.pipe://localhost/pipeMSSS";

            ServiceHost serviceHost = new ServiceHost(typeof(AstroServer));
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            serviceHost.AddServiceEndpoint(typeof(IAstroContract), binding, address);
            serviceHost.Open();

            Console.WriteLine("ServiceHost is running. Press <<Return>> to Exit");
            Console.ReadLine();
            serviceHost.Close();
        }
    }
}


