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
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MSSS_UI_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckConnection();
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
                    
                    toolStrip_Text.Text = "Method completed successfully";
                }
                catch (Exception ex)
                {
                    toolStrip_Text.Text = "Could not connect to server";
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
            try 
            { 
                double.Parse(textbox.Text); // Check if it can parse the text as a double, if it fails then returns false
                return true;
            }
            catch (Exception) // If it cannot parse it as a double then it returns false
            {
                return false; 
            }
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
            defaultToolStripMenuItem_Click(sender, e);
            this.Controls.Clear(); // Clear buttons in UI
            InitializeComponent(); // Reinitialize UI
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr"); // Change language to French
            defaultToolStripMenuItem_Click(sender, e);
            this.Controls.Clear(); // Clear buttons in UI
            InitializeComponent(); // Reinitialize UI
        }

        private void germanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de"); // Change language to German
            defaultToolStripMenuItem_Click(sender, e);
            this.Controls.Clear(); // Clear buttons in UI
            InitializeComponent(); // Reinitialize UI
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        // COLOURS + THEMES

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
            this.menuStrip1.BackColor = SystemColors.Control;
            this.statusStrip1.BackColor = SystemColors.Control;
            this.listViewResults.BackColor = SystemColors.Window;

            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BackColor = SystemColors.Window;
                }

                if (control is System.Windows.Forms.Button button)
                {
                    button.BackColor = SystemColors.Control;
                }
            }
        }

        private void darkGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Gray;
            this.menuStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.statusStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.listViewResults.BackColor = System.Drawing.Color.DarkGray;

            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BackColor = Color.DarkGray;
                }

                if (control is System.Windows.Forms.Button button)
                {
                    button.BackColor = SystemColors.Control;
                }
            }
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    this.BackColor = colorDialog.Color;
                }
            }
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Font = fontDialog.Font;
                }
            }
        }

        // BUTTON COLOUR PICKER
        private void customizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {

                    foreach (Control control in this.Controls)
                    {
                        if (control is System.Windows.Forms.Button button)
                        {
                            button.BackColor = colorDialog.Color;
                        }
                    }
                }
            }
        }

        #endregion


        #region CHECK CONNECTION
        private void testConnectionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private void CheckConnection()
        {
            try
            {
                // Call an existing method to check the connection
                // Use simple or dummy parameters if needed
                double testResult = channel.StarVelocity(0, 0); // Replace with a lightweight method
                toolStrip_Text.Text = "Connected to the service successfully!";
            }
            catch (Exception ex)
            {
                toolStrip_Text.Text = "Failed to connect to the service.";
            }
        }
        #endregion
    }
}
