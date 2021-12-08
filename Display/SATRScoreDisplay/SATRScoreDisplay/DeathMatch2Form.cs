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
    public partial class DeathMatch2Form : SATRScoreDisplay.BaseScoreboard
    {
        const int standardfont = 72;
       
        private OleDbConnection connection = new OleDbConnection();
        int AlphaScore = 0;
        int BravoScore = 0;
        bool GameOver = false;

        public DeathMatch2Form()
        {
            InitializeComponent();
            SetTimeLeftVisibility(true);
        }

        private void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
            }
        }

        protected override void ResizeScreen()
        {
            SetSpawnPosition();
            SetBattlePosition();
            SetProgressPosition();
            SetPingPosition();
        }

        private void DeathMatch2Form_Load(object sender, EventArgs e)
        {
            ResizeScreen();
            CheckMedicBoxes();
            BattleLabel.Text = Convert.ToString(Program.ScoreboardBattleCode + 1);
            REfreshResults(false);
            NewMission();
            CheckTestMode();
            String ImageFileName = Program.rootdirectory + @"\Images\" + Program.GenreFolder + @"\NoPing.png";
            if (!File.Exists(ImageFileName))//
                ImageFileName = Program.rootdirectory + @"\Images\NoPing.png";
             AlphaPing.BackgroundImage = Image.FromFile(ImageFileName);
             BravoPing.BackgroundImage = Image.FromFile(ImageFileName);
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

        private void CheckMedicBoxes()
        {
            OleDbCommand command = new OleDbCommand();
            OpenConnection();
          
            command.Connection = connection;
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Team_Code = 1 AND SynchronisedDevice.Enabled = True AND (Device_Role_Code = 4 OR Device_Role_Code = 10); ";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
                if (!(reader.GetInt32(0) == 1))
                    MessageBox.Show("Wrong number of Alpha Medic Boxes " + reader.GetInt32(0).ToString());
            reader.Close();
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Team_Code = 2 AND SynchronisedDevice.Enabled = True AND (Device_Role_Code = 4 OR Device_Role_Code = 10); ";
            reader = command.ExecuteReader();
            if (reader.Read())
                if (!(reader.GetInt32(0) == 1))
                    MessageBox.Show("Wrong number of Bravo Medic Boxes " + reader.GetInt32(0).ToString());
            reader.Close();
            connection.Close();
        }

        protected override void NewMission()
        {
            GameOver = false; //A new game has started
            if (!SynchInProgress)
            {
                connection.Close();

                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                //UPDATE THE TERMS
                command.CommandText = "SELECT Genre.Respawn_Term FROM Config INNER JOIN Genre ON Config.Genre_Code = Genre.Genre_Code;";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    AlphaLabel.Text = reader[0].ToString();
                    BravoLabel.Text = AlphaLabel.Text;
                }
                reader.Close();
                connection.Close();
            }
        }

        private void REfreshResults(bool FinalScore)
        {
            SetWaitTime(ResponseTime());
          //  TraceBox.Text = "";
           // TraceBox.AppendText("REFRESH\n");
             if (Program.TimeLeftInGame.CompareTo(TimeSpan.Zero) > 0 && GameOver && !FinalScore)
            {
                GameOver = false; //A new game has started
                
            }
            if (!GameOver || FinalScore)
            {

                const int maxdevices = 2047;
                int[] DeviceList = new int[maxdevices];
                int[] TeamCode = new int[maxdevices];

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
                    TraceBox.AppendText("Clear Pings\n");
                    //SET ALL DEVICES TO UNPINGED.
                    command.CommandText = "UPDATE SynchronisedDevice SET Ping = false  WHERE Device_Role_Code = 4 OR Device_Role_Code = 10; ";
                    command.ExecuteNonQuery();

                    //PING EACH DEVICE
                    command.CommandText = "SELECT SATR_Unit_ID, TEAM_CODE FROM SynchronisedDevice WHERE (Device_Role_Code = 4 OR Device_Role_Code = 10) AND SynchronisedDevice.Enabled = True";//Find all the Medic Boxes
                    OleDbDataReader reader = command.ExecuteReader();

                    try
                    {
                        int SATRID = 0;
                        int i = 0;
                        while (reader.Read())
                        {
                            DeviceList[i] = reader.GetInt32(0);
                            TeamCode[i] = reader.GetInt32(1);
                            i++;
                        }
                        reader.Close();
                        connection.Close();

                        for (int j = 0; j<i; j++)
                        {
                            if (CloseApp) break;
                            SATRID = DeviceList[j];
                            TraceBox.AppendText("PING "+Convert.ToString(SATRID)+"\n");

                            reader.Close();
                            connection.Close();

                            //Monitor Details
                            Program.rf.RFResponse = "No Response";
                            Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                            SynchInProgress = true;
                            EnableWaitTimer();

                            while (SynchInProgress) { Application.DoEvents(); if (CloseApp) { DisableWaitTimer(); } }//wait for the timer to complete 200ms for a response before sending next request
                            TraceBox.AppendText(Program.rf.RFResponse + "\n");
                            if (TestResponseChk.Checked)//DELETE IN PRODUCTION
                            {

                                TestDeviceResponse(SATRID, TeamCode[j]);
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
                        connection.Close();
                        Cursor.Current = Cursors.Default;
                    }
                    TraceBox.AppendText("LOAD SCORES\n");
                    LoadScores();

                }
            }
        }
        protected override void RefrehGridTimer_Tick(object sender, EventArgs e) //GET THE DETAILS OF EACH SYNCHRONISED MEDIC BOX
        {
            REfreshResults(false);
        }

        private void PingAllGamers()
        {
            TraceBox.AppendText("PING GAMERS\n");
            const int maxdevices = 2017;
            int[] DeviceList = new int[maxdevices];
            int i = 0;
            int GamersCount = 0;
            OpenConnection();


            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 1  AND SynchronisedDevice.Enabled = True ";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
                GamersCount = reader.GetInt32(0);

            reader.Close();
            TraceBox.AppendText("GAMERS " + Convert.ToString(GamersCount) + "\n");


            //SET ALL DEVICES TO UNPINGED.
            command.CommandText = "UPDATE SynchronisedDevice SET Ping = false; ";
            command.ExecuteNonQuery();

            //PING EACH DEVICE
            command.CommandText = "SELECT SATR_Unit_ID FROM SynchronisedDevice WHERE Device_Role_Code = 1  AND SynchronisedDevice.Enabled = True";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                DeviceList[i] = reader.GetInt32(0);
                i++;
            }
            reader.Close();
            connection.Close();

            for (int j = 0; j < i; j++)
            {
                // if (GamersCount > 0)
                //     progressBar1.Value = (int)(j * 100 / GamersCount);
                int SATRID = DeviceList[j];
                TraceBox.AppendText("PING " + Convert.ToString(SATRID) + "\n");
                //Monitor Details

                Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                SynchInProgress = true;
                EnableWaitTimer();

                while (SynchInProgress) { Application.DoEvents(); }//wait for the timer to complete 200ms for a response before sending next request


                if (CloseApp)
                {
                    this.Close();
                    break;
                }
            }


        }
        protected override void TriggerResultDisplay()
        {

    

            SynchInProgress = true;
            ShowSyncPicture();
            GameOver = true;//Disable Scoring based on respawns
            Cursor.Current = Cursors.WaitCursor;
            REfreshResults(true);//Get the final spawn counts
            int OldAlphaScore = BravoScore;
            int OldBravoScore = AlphaScore;
            BravoScore = 0;
            AlphaScore = 0;
            DisplayScore();


            connection.Close();
            OpenConnection();
      
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

        /*    //Count Gamers
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True;";
            OleDbDataReader reader = command.ExecuteReader();
            int GamerCount = 50;
            if (reader.Read())
            {
                GamerCount = reader.GetInt32(0);
            }
            reader.Close();
        */

            //UPDATE THE TERMS
            command.CommandText = "SELECT Genre.Kill_Term FROM Config INNER JOIN Genre ON Config.Genre_Code = Genre.Genre_Code;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                AlphaLabel.Text = reader[0].ToString();
                BravoLabel.Text = AlphaLabel.Text;
            }
            reader.Close();

            int OldSyncTimeMs = GetWaitInterval();
            int AfterSyncTime = 10;
            command.CommandText = "SELECT After_Mission_Sync_Time FROM Config";
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                AfterSyncTime = reader.GetInt32(0);
            }
            reader.Close();
            connection.Close();
          
            PingAllGamers();

            OpenConnection();
            command = new OleDbCommand();
            command.Connection = connection;

            int RecCount = 0;
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND Team_Code = 1 AND SynchronisedDevice.Enabled = True;";//Check there are records to count
            reader = command.ExecuteReader();
            if (reader.Read())
                RecCount = reader.GetInt32(0);
            else
                RecCount = 0;
            TraceBox.AppendText("Alphas " + Convert.ToString(RecCount) + "\n");
            reader.Close();
            if (RecCount == 0)
            {
                AlphaScore = 0;
            }
            else
            {
                command.CommandText = "SELECT Round(SUM(Kills),0) as KillCount FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND Team_Code = 1 AND SynchronisedDevice.Enabled = True;";//GET ALPHA TEAM SCORE TOTAL
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    try
                    {
                         AlphaScore = Convert.ToInt32(Math.Round(reader.GetDouble(0)));
                        TraceBox.AppendText("Alpha Kills " + Convert.ToString(AlphaScore) + "\n");
                        //AlphaScore = reader.GetInt32(0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex);
                        Application.Exit();
                    }
                }
            }
            reader.Close();
            RecCount = 0;
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND Team_Code = 2 AND SynchronisedDevice.Enabled = True ;";//Check there are records to count
            reader = command.ExecuteReader();
            if (reader.Read())
                RecCount = reader.GetInt32(0);
            else
                RecCount = 0;
            TraceBox.AppendText("Bravos " + Convert.ToString(RecCount) + "\n");
            reader.Close();
            if (RecCount == 0)
            {
                BravoScore = 0;
            }
            else
            {

                command.CommandText = "SELECT Round(SUM(Kills),0) as KillCount FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND Team_Code = 2  AND SynchronisedDevice.Enabled = True ";//GET BRAVO TEAM SCORE TOTAL
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                   BravoScore = Convert.ToInt32(Math.Round(reader.GetDouble(0)));
                    TraceBox.AppendText("Bravo Kills " + Convert.ToString(BravoScore) + "\n");
                    //   BravoScore = reader.GetInt32(0);
                }
                reader.Close();
            }
            connection.Close();

            if (AlphaScore < OldAlphaScore)// IN CASE OPPONENTS SPAWN COUNT WAS LARGER THAN KILL COUNT RETRIEVED
                AlphaScore = OldAlphaScore;

            if (BravoScore < OldBravoScore)
                BravoScore = OldBravoScore;

            if (AlphaScore > 0 && BravoScore > 0)
            {
                int TotalScore = AlphaScore + BravoScore;
                int AlphaPercentage = (int)AlphaScore * 100 / TotalScore;
                int BravoPercentage = (int)BravoScore * 100 / TotalScore;
                AlphaProgress.Value = AlphaPercentage; //Switched back because kills counts are the main score
                BravoProgress.Value = BravoPercentage;

            }
            DisplayScore();
           
            if (AlphaScore > BravoScore)
                TeamResult(1);
            else
          if (BravoScore > AlphaScore)
                TeamResult(2);
            else
                TeamResult(0);
            SetWaitTime(OldSyncTimeMs);
            HideSyncPicture();
            Cursor.Current = Cursors.Default;
        }

        private void TestDeviceResponse(int SATRID, int TeamCode)
        {
     
            String StatusBar = "";
            int TeamScore = Program.RandomNumber(0, 200);
            Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode,SATRID, Program.ScoreboardID, (Int16)TeamCode, 0, 0, 0, TeamScore, 0, 0, 0,4, StatusBar);

        }

            private void LoadScores()
        {
            
            string directoryName = Program.rootdirectory;
            int TeamCode = 0;
            OpenConnection();
          
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            //GET PING STATUS
            int SATRID = 0;
            command.CommandText = "SELECT Ping, SATR_Unit_ID, Team_Code, Score FROM SynchronisedDevice WHERE Device_Role_Code = 4 OR Device_Role_Code = 10 ";//GET ALL THE MEDIC AND COMBINATION BOXES 
            OleDbDataReader reader = command.ExecuteReader();
            Boolean Ping = false;
            AlphaScore = 0;
            BravoScore = 0;

            while (reader.Read()) //FOR SYNCHRONISED MEDIC BOX
            {
                Ping = reader.GetBoolean(0);
                SATRID = reader.GetInt32(1);
                TeamCode = reader.GetInt32(2);

                string pingfile;
                if (TeamCode == 1)
                {
                    pingfile = "RedPing.png";
                    AlphaScore += reader.GetInt32(3);
                }
                else if (TeamCode == 2)
                {
                    pingfile = "BluePing.png";
                    BravoScore += reader.GetInt32(3);
                }
                else
                    pingfile = "NoPing.png";
                if (!Ping)
                    pingfile = "NoPing.png";

                String ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + pingfile;
                if (!File.Exists(ImageFileName))//
                    ImageFileName = directoryName + @"\Images\" + pingfile;
                if (TeamCode == 1)
                    AlphaPing.BackgroundImage = Image.FromFile(ImageFileName);
                else if (TeamCode == 2)
                    BravoPing.BackgroundImage = Image.FromFile(ImageFileName);

            }
            reader.Close();

            if (AlphaScore > 0 && BravoScore > 0)
            {
                int TotalScore = AlphaScore + BravoScore;
                int AlphaPercentage = (int)AlphaScore * 100 / TotalScore;
                int BravoPercentage = (int)BravoScore * 100 / TotalScore;
                AlphaProgress.Value = BravoPercentage; //Switched because low spawn count is better.
                BravoProgress.Value = AlphaPercentage;

            }
            else
            {
                AlphaProgress.Value = 0;
                BravoProgress.Value = 0;
                if (AlphaScore > 0)
                    BravoProgress.Value = 100;
                else
                if (BravoScore > 0)
                    AlphaProgress.Value = 100;
            }
            connection.Close();
            DisplayScore();


        }


        void TestGeneratGamersWithKills(int Gamers)
        {
            for (int i=1; i<=Gamers;i++)
            {
                int TeamCode = 0;
                if (Program.RandomNumber(1, 100) < 51)
                    TeamCode = 1;
                else
                    TeamCode = 2;
                Program.ReceiveRadioPacket(25, Program.ScoreboardBattleCode, Program.RandomNumber(1, 2047), Program.ScoreboardID, 1, TeamCode, Program.RandomNumber(0, 20), 0, 0, 0, 0, 0, 0, "GAMER"+Convert.ToString(i));
            }
        }
        private void DisplayScore()
        {
            AlphaCountLabel.Text = Convert.ToString(AlphaScore);
            BravoCountLabel.Text = Convert.ToString(BravoScore);

        }
        private void SetSpawnPosition()
        {
            
            int x = (int)(537 * this.Width / Program.ScreenWidth);
            int y = (int)(896 * this.Height / Program.ScreenHeight);
            //537, 876
            AlphaCountLabel.Location = new Point(x, y);
            int fontheight = (int)(36 * this.Height / Program.ScreenHeight);

            AlphaCountLabel.Font = new Font(AlphaCountLabel.Font.FontFamily,Program.yLocation(48, this.Height), FontStyle.Bold);
            //2141, 876
            x = (int)(2141 * this.Width / Program.ScreenWidth);
            y = (int)(896 * this.Height / Program.ScreenHeight);

            BravoCountLabel.Location = new Point(x, y);
            BravoCountLabel.Font = new Font(BravoCountLabel.Font.FontFamily, Program.yLocation(48, this.Height));

            AlphaLabel.Location = new Point(Program.xLocation(179, this.Width), Program.yLocation(860, this.Height));
            BravoLabel.Location = new Point(Program.xLocation(1782, this.Width), Program.yLocation(860, this.Height));
            AlphaLabel.Size = new Size(Program.xLocation(336, this.Width), Program.yLocation(77, this.Height));
            BravoLabel.Size = new Size(Program.xLocation(336, this.Width), Program.yLocation(77, this.Height));
            AlphaLabel.Font = new Font(AlphaLabel.Font.FontFamily, Program.yLocation(36, this.Height), FontStyle.Bold);
            Count1Label.Font = new Font(Count1Label.Font.FontFamily, Program.yLocation(36, this.Height), FontStyle.Bold);
            BravoLabel.Font = new Font(BravoLabel.Font.FontFamily, Program.yLocation(36, this.Height), FontStyle.Bold);
            Count2Label.Font = new Font(Count2Label.Font.FontFamily, Program.yLocation(36, this.Height), FontStyle.Bold);

            Count1Label.Size = new Size(Program.xLocation(327, this.Width), Program.yLocation(53, this.Height)); //327, 53
            Count2Label.Size = new Size(Program.xLocation(327, this.Width), Program.yLocation(53, this.Height));

            
            fontheight = (int)(48 * this.Height / Program.ScreenHeight);
            //188, 945
            Count1Label.Location = new Point(Program.xLocation(188, this.Width), Program.yLocation(945, this.Height));
            
            
            Count2Label.Location = new Point(Program.xLocation(1792, this.Width), Program.yLocation(945, this.Height));

            AlphaLabel.BringToFront();
            BravoLabel.BringToFront();



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
            AlphaPing.Size = new Size(x, y);

            //2445, 1114
            x = (int)(2445 * this.Width / Program.ScreenWidth);
            y = (int)(1114 * this.Height / Program.ScreenHeight);
            BravoPing.Location = new Point(x, y);

            //74, 1114

            x = (int)(74 * this.Width / Program.ScreenWidth);
            y = (int)(1114 * this.Height / Program.ScreenHeight);
            AlphaPing.Location = new Point(x, y);
        }

        private void TraceBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
