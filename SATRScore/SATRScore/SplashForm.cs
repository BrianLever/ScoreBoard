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
using System.IO;

namespace SATRScore
{
    public partial class SplashScreen : Form
    {
        public string DBConnection;
        private OleDbConnection connection = new OleDbConnection();
        private int countReconnect = 7;
        public bool CloseApp = false;

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)    
        {

            if (ForceConnectionChk.Checked)
            { Program.rf.UnlockDisplay(); }
            Program.rf.Connect_OLE();
            Program.rf.updateSelfConnect(1);// request rf connection by admin to Display

            int isDisplayConnected = Program.rf.getDisplayConnected(); // wait unless disconnect from ScoreDisplay
            if (isDisplayConnected != 0) return;

            bool isConnected = Program.rf.rfPortOpen(true); //true-connected, false - failed

            if (isConnected == false)
            {
                lblRFConnect.Text = "Re-connecting...";
                countReconnect--;

                if (countReconnect == 2)//Ask if they want to force the connection
                {
                    if (MessageBox.Show("Force RF Connection?","USB RF Connection Locked by Display Application",MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Program.rf.UnlockDisplay();

                }
                    

                if (countReconnect == 0)
                {
                    lblRFConnect.Text = "Failed to connect!";
                    timer1.Enabled = false;
                    MessageBox.Show("Failed to connect! Insert RF USB Converter and restart SATRScore", "Message");

                    Program.rf.updateSelfConnect(0);   //admin disconnected.
                    CloseApp = true;
                    this.Close();
                    return;
   
                }
                else
                    return;
            }
            lblRFConnect.Text = "Connected!";
            timer1.Enabled = false;
            Program.rf.rfPortClose();//31 7 2018 Disconnected after checking can connect.
            this.Close();
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
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

        private Image LoadImage(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = Program.version;
            VersionLabel.Parent = PictureBox1;
            label1.Parent = PictureBox1;
        }
    }
}
