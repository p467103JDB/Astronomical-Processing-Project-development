using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MSSS_UI_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Connection to server. Need to fix if it doesnt work
        static string address = "net.pipe://localhost/pipeMSSS";
        static NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
        static EndpointAddress ep = new EndpointAddress(address);
        readonly IAstroContract channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);


        #region BUTTON CALCULATIONS
        // Example Calculation: OWL = 500.1 RWL = 500 V should equal damn near 60000m/s
        private void button_Calc_Star_Velocity_Click(object sender, EventArgs e)
        {
            if (!ValidDouble(textBox_SV_OWL)) // Checks for valid double
            {
                toolStrip_Text.Text = "Invalid double for Observed Wavelength";
                return;
            }
            else if (!ValidDouble(textBox_SV_RWL)) // Checks for valid double
            {
                toolStrip_Text.Text = "Invalid double for Rested Wavelength";
                return;
            }
            else
            {
                double OWL = double.Parse(textBox_SV_OWL.Text); // Observed Wavelength
                double RWL = double.Parse(textBox_SV_RWL.Text); // Rested Wavelength

                try
                {
                    double calc = channel.StarVelocity(OWL, RWL); // Star Velocity Speed in m/s
                    AddToListView(button_Calc_Star_Velocity.Text, OWL, RWL.ToString(), calc.ToString() + "m/s");
                }
                catch (Exception ex)
                {
                    toolStrip_Text.Text = $"{ex}";
                }
            }
        }


        // Example Calculation: Barnard's Star has parallax angle of 0.547 arcseconds and has a distance = 1.828 parsec
        private void button_Calc_Star_Distance_Click(object sender, EventArgs e)
        {
            if (!ValidDouble(textBox_SD_ASA)) // Checks for valid double
            {
                toolStrip_Text.Text = "Invalid double for Arcsecond Angle";
                return;
            }
            else
            {
                double ASA = double.Parse(textBox_SD_ASA.Text); // Arcsecond Angle

                try
                {
                    double calc = channel.StarDistance(ASA); // Star Distance in parsecs
                    AddToListView(button_Calc_Star_Distance.Text, ASA, "-", calc.ToString() + "parsecs");
                }
                catch (Exception ex)
                {
                    toolStrip_Text.Text = $"{ex}";
                }
            }
        }

        // Example calculation: DC = 0 K = 273.15 
        private void button_Calc_C_To_K_Click(object sender, EventArgs e)
        {
            if (!ValidDouble(textBox_CtK_DC)) // Checks for valid double
            {
                toolStrip_Text.Text = "Invalid double for Degrees Celsius";
                return;
            }
            else
            {
                double DC = double.Parse(textBox_CtK_DC.Text.ToString()); // Degrees Celsius

                try
                {
                    double calc = channel.TemperatureInKelvin(DC); // Temperature converted to Kelvin
                    AddToListView(button_Calc_C_To_K.Text, DC, "-", calc.ToString() + "K");
                }
                catch (Exception ex)
                {
                    toolStrip_Text.Text = $"{ex}";
                }
            }
        }

        // Example calculation: BHM = 605985810220889885728804  6.05985810220889885728804e23
        private void button_Calc_Event_Horizon_Click(object sender, EventArgs e)
        {
            if (!ValidDouble(textBox_EH_BHM))
            {
                toolStrip_Text.Text = "Invalid double for Blackhole mass";
                return;
            }
            else
            {
                double BHM = double.Parse(textBox_EH_BHM.Text.ToString()); // Blackhole Mass in kg

                try
                {
                    double calc = channel.EventHorizon(BHM); // Event horizon radius in metres 
                    AddToListView(button_Calc_Event_Horizon.Text, BHM, "-", calc.ToString() + "m");
                }
                catch (Exception ex)
                {
                    toolStrip_Text.Text = $"{ex}";
                }
            }
        }
        #endregion

        private static bool ValidDouble(System.Windows.Forms.TextBox textbox)
        {
            try { double convertToDouble = double.Parse(textbox.Text);
                convertToDouble = 0.0;
            }
            catch (Exception) { return false; } // throw exception
            return true;
        }

        private void AddToListView(string method, double input1, string input2, string result)
        {
            // https://stackoverflow.com/questions/473148/c-sharp-listview-how-do-i-add-items-to-columns-2-3-and-4-etc
            ListViewItem newItem = new ListViewItem(method); // Create item for the listview
            newItem.SubItems.Add(input1.ToString());
            newItem.SubItems.Add(input2.ToString());
            newItem.SubItems.Add(result.ToString());

            // Add item to the listview
            listViewResults.Items.Add(newItem);
        }



        #region MENU & LANGUAGE
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); // Change language to English
            this.Controls.Clear(); // Clear buttons in UI
            InitializeComponent(); // Reinitialize UI
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr"); // Change language to French
            this.Controls.Clear(); // Clear buttons in UI
            InitializeComponent(); // Reinitialize UI
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        #endregion
    }
}
