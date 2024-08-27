using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MSSS_Console_app
{
    internal class ConsoleClient
    {
        static void Main()
        {
            /*
            string address = "net.pipe://localhost/pipemynumbers";
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress ep = new EndpointAddress(address);
            IAstroContract channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);
            bool running = true;
            // I think this is the main part done, now i just link it up me thinks

            // CONSOLE CLIENT - ASTROMATH CLIENT
            Console.WriteLine("ASTROSERVER is now running. - By Jack P467103");
            Console.WriteLine("Type 'h'for help to see available options.");
            while (running)
            {
                string value = Console.ReadLine().ToLower().Trim(); // Could use a switch case but theres not THAT many options atm
                if (value == "h") { Console.WriteLine("Available options:\n'q' - to close server.\n'h' - see available options.\n"); } // Add more if necessary
                if (value == "q") { running = false; }
                // add more if necessary
            }
            Console.Write("\nPress nter to close program...");
            Console.ReadLine();*/
        }

        // Can use console to use IAstroContract
    }
}
