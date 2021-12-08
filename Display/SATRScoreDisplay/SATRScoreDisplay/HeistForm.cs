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
    public partial class HeistForm : SATRScoreDisplay.BaseScoreboard
    {
        const int smallfont = 48;
        const int standardfont = 72;
        int AlphaScore = 0;
        int BravoScore = 0;
        private OleDbConnection connection = new OleDbConnection();
        public HeistForm()
        {
            InitializeComponent();
        }

        protected override void ResizeScreen()
        {
            SetMoneyPosition();
            SetBattlePosition();
            SetProgressPosition();
            SetPingPosition();
        }

        private void HeistForm_Load(object sender, EventArgs e)
        {
            ResizeScreen();
            BattleLabel.Text = Convert.ToString(Program.ScoreboardBattleCode + 1);
            SetTimeLeftVisibility(true);
            CheckVaults();
            CheckTestMode();
            LoadScores();
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

        private void CheckVaults() //CHECK THAT EACH TEAM HAS A VAULT
        {
            OleDbCommand command = new OleDbCommand();
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 27 AND Team_Code = 1 AND SynchronisedDevice.Enabled = True";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
                if (reader.GetInt32(0) == 0)
                    MessageBox.Show("No Alpha Vaults Synchronized.");
                else if (reader.GetInt32(0) > 1)
                    MessageBox.Show("Too Many Alpha Vaults Synchronized. "+  Convert.ToString(reader.GetInt32(0)));
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 27 AND Team_Code = 2 AND SynchronisedDevice.Enabled = True";
            reader.Close();
            reader = command.ExecuteReader();
            if (reader.Read())
                if (reader.GetInt32(0) == 0)
                    MessageBox.Show("No Bravo Vaults Synchronized.");
                else if (reader.GetInt32(0) > 1)
                    MessageBox.Show("Too Many Bravo Vaults Synchronized. " + Convert.ToString(reader.GetInt32(0)));


            reader.Close(); 
            connection.Close();
        }
        private void SetMoneyPosition()
        {
            AlphaBalanceLabel.Location = new Point(Program.xLocation(379, this.Width), Program.yLocation(926, this.Height));
            AlphaBalanceLabel.Font = new Font(AlphaBalanceLabel.Font.FontFamily, Program.fontheight(smallfont, this.Height));
            AlphaBalanceLabel.Size = new Size(Program.xLocation(228, this.Width), Program.yLocation(77, this.Height));

            BravoBalanceLabel.Location = new Point(Program.xLocation(1980, this.Width), Program.yLocation(926, this.Height));
            BravoBalanceLabel.Font = new Font(BravoBalanceLabel.Font.FontFamily, Program.fontheight(smallfont, this.Height));
            BravoBalanceLabel.Size = new Size(Program.xLocation(228, this.Width), Program.yLocation(77, this.Height));
        }
        private void SetBattlePosition()
        {
            BattleLabel.Location = new Point(Program.xLocation(1229, this.Width), Program.yLocation(687, this.Height));
            BattleLabel.Font = new Font(AlphaBalanceLabel.Font.FontFamily, Program.fontheight(standardfont, this.Height));
        }

        private void SetPingPosition()
        {
            AlphaPing.Size = new Size(Program.xLocation(35, this.Width), Program.yLocation(35, this.Height));
            BravoPing.Size = new Size(Program.xLocation(35, this.Width), Program.yLocation(35, this.Height));

            AlphaPing.Location = new Point(Program.xLocation(72, this.Width), Program.yLocation(1114, this.Height));
            BravoPing.Location = new Point(Program.xLocation(2442, this.Width), Program.yLocation(1114, this.Height));
        }
        private void SetProgressPosition()
        {

            AlphaProgress.Location = new Point(Program.xLocation(996, this.Width), Program.yLocation(892, this.Height));
            BravoProgress.Location = new Point(Program.xLocation(996, this.Width), Program.yLocation(993, this.Height));
            AlphaProgress.Size = new Size(Program.xLocation(563, this.Width), Program.yLocation(95, this.Height));
            BravoProgress.Size = new Size(Program.xLocation(563, this.Width), Program.yLocation(95, this.Height));
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

        protected override void RefrehGridTimer_Tick(object sender, EventArgs e)
        {

            int[] Vault = new int[3];
            int[] TeamCode = new int[3];
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

                //SET ALL DEVICES TO UNPINGED.
                command.CommandText = "UPDATE SynchronisedDevice SET Ping = false  WHERE Device_Role_Code = 27 AND SynchronisedDevice.Enabled = True; ";
                command.ExecuteNonQuery();

                //PING EACH DEVICE
                command.CommandText = "SELECT SATR_Unit_ID, Team_Code FROM SynchronisedDevice WHERE Device_Role_Code = 27 AND SynchronisedDevice.Enabled = True ";//Find all the Vaults
                OleDbDataReader reader = command.ExecuteReader();

                try
                {
                    int i = 0;
                    while (reader.Read())// gets all the vaults
                    {
                        if (reader.GetInt32(1) < 3 && reader.GetInt32(1) > 0)
                        {
                            Vault[i] = reader.GetInt32(0);
                            TeamCode[i] = reader.GetInt32(1);
                            i++;
                        }

                    }
                    
                    reader.Close();
                    connection.Close();

                    //Monitor Details
                    for (int j = 0; j < i; j++)
                    {
                        Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, Vault[j], 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                        SynchInProgress = true;
                        EnableWaitTimer();


                        while (SynchInProgress) { Application.DoEvents(); }//wait for the timer to complete 200ms for a response before sending next request

                        if (TestResponseChk.Checked)//DELETE IN PRODUCTION
                        {

                            TestDeviceResponse(Vault[j], TeamCode[j]);
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

        private void TestDeviceResponse(int SATRID, int TeamCode)
        {

            String ALIAS = "VAULT" + Convert.ToString(TeamCode);
            int TeamScore = Program.RandomNumber(0, 1000);
            Program.ReceiveRadioPacket(25, Program.ScoreboardBattleCode, SATRID, Program.ScoreboardID, 27,TeamCode, TeamScore, 0, 0, 0, 0, 0, 0, ALIAS);

        }
        private void LoadScores()
        {

            string directoryName = Program.rootdirectory;
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            //GET PING STATUS

            command.CommandText = "SELECT Ping, SATR_Unit_ID, Team_Code, Score FROM SynchronisedDevice WHERE Device_Role_Code = 27 AND SynchronisedDevice.Enabled = True ORDER BY TEAM_CODE";//Find all the Hiest Boxes
            OleDbDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                bool Ping = reader.GetBoolean(0);
                int SATRID = reader.GetInt32(1);
                int TeamCode = reader.GetInt32(2);
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
                    case 1: AlphaPing.BackgroundImage = Image.FromFile(ImageFileName); AlphaScore = reader.GetInt32(3); break;
                    case 2: BravoPing.BackgroundImage = Image.FromFile(ImageFileName); BravoScore = reader.GetInt32(3); break;
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
            AlphaBalanceLabel.Text = Convert.ToString(AlphaScore);
            BravoBalanceLabel.Text = Convert.ToString(BravoScore);

        }
    }
}
