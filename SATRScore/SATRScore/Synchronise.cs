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
    public partial class SynchroniseForm : Form
    {
        private int ResponseTimerMS = 1000;
        public string DBConnection;
        private OleDbConnection connection = new OleDbConnection();
        public string GenreFolder;
        public Int16 GenreCode;
        public Image BackButtonImage;
        public bool GoHome = false;
        public bool closeapp = false;

        Int16[] DeviceCodes = new Int16[10];
        Int16 DeviceRoleCode=1;
        string[] DeviceNames = new string[10];
        PictureBox[] DeviceBoxes = new PictureBox[10];
        Label[] DeviceLabels = new Label[10];
        public ConfigData cd = new ConfigData();

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private int MaxGamingGuns = 0;
        private int ProgressTicks = 0;



        public SynchroniseForm()
        {
            InitializeComponent();
        }

        private int ResponseTime()//If not radio repeater, then shorter response time, 400MS else 3000MS
        {
            int ReturnedResponseTime = 0;
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Radio_Repeater FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (!reader.GetBoolean(0))
                    ReturnedResponseTime = Program.RFWaitTimeMS*2;
                else
                    ReturnedResponseTime = ResponseTimerMS;
            }
            else

            reader.Close();
            connection.Close();
            return ReturnedResponseTime;
        }
        private void DeviceBox_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.  
            String name = null;
            if (sender is PictureBox)
            {
                name = (sender as PictureBox).Name;
            }
            else if (sender is Label)
            {
                name = (sender as Label).Name;
            }
            else
                name = "Not picture box";
          
            int DeviceIndex = Convert.ToInt32(name);
            DeviceRoleCode = DeviceCodes[DeviceIndex];
            if (DeviceRoleCode == 1)//GAMER
            {
                TotalGamers TG = new TotalGamers();
                TG.ShowDialog();
                MaxGamingGuns = TG.GamingGunLimit;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                SyncTimer.Interval = TG.GamingGunLimit * ResponseTime();
                ProgressTicks = 0;
                ProgressTimer.Enabled = false;
                ProgressTimer.Interval = ResponseTime();
                ProgressTimer.Enabled = true;


            }
            else
            {
                MaxGamingGuns = 7;
                ProgressTimer.Enabled = false;
                progressBar1.Visible = true;

                SyncTimer.Interval = MaxGamingGuns * ResponseTime();//Wait for 5 devices to Sync
                ProgressTicks = 0;
                ProgressTimer.Enabled = false;
                ProgressTimer.Interval = ResponseTime();
                ProgressTimer.Enabled = true;
            }
            Cursor.Current = Cursors.WaitCursor;
            Program.NewSyncDevices = 0;
            Program.UpdatedSyncDevices = 0;
            //SEND REQUEST TO DEVICES TO RESPOND
            Program.rf.rfPortOpen(false);
            SyncPicture.Visible = true;
            SyncTimer.Enabled = true;
            Program.SendRadioPacket(cd.DBLevel, 24, cd.BattleCode, 0, DeviceRoleCode, 0, 0, 0, 0, 0, 0, 0, 0, "");//send a request to send back list of Devices on same battle with the right device role
            while (SyncTimer.Enabled)
            {
                Application.DoEvents();
                if (Program.NewSyncDevices >= MaxGamingGuns)
                {
                    EndSync();
                }
            }
            
          
        }
        private void SynchroniseForm_Load(object sender, EventArgs e)
        {
            SyncPicture.Visible = false;
            //LOAD THE RIGHT IMAGE FOR THE BACK BUTTON
            BackBtn.BackgroundImage = BackButtonImage;
            //LOAD BACK GROUND

            string directoryName = Program.rootdirectory;
            string ImageFileName = directoryName + @"\Backgrounds\" + GenreFolder + @"\SyncDevices.png";
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Backgrounds\SyncDevices.png";
            this.BackgroundImage = Image.FromFile(ImageFileName);

            //LOAD DEVICE ROLES ALLOWED FOR THIS GENRE
            //LOAD MISSION BADGES
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            List<Control> listControls = new List<Control>();

            foreach (Control control in DeviceTypePanel.Controls)
            {
                listControls.Add(control);
            }

            foreach (Control control in listControls)
            {
                DeviceTypePanel.Controls.Remove(control);
                control.Dispose();
            }

            int Devices = 0;
            int Deviceindex = 0;


            command.CommandText = "SELECT Device_Code, Device_Name FROM DeviceByGenre WHERE Genre_Code = " + Convert.ToString(GenreCode) + ";";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read() && Devices < 11)
            {
                Devices++;
                DeviceCodes[Devices] = reader.GetInt16(0);
                DeviceNames[Devices] = reader[1].ToString();
            }
            reader.Close();

            while (Deviceindex <= Devices && Deviceindex < 11)
            {
                Deviceindex++;
                command.CommandText = "SELECT Badge_FileName FROM DeviceRole WHERE Device_Role_Code = " + Convert.ToString(DeviceCodes[Deviceindex]) + ";";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string badgefilename = reader[0].ToString();
                    DeviceLabels[Deviceindex] = new Label();
                    DeviceBoxes[Deviceindex] = new PictureBox();
                    DeviceBoxes[Deviceindex].Height = 140;
                    DeviceBoxes[Deviceindex].Width = DeviceBoxes[Deviceindex].Height;

                    ImageFileName = directoryName + @"\Badges\" + GenreFolder + @"\" + badgefilename;
                    if (!File.Exists(ImageFileName))//
                        ImageFileName = directoryName + @"\Badges\" + badgefilename;
                    if (File.Exists(ImageFileName))
                    {
                        DeviceBoxes[Deviceindex].SizeMode = PictureBoxSizeMode.StretchImage;
                        DeviceBoxes[Deviceindex].ImageLocation = ImageFileName;
                        DeviceBoxes[Deviceindex].Click += new EventHandler(DeviceBox_Click);
                        DeviceBoxes[Deviceindex].Name = Convert.ToString(Deviceindex);
                        DeviceTypePanel.Controls.Add(DeviceBoxes[Deviceindex]);

                    }
                    //lab.Font = new Font("Arial", 20);
                    DeviceLabels[Deviceindex].AutoSize = false;
                    DeviceLabels[Deviceindex].Font = new Font(FontFamily.GenericSansSerif,
                    14.25F, FontStyle.Bold);
                    //Roboto, 14.25pt, style=Bold
                    DeviceLabels[Deviceindex].Text = DeviceNames[Deviceindex];
                    DeviceLabels[Deviceindex].ForeColor = System.Drawing.Color.White;

                    DeviceLabels[Deviceindex].MinimumSize = new Size(250, 140);
                    DeviceLabels[Deviceindex].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    DeviceLabels[Deviceindex].Click += new EventHandler(DeviceBox_Click);
                    DeviceLabels[Deviceindex].Name = Convert.ToString(Deviceindex);
                    DeviceTypePanel.Controls.Add(DeviceLabels[Deviceindex]);
                }
                reader.Close();

            }
            connection.Close();
            if (Program.TestMode)
                TestResponseBtn.Visible = true;
            else
                TestResponseBtn.Visible = false;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            GoHome = false;
            this.Close();
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            GoHome = true;
            this.Close();
        }

        private void Mission1Box_Click(object sender, EventArgs e)
        {

        }

        static int RandomNumber(int min, int max)
        {
            Random random = new Random(); return random.Next(min, max);

        }

        static Int16 RandomNumber16(int min, int max)
        {
            Random random = new Random(); return Convert.ToInt16(random.Next(min, max));

        }

     

        private void TestResponseBtn_Click(object sender, EventArgs e)
        {
            Program.ReceiveRadioPacket(25, Program.ScoreboardBattleCode, RandomNumber(1, 2043), Program.ScoreboardID, DeviceRoleCode, RandomNumber16(0, 3), 0, 0, 0, 0, 0, 0, 0, "NO ALIAS");
         //   SQLText.Text = Program.textSQLstring;
         
        }

        private void EndSync()
        {
            SyncTimer.Enabled = false;
            Cursor.Current = Cursors.Default;
            SyncPicture.Visible = false;
            Program.rf.rfPortClose();
            MessageBox.Show("NEW: " + Convert.ToString(Program.NewSyncDevices), "Device SYNC");
            this.Close();
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {

                EndSync();
        }

        private void CloseAppPicture_Click(object sender, EventArgs e)
        {
            ConfigData cd = new ConfigData();
            closeapp = cd.closeappcheck();
            if (closeapp)
                this.Close();
        }

        private void SynchroniseForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void SynchroniseForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void SynchroniseForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Mission1Label_Click(object sender, EventArgs e)
        {

        }

        private void ProgresTimer_Tick(object sender, EventArgs e)
        {
            ProgressTicks++;
            progressBar1.Value = (int) ((float)ProgressTicks * 100 / (float)MaxGamingGuns);
            if (ProgressTicks >= MaxGamingGuns)
            {
                progressBar1.Value = 100;
                ProgressTimer.Enabled = false;
            }
        }

        private void DeviceTypePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
