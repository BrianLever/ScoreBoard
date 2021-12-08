using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace SATRScoreDisplay
{


    public partial class OnePointDomination : SATRScoreDisplay.BaseScoreboard
    {
        const int standardfont = 60;
        
        private OleDbConnection connection = new OleDbConnection();
        int AlphaScore = 0;
        int BravoScore = 0;

        public OnePointDomination()
        {
            InitializeComponent();
        }

        protected override void ResizeScreen()
        {
            SetTimePosition();
            SetBattlePosition();
            SetProgressPosition();
            SetPingPosition();
        }

        private void OnePointDomination_Load(object sender, EventArgs e)
        {

            ResizeScreen();
            BattleLabel.Text = Convert.ToString(Program.ScoreboardBattleCode + 1);
            SetTimeLeftVisibility(true);
           // MessageBox.Show("Intial Load");
            REfreshResults();
            //LoadScores();
            CheckTestMode();

        }
        private void CheckTestMode()
        {
            if (Program.TestMode)
            {
                TestResponseChk.Visible = true;
                TestResponseChk.Checked = true;
            }
            else
            {
                TestResponseChk.Visible = false;
                TestResponseChk.Checked = false;
            }
        }
        private void REfreshResults()
        {
            if (TraceSendChk.Checked)
                DisableRefreshGridTimer();
               

            if (!SynchInProgress)

                Cursor.Current = Cursors.WaitCursor;
            //Update The screen from the database.
            //MessageBox.Show("Update Grid");
            if (connection.State == ConnectionState.Closed)
            {

                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
            }
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            //                Program.ScoreboardBattleCode = reader.GetInt16(1);

            //SET ALL DEVICES TO UNPINGED.
            command.CommandText = "UPDATE SynchronisedDevice SET Ping = false  WHERE Device_Role_Code = 12; ";
            command.ExecuteNonQuery();

            //PING EACH DEVICE
            command.CommandText = "SELECT SATR_Unit_ID FROM SynchronisedDevice WHERE Device_Role_Code = 12  AND SynchronisedDevice.Enabled = True ";//Find all the Domination Boxes
            OleDbDataReader reader = command.ExecuteReader();

            try
            {
                int SATRID = 0;
                if (reader.Read())//Look at the first Domination box found 
                {
                    SATRID = reader.GetInt32(0);

                }
                else
                {
                    DisableRefreshGridTimer();
                    MessageBox.Show("No Domination Box Synchronised.");
                    EnableRefreshGridTimer();
                }
                reader.Close();
                connection.Close();

                //Monitor Details

                Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                SynchInProgress = true;
                EnableWaitTimer();
                

                while (SynchInProgress) { Application.DoEvents(); }//wait for the timer to complete 400ms for a response before sending next request

                if (TestResponseChk.Checked)//DELETE IN PRODUCTION
                {

                    TestDeviceResponse(SATRID);
                    SynchInProgress = true;
                    EnableWaitTimer();
                    while (SynchInProgress)
                    { //wait for the timer to complete 200ms before generating a test response

                        Application.DoEvents();
                    }
                }
            }

            finally
            {
                Cursor.Current = Cursors.Default;
            }
            LoadScores();
            SynchInProgress = true;
            EnableWaitTimer();
            while (SynchInProgress)
                Application.DoEvents();
            LoadScores();


        }

        protected override void RefrehGridTimer_Tick(object sender, EventArgs e)
        {
            REfreshResults();
          
        }


       private void LoadScores()
        {
           // MessageBox.Show("Load Scores");
            string directoryName = Program.rootdirectory;

            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            //GET PING STATUS
            int SATRID = 0;
            command.CommandText = "SELECT Ping, SATR_Unit_ID FROM SynchronisedDevice WHERE Device_Role_Code = 12  AND SynchronisedDevice.Enabled = True";//Find all the Domination Boxes
            OleDbDataReader reader = command.ExecuteReader();
            Boolean Ping = false;
            if (reader.Read())
            {
                Ping = reader.GetBoolean(0);
                SATRID = reader.GetInt32(1);
            }
            reader.Close();
         

            string pingfile;
            if (Ping)
                pingfile = "RedPing.png";
            else
                pingfile = "NoPing.png";
            String ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + pingfile;
            if (!File.Exists(ImageFileName))//
                ImageFileName = directoryName + @"\Images\" + pingfile;
            BravoPing.BackgroundImage = Image.FromFile(ImageFileName);
            

            //GET THE TEAM TIME SCORES FOR ALPHA AND BRAVO TEAMS
           
            for (int i =1; i<3;i++)
            {
                command.CommandText = "SELECT Score FROM TeamScore WHERE SATR_Unit_ID = " + Convert.ToString(SATRID) + " AND Team_Code = " + Convert.ToString(i) + ";";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TimeSpan ControlTime;
                    if (i == 1)//ALPHA TEAM
                    {
                        AlphaScore = reader.GetInt32(0);
                        ControlTime = TimeSpan.FromSeconds(AlphaScore);
                        AlphaTimeLabel.Text = ControlTime.ToString();
                    }
                    else
                    {
                        BravoScore = reader.GetInt32(0);
                        ControlTime = TimeSpan.FromSeconds(BravoScore);
                        BravoTimeLabel.Text = ControlTime.ToString();
                    }
                }
                reader.Close();
            }
            if (AlphaScore > 0 || BravoScore > 0)
            {
                int TotalScore = AlphaScore + BravoScore;
                int AlphaPercentage = (int)AlphaScore * 100 / TotalScore;
                int BravoaPercentage = (int)BravoScore * 100 / TotalScore;
                AlphaProgress.Value = AlphaPercentage;
                BravoProgress.Value = BravoaPercentage;

            }
            else
            {
                AlphaProgress.Value = 0;
                BravoProgress.Value = 0;
            }
            connection.Close();
        }

        protected override void TriggerResultDisplay()
        {
            LoadScores();//GET THE LATEST SCORES FROM DB.
            if (AlphaScore > BravoScore)
                TeamResult(1);
            else
            if (BravoScore > AlphaScore)
                TeamResult(2);
            else
                TeamResult(0);
               
        }

        private void TestDeviceResponse(int SATRID)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
            }
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM SynchronisedDevice WHERE SATR_UNIT_ID = " + Convert.ToString(SATRID) + "; ";
            OleDbDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {
                int DeviceID = reader.GetInt32(0);
                // string Alias = reader[1].ToString();
                Int16 DeviceRoleCode = Convert.ToInt16(reader.GetInt32(2));
                Int16 TeamCode = Program.RandomNumber16(0, 7);
     

                String StatusBar = "";
                int TeamAScore = Program.RandomNumber(0, (15*60));
                int TeamBScore = Program.RandomNumber(100, 1000)+100;
                reader.Close();
                connection.Close();

                Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode, DeviceID, Program.ScoreboardID, TeamCode, TeamAScore, TeamBScore, 0, 0, 0, 0, 0, DeviceRoleCode, StatusBar);

            }

            connection.Close();
        }



        private void SetTimePosition()
        {
           
            //283, 911
            int x = (int)(305 * this.Width / Program.ScreenWidth);
            int y = (int)(920 * this.Height / Program.ScreenHeight);

            AlphaTimeLabel.Location = new Point(x, y);
            int fontheight = (int)(standardfont * this.Height / Program.ScreenHeight);

            AlphaTimeLabel.Font = new Font(AlphaTimeLabel.Font.FontFamily, fontheight);
            //1890, 911
            x = (int)(1912 * this.Width / Program.ScreenWidth);
            y = (int)(920 * this.Height / Program.ScreenHeight);

            BravoTimeLabel.Location = new Point(x, y);
            BravoTimeLabel.Font = new Font(BravoTimeLabel.Font.FontFamily, fontheight);
        }

        private void SetBattlePosition()
        {
            //1232, 656
            int x = (int)(1232 * this.Width / Program.ScreenWidth);
            int y = (int)(656 * this.Height / Program.ScreenHeight);

            BattleLabel.Location = new Point(x, y);
            
            int fontheight = (int)(standardfont * this.Height / Program.ScreenHeight);
            BattleLabel.Font = new Font(BattleLabel.Font.FontFamily, fontheight);
        }

        private void SetProgressPosition()
        {
            //1001, 931
            int x = (int)(1001 * this.Width / Program.ScreenWidth);
            int y = (int)(931 * this.Height / Program.ScreenHeight);

            AlphaProgress.Location = new Point(x, y);

            //563, 95
            x = (int)(563 * this.Width / Program.ScreenWidth);
            y = (int)(95 * this.Height / Program.ScreenHeight);
            AlphaProgress.Size = new Size(x, y);
            BravoProgress.Size = new Size(x, y);

            //1001, 1032
            x = (int)(1001 * this.Width / Program.ScreenWidth);
            y = (int)(1032 * this.Height / Program.ScreenHeight);

            BravoProgress.Location = new Point(x, y);
        }

        private void SetPingPosition()
        {
            //35, 35
           int x = (int)(35 * this.Width / Program.ScreenWidth);
           int y = (int)(35 * this.Height / Program.ScreenHeight);
            
            BravoPing.Size = new Size(x, y);
            BravoPing.Location = new Point(Program.xLocation(1296,this.Width), Program.yLocation(841, this.Height));//1296, 841


        }

        private void BravoPing_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopCheckMissionTimer();
            DisableRefreshGridTimer();
            StopSecondTickTimer();
            DisableWaitTimer();

            Program.rf.rfPortClose();
            Program.rf.rfPortOpen();
            EnableRefreshGridTimer();
        }

        private void TraceSendChk_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
