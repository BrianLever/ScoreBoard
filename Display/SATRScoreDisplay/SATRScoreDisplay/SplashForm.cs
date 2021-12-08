using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SATRScoreDisplay
{
    public partial class SplashScreen : Form
    {
        public string DBConnection;
        public bool CloseApp = false;
        private OleDbConnection connection = new OleDbConnection();
        private int countReconnect = 3;

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = Program.version;
            VersionLabel.Parent = PictureBox1;
            label1.Parent = PictureBox1;
            ForceConnectionChk.BackColor = Color.Transparent;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //=======================================================================================
            //===== Finding RF USB module, and Connecting to Com port. 
            //=======================================================================================

            if (ForceConnectionChk.Checked)
            { Program.rf.UnlockAdmin(); }

            string portName = Program.rf.getRFUSBPortName(); //true-connected, false - failed

            if (portName == "")
            {
                lblRFConnect.Text = "Re-connecting...";
                
                countReconnect--;
                if (countReconnect == 0)
                {
                    lblRFConnect.Text = "Failed to connecting!";
                    MessageBox.Show("Failed to connect! Insert RF USB Converter and restart SATRScoreDisplay", "Message");
                    Program.rf.WriteErrorLog("Failed to connect! Insert RF USB Converter and restart SATRScoreDisplay");
                    CloseApp = true;
                }
                else
                    return;
            }
            else
                lblRFConnect.Text = "RF USB Port Name: " + portName;

            timer1.Enabled = false;
            this.Close();
        }

        private void SplashScreen_Activated(object sender, EventArgs e)
        {
            Int16 GenreCode = 0;
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT * FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                GenreCode = reader.GetInt16(0);
                //   MessageBox.Show(Convert.ToString(GenreCode));
                reader.Close();
            }
            else
                MessageBox.Show("Config  file closed");

            command.CommandText = "SELECT * FROM Genre WHERE Genre_Code = " + Convert.ToString(GenreCode) + ";";
            reader = command.ExecuteReader();

            
            string directoryName = Program.rootdirectory;
            //  MessageBox.Show(directoryName);

            if (reader.Read())
            {
                string ImageFileName = directoryName + @"\Backgrounds\" + reader[3].ToString();
                PictureBox1.ImageLocation = ImageFileName;

                //          MessageBox.Show(ImageFileName);
                reader.Close();
            }

            connection.Close();
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
