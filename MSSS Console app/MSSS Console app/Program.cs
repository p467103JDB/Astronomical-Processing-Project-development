using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MSSS_Console_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(AstroServer),
            new Uri[]{
                        new Uri("net.pipe://localhost")
            }))
            {
                host.AddServiceEndpoint(typeof(AstroServer),
                new NetNamedPipeBinding(), "PipeReverse");
                host.Open();
                Console.WriteLine("Service is available. " + "Press <ENTER> to exit.");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
