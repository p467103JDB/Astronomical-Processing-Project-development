using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MSSS_Console_Client_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string address = "net.pipe://localhost/pipeMSSS";
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress ep = new EndpointAddress(address);
            IAstroContract channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);

            // CONSOLE CLIENT - ASTROMATH CLIENT MENU
            bool running = true;
            Console.WriteLine("ASTROSERVER CLIENT is now running. - By Jack P467103");
            ShowOptions();
            while (running)
            {
                Console.Write("Select an option: ");
                switch (Console.ReadLine().ToLower().Trim())
                {
                    case "h": // Help menu
                        ShowOptions();
                        break;
                    case "q": // Quit Client
                        running = false;
                        break;
                    case "sv": // Star Velocity
                        {
                            Console.Write("\nSelected Star Velocity\nPlease input double for Observed Wave Length: ");
                            double OWL = GetDouble(); //OBSERVED WAVELENGTH // Example number 500.1
                            Console.Write("\nPlease input double for Rest Wave Length: ");
                            double RWL = GetDouble(); //RESTED WAVELENGTH // Example numner 500 
                            Console.WriteLine("Star velocity: " + channel.StarVelocity(OWL, RWL) + "m/s\n"); // Should get desired damn near 60000m/s
                            break;
                        }
                    case "sd": // Star Distance
                        {
                            Console.Write("\nSelected Star Distance\nPlease input double for Arcsecond Angle: ");
                            double ASA = GetDouble(); //Barnard's Star has parallax angle of 0.547 arcseconds and has a distance = 1.828 parsec
                            Console.WriteLine("Star distance: " + channel.StarDistance(ASA) + " parsecs\n");
                            break;
                        }
                    case "tk": // Temperature to Kelvin
                        {
                            Console.Write("\nSelected convert temperature (C) to Kelvin (K) \nPlease input double for degrees Celsius: ");
                            double DC = GetDouble(); // the logic is dv + 273.15
                            Console.WriteLine("Temperature in Kelvin(K): " + channel.TemperatureInKelvin(DC) + "K\n");
                            break;
                        }
                    case "eh": // Event Horizon
                        {
                            Console.Write("\nSelected Event Horizon\nPlease input double for Blackhole Mass in Kg: ");
                            double BHM = GetDouble(); // Example: black hole mass of 605985810220889885728804 kg to find Event horizon radius is in metres 
                            Console.WriteLine($"Event Horizon radius: " + channel.EventHorizon(BHM) + " meteres\n");
                            break;
                        }
                    default: // if none of those loop back and do nothing
                        {
                            break;
                        }
                }
            }
            Console.Write("\nSelected Close Server.\nPress enter to quit...");
            Console.ReadLine();
        }

        private static double GetDouble() // Get double for methods in dll
        {
            bool successfulDouble = false;
            double convertToDouble = 0.0;
            while (!successfulDouble) 
            {
                string readline = Console.ReadLine();
                try
                {
                    convertToDouble = double.Parse(readline);
                    successfulDouble = true;
                }
                catch (Exception) // i dont need to keep the exception
                {
                    Console.Write("- Not a valid double.\nPlease re-enter double: ");
                }
            }
            return convertToDouble;
        }

        private static void ShowOptions()
        {
            Console.WriteLine("\nAvailable options:\n" +
                "'q'  - to close client.\n" +
                "'h'  - see available options again.\n" +
                "'sv' - to use Star Velocity method.\n" +
                "'sd' - to use Star Distance method.\n" +
                "'tk' - to use Temperature to Kelvin method.\n" +
                "'eh' - to use Event Horizon method.\n");
        }
    }
}
