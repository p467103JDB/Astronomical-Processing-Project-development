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

// JACK DU BOULAY
// P467103
// 17/09/2024

namespace MSSS_UI_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckConnection();
        }
        // Connection to server on start up
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
                    AddToListView(button_Calc_Star_Velocity.Text, OWL, RWL.ToString(), calc.ToString() + " m/s");
                    ClearTextBoxes();
                    toolStrip_Text.Text = "Method completed successfully";
                }
                catch (Exception)
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
                    AddToListView(button_Calc_Star_Distance.Text, ASA, "-", calc.ToString() + " parsecs");
                }
                catch (Exception)
                {
                    toolStrip_Text.Text = "Could not connect to server";
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
                    AddToListView(button_Calc_C_To_K.Text, DC, "-", calc.ToString() + " K");
                }
                catch (Exception)
                {
                    toolStrip_Text.Text = "Could not connect to server";
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
                    AddToListView(button_Calc_Event_Horizon.Text, BHM, "-", calc.ToString() + " m");
                }
                catch (Exception)
                {
                    toolStrip_Text.Text = "Could not connect to server";
                }
            }
        }

        // CLEAR ALL BUTTON
        private void buttonClearData_Click(object sender, EventArgs e)
        {
            listViewResults.Items.Clear();
            ClearTextBoxes();

        }
        #endregion

        // Clear Textboxes
        private void ClearTextBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.Text = "";
                }
            }
        }

        // Double Validator
        private static bool ValidDouble(System.Windows.Forms.TextBox textbox)
        {
            try
            {
                double.Parse(textbox.Text); // Check if it can parse the text as a double, if it fails then returns false
                return true;
            }
            catch (Exception) // If it cannot parse it as a double then it returns false
            {
                textbox.Clear();
                textbox.Focus();
                return false;
            }
        }

        // Add item to ListView 
        private void AddToListView(string method, double input1, string input2, string result)
        {
            // https://stackoverflow.com/questions/473148/c-sharp-listview-how-do-i-add-items-to-columns-2-3-and-4-etc
            ListViewItem newItem = new ListViewItem(method); // Create item for the listview
            newItem.SubItems.Add(input1.ToString());
            newItem.SubItems.Add(input2.ToString());
            newItem.SubItems.Add(result.ToString());

            // Add item to the listview
            listViewResults.Items.Add(newItem);

            ClearTextBoxes();
            toolStrip_Text.Text = "Method completed successfully";
        }

        #region UI THEME AND BUTTON PICKER
        // Q7.4 Change Form UI Style
        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.Control;
            this.menuStrip1.BackColor = SystemColors.Control;
            this.statusStrip1.BackColor = SystemColors.Control;
            this.listViewResults.BackColor = SystemColors.Window;

            // Although it might be slow, it goes through all controls to check for button or textboxes
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BackColor = SystemColors.Window;
                }
                else if (control is System.Windows.Forms.Button button)
                {
                    button.BackColor = SystemColors.Control;
                }
            }
        }

        private void darkGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Changes colours to UI to gray and dark gray
            this.BackColor = System.Drawing.Color.Gray;
            this.menuStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.statusStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.listViewResults.BackColor = System.Drawing.Color.DarkGray;

            // Although it might be slow, it goes through all controls to check for button or textboxes
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BackColor = Color.DarkGray;
                }
                else if (control is System.Windows.Forms.Button button)
                {
                    button.BackColor = SystemColors.Control;
                }
            }
        }

        // Q7.5 Background Colour Picker
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

        // BUTTON COLOUR PICKER
        private void customizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Open colorDialog for 
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

        // Q7.6
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
        #endregion

        #region LANGUAGES
        /// Q7.3 LANGUAGE OPTIONS
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ListViewItem> currentList = GetCurrenTList();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); // Change language to English
            defaultToolStripMenuItem_Click(sender, e);
            ClearControls();
            DisplayCurrentList(currentList);

        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ListViewItem> currentList = GetCurrenTList();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr"); // Change language to French
            defaultToolStripMenuItem_Click(sender, e);
            ClearControls();
            DisplayCurrentList(currentList);


        }

        private void germanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ListViewItem> currentList = GetCurrenTList();
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de"); // Change language to German
            defaultToolStripMenuItem_Click(sender, e);
            ClearControls();
            DisplayCurrentList(currentList);
        }

        private void ClearControls()
        {
            this.Controls.Clear(); // Clear buttons in UI
            InitializeComponent(); // Reinitialize UI
        }

        #region Displaying Results
        // This method is to make sure we dont lose our results once we have changed language
        private List<ListViewItem> GetCurrenTList() 
        {
            List<ListViewItem> currentList = new List<ListViewItem>();

            if (listViewResults.Items.Count > 0)
            {
                for (int i = 0; i < listViewResults.Items.Count; i++)
                {
                    ListViewItem newItem = new ListViewItem(listViewResults.Items[i].Text);

                    for (int j = 1 ; j <= 3 ; j++)
                    {
                        newItem.SubItems.Add(listViewResults.Items[i].SubItems[j].Text);
                    }
                    // Add item to the listview
                    currentList.Add(newItem);
                }
                return currentList;
            }
            return currentList;
        }

        // This one displays the returned results once the clearing has been done for the controls
        private void DisplayCurrentList(List<ListViewItem> cList)
        {
            foreach (ListViewItem item in cList)
            {
                listViewResults.Items.Add(item);
            }
        }
        #endregion

        #endregion

        #region MENU OPTIONS
        // Check connection to server
        private void testConnectionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private void CheckConnection()
        {
            try
            {
                // Call simple method to test connection.
                double testResult = channel.TemperatureInKelvin(0);
                toolStrip_Text.Text = "Connected to the service successfully!";
            }
            catch (Exception)
            {
                toolStrip_Text.Text = "Failed to connect to the service. If server was not open before opening this program. Please restart client.";
            }
        }

        // Exit Program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion
    }
}
