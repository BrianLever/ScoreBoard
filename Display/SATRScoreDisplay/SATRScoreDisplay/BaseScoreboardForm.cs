using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace SATRScoreDisplay
{
    public partial class BaseScoreboard : Form
    {
        public bool CloseApp = false;
        public int FormMissionCode;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public string backgroundfilename;
        const int standardfont = 72;
        public bool SynchInProgress = false;
        public bool ShowStatusText = true;

        private const int bottombuttonsY = 1350;

        ConfigData cd = new ConfigData();

        private OleDbConnection connection = new OleDbConnection();

        public BaseScoreboard()
        {
            InitializeComponent();
        }

        public void  SetTimeLeftVisibility(bool visibility)
        {
            TimeLeftLabel.Visible = visibility;
        }
        private void LoadTimeButtons()
        {

            string directoryName = Program.rootdirectory;
            //SEARCH GENRE FOLDER FIRST
            string ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\TimeBtn.png";
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Images\TimeBtn.png";
            //MessageBox.Show(ImageFileName);
            GameTimeBtn.BackgroundImage = Bitmap.FromFile(ImageFileName);
            TimeofDayBtn.BackgroundImage = Bitmap.FromFile(ImageFileName);


            /*int xposition = (int)(this.Width - (500 * this.Width / Program.ScreenWidth));
            int yposition = (int)(this.Height - (110 * this.Height / Program.ScreenHeight));*/
            //            GameTimeBtn.Location = new Point(xposition, yposition);
            GameTimeBtn.Location = new Point(Program.xLocation(2145, this.Width), Program.yLocation(bottombuttonsY, this.Height));

            //xposition = (int)(this.Width - (850 * this.Width / Program.ScreenWidth));


            //TimeofDayBtn.Location = new Point(xposition, yposition);//1810, 1350
            TimeofDayBtn.Location = new Point(Program.xLocation(1810, this.Width), Program.yLocation(bottombuttonsY, this.Height));

            //Resize the buttons 302, 81

            int x = (int)(302 * this.Width / Program.ScreenWidth);
            int y = (int)(90 * this.Height / Program.ScreenHeight);
            GameTimeBtn.Size = new Size(x, y);
            TimeofDayBtn.Size = new Size(x, y);

            int buttonheight = (int)(35 * this.Height / Program.ScreenHeight);
            GameTimeBtn.Font = new Font(GameTimeBtn.Font.FontFamily, buttonheight);
            TimeofDayBtn.Font = new Font(TimeofDayBtn.Font.FontFamily, buttonheight);

            //   StatusLabel.Location = new Point(this.Width / 2, this.Height - 250);
            StatusLabel.Size = new Size(this.Width - 20, 110);

            MaximiseBtn.Location = new Point(Program.xLocation(2507, this.Width), Program.yLocation(0, this.Height));
            MaximiseBtn.Size = new Size(Program.xLocation(50, this.Width), Program.yLocation(50, this.Height));


        }

        private void BaseScoreboard_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void BaseScoreboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void BaseScoreboard_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void CloseAppPicture_Click(object sender, EventArgs e)
        {
            Program.rf.rfPortClose();

            DisableWaitTimer();
            CloseApp = true;
            this.Close();
        }

        protected virtual void NewMission()
        {

        }

        public void StopCheckMissionTimer()
        {
            CheckMissionTimer.Stop();
        }

        private void CheckMissionTimer_Tick(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                //CHECK IF A NEW MISSION HAS STARTED AND IF YES HIDE THE RESULT PICTURE

      
                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                command.CommandText = "SELECT New_Mission FROM Config";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.GetBoolean(0))//if this is a new mission
                    {
                        ScorePicture.Visible = false;
                        NewMission();

                        reader.Close();
                        command.CommandText = "UPDATE Config SET New_Mission = false";
                        command.ExecuteNonQuery();
                    }
                    reader.Close();
                }
                connection.Close();


                ConfigData cd = new ConfigData();
                int NewMissionCode = 0;
                NewMissionCode = cd.CheckMission();
                if (FormMissionCode != NewMissionCode)
                { //Mission has changed and therefore load a new scoreboard
                    Program.MissionCode = NewMissionCode;
                    RefrehGridTimer.Enabled = false;
                    DisableWaitTimer();
                    this.Close();
                }

                
            }
    }
      
        public void EnableRefreshGridTimer()
        {
            RefrehGridTimer.Enabled = true;
        }

        public void DisableRefreshGridTimer()
        {
            RefrehGridTimer.Enabled = false;
        }

        public void TeamResult(int WinningTeamCode)
        {

            string directoryName = Program.rootdirectory;

            string filename = "";
            switch (WinningTeamCode)
            {
                case 0: filename = "Draw.png"; break;
                case 1: filename = "AlphaWins.png"; break;
                case 2: filename = "BravoWins.png"; break;
            }
            //SEARCH GENRE FOLDER FIRST
            string ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + filename;
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Images\" + filename;
            if (File.Exists(ImageFileName))
            {
                ScorePicture.BackgroundImage = Image.FromFile(ImageFileName);
                ScorePicture.Visible = true;

            }
        }

  
        private void LoadBackground()
        {
            
            string directoryName = Program.rootdirectory;
            string ImageFileName = directoryName + @"\Backgrounds\" + Program.GenreFolder + @"\" + backgroundfilename;
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Backgrounds\" + backgroundfilename;
            this.BackgroundImage = Image.FromFile(ImageFileName);

            
            CloseAppPicture.Location = new Point(Program.xLocation(2464, this.Width), Program.yLocation(1345, this.Height));//2464, 1345
          
            CloseAppPicture.Size = new Size(Program.xLocation(89, this.Width), Program.yLocation(89, this.Height));

            int xposition = (int)((12 * this.Width / Program.ScreenWidth));
            int yposition = (int)((1180 * this.Height / Program.ScreenHeight));
            StatusLabel.Location = new Point(xposition, yposition);

            
            int x = (int)(2520 * this.Width / Program.ScreenWidth);
            int y = (int)(110 * this.Height / Program.ScreenHeight);
            StatusLabel.Size = new Size(x, y);

            int buttonheight = (int)(26.25 * this.Height / Program.ScreenHeight);
            StatusLabel.Font = new Font(StatusLabel.Font.FontFamily, buttonheight);
            LoadTimeButtons();
            StatusLabel.SendToBack();

            x = (int)(1073 * this.Width / Program.ScreenWidth);
            y = (int)(340 * this.Height / Program.ScreenHeight);

            TimeLeftLabel.Location = new Point(x, y);
            int fontheight = (int)(standardfont * this.Height / Program.ScreenHeight);
            TimeLeftLabel.Font = new Font(TimeLeftLabel.Font.FontFamily, fontheight);
            //787, 422 Location
            x = (int)(787 * this.Width / Program.ScreenWidth);
            y = (int)(422 * this.Height / Program.ScreenHeight);

            ScorePicture.Location = new Point(x, y);
            //889, 490 size
            x = (int)(889 * this.Width / Program.ScreenWidth);
            y = (int)(490 * this.Height / Program.ScreenHeight);
            ScorePicture.Size = new Size(x, y);
            ScorePicture.BringToFront();

            //1104, 450
            x = (int)(1104 * this.Width / Program.ScreenWidth);
            y = (int)(450 * this.Height / Program.ScreenHeight);

            SyncPicture.Location = new Point(x, y);

        }

        private void BaseScoreboard_Load(object sender, EventArgs e)
        {
            //   if (!this.Site.DesignMode)
            if (!this.DesignMode)
            {
                LoadBackground();
                RefrehGridTimer.Interval = Program.UpdateFrequency * 1000;
                RefrehGridTimer.Enabled = true;
                WaitSyncTimer.Interval = Program.RFWaitTimeMS;

                if (Program.TestMode)
                {
                    TestPhraseBtn.Visible = true;
                    GameTimeSetBtn.Visible = true;
                }
                else
                {
                    TestPhraseBtn.Visible = false;
                    GameTimeSetBtn.Visible = false;
                }
                SetWaitTime(ResponseTime());

            }
        }

        public int ResponseTime()//If not radio repeater, then shorter response time, 400MS else 3000MS
        {
            int ReturnedResponseTime = 0;
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Radio_Repeater, RF_Transmission_Time_MS FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (!reader.GetBoolean(0))
                    ReturnedResponseTime = reader.GetInt32(1) * 2;
                else
                    ReturnedResponseTime = 1000;
            }
            else

                reader.Close();
            connection.Close();
            return ReturnedResponseTime;
        }


        protected virtual void TriggerResultDisplay()
        {

        }

        public void HideStatusLabel()
        {
            StatusLabel.Visible = false;
        }

        public void StopSecondTickTimer()
        {
            SecondTickTimer.Stop();
        }

        private void SecondTickTimer_Tick(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                DateTime localDate = DateTime.Now;
                TimeofDayBtn.Text = localDate.ToLongTimeString();
                if (Program.TimeLeftInGame == TimeSpan.FromSeconds(1))
                {
                    Program.TimeLeftInGame = TimeSpan.Zero;
                    GameTimeBtn.Text = Program.TimeLeftInGame.ToString();
                    TimeLeftLabel.Text = Program.TimeLeftInGame.ToString();
                    TriggerResultDisplay();
                }
                else
                    Program.TimeLeftInGame -= TimeSpan.FromSeconds(1);
                if (Program.TimeLeftInGame.CompareTo(TimeSpan.Zero) < 0)
                    Program.TimeLeftInGame = TimeSpan.Zero;

                GameTimeBtn.Text = Program.TimeLeftInGame.ToString();
                TimeLeftLabel.Text = Program.TimeLeftInGame.ToString();
                StatusLabel.Text = Program.StatusText;
                StatusLabel.Visible = ShowStatusText;
                if (Program.TimeLeftInGame > TimeSpan.FromSeconds(1))
                    ScorePicture.Visible = false;
            }

        }

        private void TestPhraseBtn_Click(object sender, EventArgs e)
        {
            Program.ReceiveRadioPacket(23, Program.ScoreboardBattleCode, Program.ScoreboardID, 0, 0, Program.RandomNumber(0, 64), 0, 0, 0, 0, 0, 0, 0, "");
                
            
        }

        private void GameTimeSetBtn_Click(object sender, EventArgs e)
        {
            Program.ReceiveRadioPacket(17, cd.BattleCode, 2000, 0, 0, 30, 0, 0, 0, 0, 0, 0, 0, "CONTROL1");
        }

        private void StatusLabel_Click(object sender, EventArgs e)
        {

        }

      //  protected virtual void LoadScores() { }

        protected virtual void RefrehGridTimer_Tick(object sender, EventArgs e)
        {
           // if (!DesignMode)
            //    MessageBox.Show("Base Class Timer");
            //Update The screen from the database.
        }

        public void ShowSyncPicture()
        {
            SyncPicture.Visible = true;
            SyncPicture.BringToFront();
        }

        public void HideSyncPicture()
        {
            SyncPicture.Visible = false;
        }

        public void EnableWaitTimer()
        {
            SynchInProgress = true;
            WaitSyncTimer.Enabled = true;
        }

        public void DisableWaitTimer()
        {
            SynchInProgress = false;
            WaitSyncTimer.Enabled = false;
            SyncPicture.Visible = false;
     
    }

        public void SetWaitTime(int WaitTimeMS)
        {
            WaitSyncTimer.Interval = WaitTimeMS;
        }
        public int GetWaitInterval()
        {
            return WaitSyncTimer.Interval;
        }
        private void WaitSyncTimer_Tick(object sender, EventArgs e)
        {
            WaitSyncTimer.Enabled = false;
            SynchInProgress = false;
            //LoadScores();

            //    MessageBox.Show("Wait Complete");
        }

        private void MaximiseBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Program.ScoreboardWidth = this.Width;
            Program.ScoreboardHeight = this.Height;
            this.TopMost = true;
            this.Visible = false;
            LoadTimeButtons();
            LoadBackground();
            ResizeScreen();

            this.Visible = true;
        }

        protected virtual void ResizeScreen()
        {

        }

        private void PortOpen_Click(object sender, EventArgs e)
        {
            if (Program.rf.getAdminConnected() == 0)
            {
                if (Program.rf.serialPort1.IsOpen)
                    MessageBox.Show("RF Port Open/not Admin Locked");
                else
                    MessageBox.Show("RF Port Closed/not admin locked");
            }
            else
            {
                if (Program.rf.serialPort1.IsOpen)
                    MessageBox.Show("RF Port Open/ Admin Locked");
                else
                    MessageBox.Show("RF Port Closed/admin locked");

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.rf.rfPortOpen();
        }
    }
}
