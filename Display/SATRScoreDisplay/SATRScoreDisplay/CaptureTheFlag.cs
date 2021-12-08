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
    public partial class CaptureTheFlagForm : SATRScoreDisplay.BaseScoreboard
    {
        private OleDbConnection connection = new OleDbConnection();
        int AlphaScore = 0;
        int BravoScore = 0;

        public CaptureTheFlagForm()
        {
            InitializeComponent();
        }

        protected override void ResizeScreen()
        {
            SetFlagPosition();
            SetBattlePosition();
            SetProgressPosition();
            SetPingPosition();
        }
        private void CaptureTheFlagForm_Load(object sender, EventArgs e)
        {
            ResizeScreen();
            BattleLabel.Text = Convert.ToString(Program.ScoreboardBattleCode + 1);
            SetTimeLeftVisibility(true);
            CheckFlagBoxes();
            LoadPing(false, 0, 1);  //Set the Ping graphics
            LoadPing(false, 0, 2);
            CheckTestMode();
            REfreshResults();

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
        private void REfreshResults()
        {
            int[] FlagBox = new int[3];
        int[] TeamCode = new int[3];
            if (!SynchInProgress)

                Cursor.Current = Cursors.WaitCursor;
            //Update The screen from the database.
            
            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
        command.Connection = connection;
                

                //SET ALL DEVICES TO UNPINGED.
                command.CommandText = "UPDATE SynchronisedDevice SET Ping = false  WHERE Device_Role_Code = 16; ";
                command.ExecuteNonQuery();

                //PING EACH DEVICE
                command.CommandText = "SELECT SATR_Unit_ID, Team_Code FROM SynchronisedDevice WHERE Device_Role_Code = 16 AND SynchronisedDevice.Enabled = True";//Find all the Flags
                OleDbDataReader reader = command.ExecuteReader();

                try
                {
                    int i = 0;
                    while (reader.Read())// gets all the vaults
                    {
                        if (reader.GetInt32(1) < 3 && reader.GetInt32(1) > 0)
                        {
                            FlagBox[i] = reader.GetInt32(0);
                            TeamCode[i] = reader.GetInt32(1);
                            i++;
                        }

}

reader.Close();
                    connection.Close();

                    //Monitor Details
                    for (int j = 0; j<i; j++)
                    {
                        if (CloseApp) break;
                        Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, FlagBox[j], 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                        SynchInProgress = true;
                        EnableWaitTimer();


                        while (SynchInProgress) { Application.DoEvents(); if (CloseApp) { DisableWaitTimer(); } }//wait for the timer to complete 200ms for a response before sending next request

                        if (TestResponseChk.Checked)//DELETE IN PRODUCTION
                        {

                            TestDeviceResponse(FlagBox[j], TeamCode[j]);
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
                LoadScores();


            }
}
        protected override void RefrehGridTimer_Tick(object sender, EventArgs e)
        {

            REfreshResults();
        }

        private void TestDeviceResponse(int SATRID, int TeamCode)
        {

            String ALIAS = "FLAGBOX"+Convert.ToString(TeamCode);
            int TeamScore = Program.RandomNumber(0, 40);
            Program.ReceiveRadioPacket(25, Program.ScoreboardBattleCode, SATRID, Program.ScoreboardID, 16, TeamCode, TeamScore, 0, 0, 0, 0, 0, 0, ALIAS);

        }

        private void LoadPing(bool Ping, int SATRID, int TeamCode)
        {
            string directoryName = Program.rootdirectory;
            string pingfile;
            if (Ping && SATRID > 0)
            {
                if (TeamCode == 1)
                    pingfile = "RedPing.png";
                else
                    pingfile = "BluePing.png";
            }
            else
                pingfile = "NoPing.png";
            String ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + pingfile;
            if (!File.Exists(ImageFileName))//
                ImageFileName = directoryName + @"\Images\" + pingfile;
            switch (TeamCode)
            {
                case 1: AlphaPing.BackgroundImage = Image.FromFile(ImageFileName); break;
                case 2: BravoPing.BackgroundImage = Image.FromFile(ImageFileName); break;
            }
        }
        private void LoadScores()
        {


            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            //GET PING STATUS

            command.CommandText = "SELECT Ping, SATR_Unit_ID, Team_Code, Score FROM SynchronisedDevice WHERE Device_Role_Code = 16 AND SynchronisedDevice.Enabled = True  ORDER BY TEAM_CODE";//Find all the Flag Boxes
            OleDbDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                bool Ping = reader.GetBoolean(0);
                int SATRID = reader.GetInt32(1);
                int TeamCode = reader.GetInt32(2);
                LoadPing(Ping, SATRID, TeamCode);
                switch (TeamCode)
                {
                    case 1: AlphaScore = reader.GetInt32(3); break;
                    case 2: BravoScore = reader.GetInt32(3); break;
                }

            }
            reader.Close();
            connection.Close();
            int AlphaPercentage = 0;
            int BravoPercentage = 0;

            if (AlphaScore > 0 || BravoScore > 0)
            {
                int TotalScore = AlphaScore + BravoScore;
                AlphaPercentage = (int)AlphaScore * 100 / TotalScore;
                BravoPercentage = (int)BravoScore * 100 / TotalScore;
            }

            AlphaProgress.Value = AlphaPercentage;
            BravoProgress.Value = BravoPercentage;
            AlphaFlagsLabel.Text = Convert.ToString(AlphaScore);
            BravoFlagsLabel.Text = Convert.ToString(BravoScore);

        }


        private void CheckFlagBoxes()
        {
            OleDbCommand command = new OleDbCommand();
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE SynchronisedDevice.Enabled = True AND Team_Code = 1 AND (Device_Role_Code = 16); ";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
                if (!(reader.GetInt32(0) == 1))
                    MessageBox.Show("Wrong number of Alpha Flag Boxes " + reader.GetInt32(0).ToString());
            reader.Close();
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE SynchronisedDevice.Enabled = True AND Team_Code = 2 AND (Device_Role_Code = 16); ";
            reader = command.ExecuteReader();
            if (reader.Read())
                if (!(reader.GetInt32(0) == 1))
                    MessageBox.Show("Wrong number of Bravo Flag Boxes " + reader.GetInt32(0).ToString());


            reader.Close();
            connection.Close();
        }
        private void SetFlagPosition()
        {//509, 877
            AlphaFlagsLabel.Location = new Point(Program.xLocation(509, this.Width), Program.yLocation(877, this.Height));
            AlphaFlagsLabel.Font = new Font(AlphaFlagsLabel.Font.FontFamily, Program.fontheight(72, this.Height),FontStyle.Bold);
            AlphaFlagsLabel.Size = new Size(Program.xLocation(228, this.Width), Program.yLocation(77, this.Height));
            //2122, 877
            BravoFlagsLabel.Location = new Point(Program.xLocation(2117, this.Width), Program.yLocation(877, this.Height));
            BravoFlagsLabel.Font = new Font(BravoFlagsLabel.Font.FontFamily, Program.fontheight(72, this.Height), FontStyle.Bold);
            BravoFlagsLabel.Size = new Size(Program.xLocation(228, this.Width), Program.yLocation(77, this.Height));
        }

        private void SetBattlePosition()
        {
            BattleLabel.Location = new Point(Program.xLocation(1229, this.Width), Program.yLocation(683, this.Height));
            BattleLabel.Font = new Font(BattleLabel.Font.FontFamily, Program.fontheight(72, this.Height));
        }
        private void SetProgressPosition()
        {

            AlphaProgress.Location = new Point(Program.xLocation(996, this.Width), Program.yLocation(892, this.Height));
            BravoProgress.Location = new Point(Program.xLocation(996, this.Width), Program.yLocation(993, this.Height));
            AlphaProgress.Size = new Size(Program.xLocation(563, this.Width), Program.yLocation(95, this.Height));
            BravoProgress.Size = new Size(Program.xLocation(563, this.Width), Program.yLocation(95, this.Height));
        }

        private void SetPingPosition()
        {
            AlphaPing.Size = new Size(Program.xLocation(35, this.Width), Program.yLocation(35, this.Height));
            BravoPing.Size = new Size(Program.xLocation(35, this.Width), Program.yLocation(35, this.Height));

            AlphaPing.Location = new Point(Program.xLocation(78, this.Width), Program.yLocation(1114, this.Height));
            BravoPing.Location = new Point(Program.xLocation(2448, this.Width), Program.yLocation(1114, this.Height));
        }

    }
}
