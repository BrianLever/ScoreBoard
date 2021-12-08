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
    public partial class ThreePointDominationForm : SATRScoreDisplay.BaseScoreboard
    {
        const int largefont = 36;
        const int smallfont = 22;
        private int AlphaBoxes = 0;
        private int BravoBoxes = 0;
        private OleDbConnection connection = new OleDbConnection();
  

        public ThreePointDominationForm()
        {
            InitializeComponent();
        }

        private void ThreePointDominationForm_Load(object sender, EventArgs e)
        {
            ResizeScreen();
            BoxCheck();
            REfreshResults();
            //   LoadScores();
            SetTimeLeftVisibility(true);
            CheckTestMode();
        }

        protected override void ResizeScreen()
        {
            SetTimePosition();
            SetLeaderPosition();
            SetProgressPosition();
            SetPingPosition();
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
        private void BoxCheck()
        {

            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            //PING EACH DEVICE
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 12  AND SynchronisedDevice.Enabled = True";
            OleDbDataReader reader = command.ExecuteReader();
            
            try
            {
                if (reader.Read())//Look at the first Domination box 
                {
                    if (reader.GetInt32(0) < 3)
                        MessageBox.Show("Insufficient Domination Boxes Synchronised. "+Convert.ToString(reader.GetInt32(0)));
                    else
                        if (reader.GetInt32(0) > 3)
                        MessageBox.Show("Too many Domination Boxes Synchronised. " + Convert.ToString(reader.GetInt32(0)));

                }
                reader.Close();
            }
            finally
            {
                connection.Close();
            }
        }
        private void REfreshResults()
        {
            TestOutput.Clear();
            //  SetWaitTime(1000);
            int[] DeviceList = new int[4];
            if (!SynchInProgress)

                Cursor.Current = Cursors.WaitCursor;
            //Update The screen from the database.
            //MessageBox.Show("Update Grid");
            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                //                Program.ScoreboardBattleCode = reader.GetInt16(1);
                // TestOutput.AppendText("Clear Ping\r\n");
                //SET ALL DEVICES TO UNPINGED.
                command.CommandText = "UPDATE SynchronisedDevice SET Ping = false  WHERE Device_Role_Code = 12 AND SynchronisedDevice.Enabled = True; ";
                command.ExecuteNonQuery();

                //PING EACH DEVICE
                command.CommandText = "SELECT SATR_Unit_ID FROM SynchronisedDevice WHERE Device_Role_Code = 12 AND SynchronisedDevice.Enabled = True ORDER BY ALIAS ";//Find all the Domination Boxes
                OleDbDataReader reader = command.ExecuteReader();
                int i = 0;
                try
                {

                    if (reader.Read())//Look at the first Domination box found 
                    {
                        i = 1;
                        DeviceList[0] = reader.GetInt32(0);
                        if (reader.Read())//Look at the 2nd Domination box found 
                        {
                            i = 2;
                            DeviceList[1] = reader.GetInt32(0);
                            if (reader.Read())//Look at the 3rd Domination box found 
                            {
                                i = 3;
                                DeviceList[2] = reader.GetInt32(0);
                            }
                        }
                    }

                    reader.Close();
                    connection.Close();

                    //Monitor Details
                    for (int j = 0; j < i; j++)
                    {
                        if (CloseApp) break;
                        Program.LastException = "";
                        TestOutput.AppendText("Monitor " + Convert.ToString(DeviceList[j]) + "\r\n");
                        Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, DeviceList[j], 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                        SynchInProgress = true;

                        //   TestOutput.AppendText("Wait MS" +  Convert.ToString(GetWaitInterval()) + "\r\n");
                        EnableWaitTimer();

                        while (SynchInProgress) { Application.DoEvents();
                            if (CloseApp) { DisableWaitTimer(); } }//wait for the timer to complete 400ms for a response before sending next request
                        TestOutput.AppendText("Sync Time Expired\r\n");
                        if (Program.LastException != "")
                            TestOutput.AppendText("EXCEPTION:" + Program.LastException + "\r\n");
                        if (TestResponseChk.Checked)//DELETE IN PRODUCTION
                        {

                            TestDeviceResponse(DeviceList[j]);
                            SynchInProgress = true;
                            EnableWaitTimer();
                            while (SynchInProgress)
                            { //wait for the timer to complete 200ms before generating a test response

                                Application.DoEvents();
                            }
                        }
                    }
                }

                finally
                {
                    Cursor.Current = Cursors.Default;
                }
                TestOutput.AppendText("Load Scores\r\n");
                LoadScores();

            }
        }
        protected override void RefrehGridTimer_Tick(object sender, EventArgs e)
        {
            REfreshResults();
        }

        protected override void TriggerResultDisplay()
        {
            LoadScores();//GET THE LATEST SCORES FROM DB.
            if (AlphaBoxes > BravoBoxes)
                TeamResult(1);
            else
            if (BravoBoxes > AlphaBoxes)
                TeamResult(2);
            else
                TeamResult(0);

        }

        private void LoadScores()
        {

            AlphaBoxes = 0;
            BravoBoxes = 0;

            string directoryName = Program.rootdirectory;

            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            int[] DeviceList = new int[4];
            bool[] PingList = new bool[4];

            //GET PING STATUS

            command.CommandText = "SELECT Ping, SATR_Unit_ID, ALIAS FROM SynchronisedDevice WHERE Device_Role_Code = 12 AND SynchronisedDevice.Enabled = True ORDER BY ALIAS ";//Find all the Domination Boxes
            OleDbDataReader reader = command.ExecuteReader();
            int i = 0;
            for (i = 0; i < 4; i++)
            {
                string BoxAlias = "";
                PingList[i] = false;
                DeviceList[i] = 0;
                if (reader.Read())
                {
                    PingList[i] = reader.GetBoolean(0);
                    DeviceList[i] = reader.GetInt32(1);
                    BoxAlias = reader[2].ToString();
                }
                switch (i)
                {
                    case 0: Alpha1Label.Text = BoxAlias; Dom1Alias.Text = Alpha1Label.Text; Bravo1Label.Text = Alpha1Label.Text; break;
                    case 1: Alpha2Label.Text = BoxAlias; Dom2Alias.Text = Alpha2Label.Text; Bravo2Label.Text = Alpha2Label.Text; break;
                    case 2: Alpha3Label.Text = BoxAlias; Dom3Alias.Text = Alpha3Label.Text; Bravo3Label.Text = Alpha3Label.Text; break;
                }
            }
            reader.Close();
            for (int l = 0;l<i;l++)
            { 
                string pingfile;
                if (PingList[l] && DeviceList[l] > 0)
                    pingfile = "RedPing.png";
                else
                    pingfile = "NoPing.png";
                String ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + pingfile;
                if (!File.Exists(ImageFileName))//
                    ImageFileName = directoryName + @"\Images\" + pingfile;
                switch (l)
                {
                    case 0: Box1Ping.BackgroundImage = Image.FromFile(ImageFileName); break;
                    case 1: Box2Ping.BackgroundImage = Image.FromFile(ImageFileName); break;
                    case 2: Box3Ping.BackgroundImage = Image.FromFile(ImageFileName); break;
                }
                //GET THE TEAM TIME SCORES FOR ALPHA AND BRAVO TEAMS
                int AlphaScore = 0;
                int BravoScore = 0;
                TimeSpan ControlTime;
                if (DeviceList[l] == 0)//No Box not found in sycnhronise list
                {
                    ControlTime = TimeSpan.FromSeconds(0);
                    switch (l)
                    {
                        case 0: Box1Alpha.Text = ControlTime.ToString(); Box1Bravo.Text = Box1Alpha.Text; break;
                        case 1: Box2Alpha.Text = ControlTime.ToString(); Box2Bravo.Text = Box2Alpha.Text; break;
                        case 2: Box3Alpha.Text = ControlTime.ToString(); Box3Bravo.Text = Box2Alpha.Text; break;
                    }
                }
                else
                {

                    for (int j = 1; j < 3; j++)
                    {
                        if (DeviceList[l] > 0)
                        {
                            command.CommandText = "SELECT Score FROM TeamScore WHERE SATR_Unit_ID = " + Convert.ToString(DeviceList[l]) + " AND Team_Code = " + Convert.ToString(j) + ";";
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {

                                if (j == 1)//ALPHA TEAM
                                {
                                    AlphaScore = reader.GetInt32(0);
                                    ControlTime = TimeSpan.FromSeconds(AlphaScore);
                                    switch (l)
                                    {
                                        case 0: Box1Alpha.Text = ControlTime.ToString(); break;
                                        case 1: Box2Alpha.Text = ControlTime.ToString(); break;
                                        case 2: Box3Alpha.Text = ControlTime.ToString(); break;
                                    }
                                }
                                else
                                {
                                    BravoScore = reader.GetInt32(0);
                                    ControlTime = TimeSpan.FromSeconds(BravoScore);
                                    switch (l)
                                    {
                                        case 0: Box1Bravo.Text = ControlTime.ToString(); break;
                                        case 1: Box2Bravo.Text = ControlTime.ToString(); break;
                                        case 2: Box3Bravo.Text = ControlTime.ToString(); break;
                                    }
                                }
                            }
                            reader.Close();
                        }
                        else
                        {
                            switch (l)
                            {
                                case 0: Box1Bravo.Text = ""; break;
                                case 1: Box2Bravo.Text = ""; break;
                                case 2: Box3Bravo.Text = ""; break;
                            }

                        }
                    }
                }
                int AlphaPercentage = 0;
                int BravoPercentage = 0;

                if (AlphaScore > 0 || BravoScore > 0)
                {
                    int TotalScore = AlphaScore + BravoScore;
                    AlphaPercentage = (int)AlphaScore * 100 / TotalScore;
                    BravoPercentage = (int)BravoScore * 100 / TotalScore;
                }
                switch (l)
                {
                    case 0: Box1AlphaProgress.Value = AlphaPercentage; Box1BravoProgress.Value = BravoPercentage; break;
                    case 1: Box2AlphaProgress.Value = AlphaPercentage; Box2BravoProgress.Value = BravoPercentage; break;
                    case 2: Box3AlphaProgress.Value = AlphaPercentage; Box3BravoProgress.Value = BravoPercentage; break;

                }

                if (AlphaScore > BravoScore)
                    AlphaBoxes++;
                else if (BravoScore > AlphaScore)
                    BravoBoxes++;

                
            }
            reader.Close();

            int WinningTeamCode = 0;
            if (AlphaBoxes > BravoBoxes)
                WinningTeamCode = 1;
            else
            if (BravoBoxes > AlphaBoxes)
                WinningTeamCode = 2;

            //LeaderLabel
            if (WinningTeamCode == 0)
                LeaderLabel.Text = "Tie";
            else
            {

                command.CommandText = "SELECT TeamsByGenre.Team_Name FROM Config INNER JOIN TeamsByGenre ON Config.Genre_Code = TeamsByGenre.Genre_Code WHERE(((TeamsByGenre.Team_Code) = " + Convert.ToString(WinningTeamCode) + "));";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LeaderLabel.Text = reader[0].ToString();
                }
                reader.Close();
            }
            connection.Close();
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
                int TeamAScore = Program.RandomNumber(0, (15 * 60));
                int TeamBScore = Program.RandomNumber(100, (15*50));
                reader.Close();
                connection.Close();

                Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode, DeviceID, Program.ScoreboardID, TeamCode, TeamAScore, TeamBScore, 0, 0, 0, 0, 0, DeviceRoleCode, StatusBar);

            }

            connection.Close();
        }

        private void SetTimePosition()
        {
            
            Alpha1Label.Location = new Point(Program.xLocation(598, this.Width), Program.yLocation(480, this.Height));
            Alpha1Label.Font = new Font(Alpha1Label.Font.FontFamily, Program.fontheight(smallfont,this.Height), FontStyle.Bold);
            Alpha2Label.Location = new Point(Program.xLocation(598, this.Width), Program.yLocation(671, this.Height));
            Alpha2Label.Font = new Font(Alpha2Label.Font.FontFamily, Program.fontheight(smallfont, this.Height),FontStyle.Bold);
            Alpha3Label.Location = new Point(Program.xLocation(598, this.Width), Program.yLocation(869, this.Height));
            Alpha3Label.Font = new Font(Alpha3Label.Font.FontFamily, Program.fontheight(smallfont, this.Height), FontStyle.Bold);

            Bravo1Label.Location = new Point(Program.xLocation(1790, this.Width), Program.yLocation(480, this.Height));
            Bravo1Label.Font = new Font(Bravo1Label.Font.FontFamily, Program.fontheight(smallfont, this.Height), FontStyle.Bold);
            Bravo2Label.Location = new Point(Program.xLocation(1790, this.Width), Program.yLocation(671, this.Height));
            Bravo2Label.Font = new Font(Bravo2Label.Font.FontFamily, Program.fontheight(smallfont, this.Height), FontStyle.Bold);
            Bravo3Label.Location = new Point(Program.xLocation(1790, this.Width), Program.yLocation(869, this.Height));
            Bravo3Label.Font = new Font(Bravo3Label.Font.FontFamily, Program.fontheight(smallfont, this.Height), FontStyle.Bold);

            
            Dom1Alias.Location = new Point(Program.xLocation(1007, this.Width), Program.yLocation(600, this.Height));
            Dom1Alias.Font = new Font(Dom1Alias.Font.FontFamily, Program.fontheight(smallfont, this.Height), FontStyle.Bold);
            Dom2Alias.Location = new Point(Program.xLocation(1007, this.Width), Program.yLocation(771, this.Height));
            Dom2Alias.Font = new Font(Dom2Alias.Font.FontFamily, Program.fontheight(smallfont, this.Height), FontStyle.Bold);
            Dom3Alias.Location = new Point(Program.xLocation(1007, this.Width), Program.yLocation(944, this.Height));
            Dom3Alias.Font = new Font(Dom3Alias.Font.FontFamily, Program.fontheight(smallfont, this.Height), FontStyle.Bold);
            
            Box1Alpha.Location = new Point(Program.xLocation(581, this.Width), Program.yLocation(534, this.Height));
            Box1Alpha.Font = new Font(Box1Alpha.Font.FontFamily, Program.fontheight(largefont, this.Height));
            Box2Alpha.Location = new Point(Program.xLocation(581, this.Width), Program.yLocation(733, this.Height));
            Box2Alpha.Font = new Font(Box2Alpha.Font.FontFamily, Program.fontheight(largefont, this.Height));
            Box3Alpha.Location = new Point(Program.xLocation(581, this.Width), Program.yLocation(926, this.Height));
            Box3Alpha.Font = new Font(Box3Alpha.Font.FontFamily, Program.fontheight(largefont, this.Height));

            Box1Bravo.Location = new Point(Program.xLocation(1777, this.Width), Program.yLocation(534, this.Height));
            Box1Bravo.Font = new Font(Box1Bravo.Font.FontFamily, Program.fontheight(largefont, this.Height));
            Box2Bravo.Location = new Point(Program.xLocation(1777, this.Width), Program.yLocation(733, this.Height));
            Box2Bravo.Font = new Font(Box2Bravo.Font.FontFamily, Program.fontheight(largefont, this.Height));
            Box3Bravo.Location = new Point(Program.xLocation(1777, this.Width), Program.yLocation(926, this.Height));
            Box3Bravo.Font = new Font(Box3Bravo.Font.FontFamily, Program.fontheight(largefont, this.Height));

        }

        private void SetLeaderPosition() 
        {
            LeaderLabel.Location = new Point(Program.xLocation(1309, this.Width), Program.yLocation(1075, this.Height));
            LeaderLabel.Font = new Font(LeaderLabel.Font.FontFamily, Program.fontheight(48, this.Height));
        }

        private void SetProgressPosition() {
            
            Box1AlphaProgress.Location = new Point(Program.xLocation(1211, this.Width), Program.yLocation(600, this.Height));
            Box1AlphaProgress.Size = new Size(Program.xLocation(317, this.Width), Program.yLocation(48, this.Height));
            
            Box1BravoProgress.Location = new Point(Program.xLocation(1211, this.Width), Program.yLocation(654, this.Height));
            Box1BravoProgress.Size = new Size(Program.xLocation(317, this.Width), Program.yLocation(48, this.Height));

            Box2AlphaProgress.Location = new Point(Program.xLocation(1211, this.Width), Program.yLocation(771, this.Height));
            Box2AlphaProgress.Size = new Size(Program.xLocation(317, this.Width), Program.yLocation(48, this.Height));

            Box2BravoProgress.Location = new Point(Program.xLocation(1211, this.Width), Program.yLocation(825, this.Height));
            Box2BravoProgress.Size = new Size(Program.xLocation(317, this.Width), Program.yLocation(48, this.Height));

            Box3AlphaProgress.Location = new Point(Program.xLocation(1211, this.Width), Program.yLocation(949, this.Height));
            Box3AlphaProgress.Size = new Size(Program.xLocation(317, this.Width), Program.yLocation(48, this.Height));

            Box3BravoProgress.Location = new Point(Program.xLocation(1211, this.Width), Program.yLocation(1003, this.Height));
            Box3BravoProgress.Size = new Size(Program.xLocation(317, this.Width), Program.yLocation(48, this.Height));

        }

        private void SetPingPosition()
        {
            Box1Ping.Location = new Point(Program.xLocation(1120, this.Width), Program.yLocation(657, this.Height));
            Box1Ping.Size = new Size(Program.xLocation(35, this.Width), Program.yLocation(35, this.Height));

            Box2Ping.Location = new Point(Program.xLocation(1120, this.Width), Program.yLocation(829, this.Height));
            Box2Ping.Size = new Size(Program.xLocation(35, this.Width), Program.yLocation(35, this.Height));

            Box3Ping.Location = new Point(Program.xLocation(1120, this.Width), Program.yLocation(1003, this.Height));
            Box3Ping.Size = new Size(Program.xLocation(35, this.Width), Program.yLocation(35, this.Height));

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

  
}
