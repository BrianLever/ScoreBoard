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
    public partial class RushForm : SATRScoreDisplay.BaseScoreboard
    {
        private OleDbConnection connection = new OleDbConnection();

        private int AttackersTeamCode = 1;//TEST ONLY
        private int DefendersTeamCode = 2;//TEST ONLY

        public RushForm()
        {
            InitializeComponent();
        }

        private void RushForm_Load(object sender, EventArgs e)
        {
            BattleLabel.Text = Convert.ToString(Program.ScoreboardBattleCode + 1);
            SetTimeLeftVisibility(true);
            ResizeScreen();
            CheckRushBoxes();
            LoadScores();
            TestSetAttackersDefendersTeam();//TEST
            CheckTestMode();
        }

        protected override void ResizeScreen()
        {
            PositionPanels();
            SetBattlePosition();
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

        private void CheckRushBoxes()
        {
            OleDbCommand command = new OleDbCommand();
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 25 AND SynchronisedDevice.Enabled = True";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
                if (reader.GetInt32(0) < 1)
                    MessageBox.Show("Insufficient Rush Synchronized.");
                else if (reader.GetInt32(0) > 5)
                    MessageBox.Show("Too Many Rush Boxes Synchronized.");


            reader.Close();
            connection.Close();
        }


        private void SetBattlePosition()
        {
            BattleLabel.Location = new Point(Program.xLocation(1232, this.Width), Program.yLocation(676, this.Height));
            BattleLabel.Font = new Font(BattleLabel.Font.FontFamily, Program.fontheight(72, this.Height), FontStyle.Bold);

        }

        private void PositionPanels()
        {
            SetAPanel(AlphaBox1, 416, 523, Alpha1Alias, Alpha1Ping, AlphaPingLabel1);
            SetAPanel(AlphaBox2, 416, 621, Alpha2Alias, Alpha2Ping, AlphaPingLabel2);
            SetAPanel(AlphaBox3, 416, 719, Alpha3Alias, Alpha3Ping, AlphaPingLabel3);
            SetAPanel(AlphaBox4, 416, 817, Alpha4Alias, Alpha4Ping, AlphaPingLabel4);
            SetAPanel(AlphaBox5, 416, 915, Alpha5Alias, Alpha5Ping, AlphaPingLabel5);

            SetAPanel(BravoBox1, 1700, 523, Bravo1Alias, Bravo1Ping, BravoPingLabel1);
            SetAPanel(BravoBox2, 1700, 621, Bravo2Alias, Bravo2Ping, BravoPingLabel2);
            SetAPanel(BravoBox3, 1700, 719, Bravo3Alias, Bravo3Ping, BravoPingLabel3);
            SetAPanel(BravoBox4, 1700, 817, Bravo4Alias, Bravo4Ping, BravoPingLabel4);
            SetAPanel(BravoBox5, 1700, 915, Bravo5Alias, Bravo5Ping, BravoPingLabel5);
            HideAllPanels();


        }

        private void HideAllPanels()
        {
            AlphaBox1.Visible = false;
            AlphaBox2.Visible = false;
            AlphaBox3.Visible = false;
            AlphaBox4.Visible = false;
            AlphaBox5.Visible = false;

            BravoBox1.Visible = false;
            BravoBox2.Visible = false;
            BravoBox3.Visible = false;
            BravoBox4.Visible = false;
            BravoBox5.Visible = false;
        }


        private void SetAPanel(Control aPanel, int x, int y, Control aAlias, Control aPing, Control alabel)
        {
            aPanel.Location = new Point(Program.xLocation(x, this.Width), Program.yLocation(y, this.Height));
            aPanel.Size = new Size(Program.xLocation(445, this.Width), Program.yLocation(88, this.Height));

            aAlias.Font = new Font(aAlias.Font.FontFamily, Program.fontheight(36, this.Height), FontStyle.Bold);
            aAlias.Location = new Point(Program.xLocation(10, this.Width), Program.yLocation(18, this.Height));
            aPing.Location = new Point(Program.xLocation(329, this.Width), Program.yLocation(30, this.Height));
            aPing.Size = new Size(Program.xLocation(35, this.Width), Program.yLocation(35, this.Height));
            alabel.Location = new Point(Program.xLocation(361, this.Width), Program.yLocation(28, this.Height));
            alabel.Font = new Font(alabel.Font.FontFamily, Program.fontheight(24, this.Height), FontStyle.Bold);

        }
        private void ABoxSettings(Control aPanel, Control aAlias, Control aPing, string Alias, bool Ping)
        {
            aPanel.Visible = true;
            aAlias.Text = Alias;
            string pingfile;
            if (Ping)
                pingfile = "RedPing.png";
            else
                pingfile = "NoPing.png";
            string directoryName = Program.rootdirectory;
            String ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + pingfile;
            if (!File.Exists(ImageFileName))//
                ImageFileName = directoryName + @"\Images\" + pingfile;
            aPing.BackgroundImage = Image.FromFile(ImageFileName);
        }

        protected override void NewMission()
        {
            HideAllPanels();
        }

        private void HideAlternativeBox(string Alias, int TeamCode)
        {
            for (int i = 1; i < 6; i++)
            {
                if (TeamCode == 1)//Alpha team has the box, so hide the bravo team box
                {
                    if (Bravo1Alias.Text == Alias) BravoBox1.Visible = false;
                    if (Bravo2Alias.Text == Alias) BravoBox2.Visible = false;
                    if (Bravo3Alias.Text == Alias) BravoBox3.Visible = false;
                    if (Bravo4Alias.Text == Alias) BravoBox4.Visible = false;
                    if (Bravo5Alias.Text == Alias) BravoBox5.Visible = false;
                }
                if (TeamCode == 2)//Bravo team has the box, so hide the Alpha team box
                {
                    if (Alpha1Alias.Text == Alias) AlphaBox1.Visible = false;
                    if (Alpha2Alias.Text == Alias) AlphaBox2.Visible = false;
                    if (Alpha3Alias.Text == Alias) AlphaBox3.Visible = false;
                    if (Alpha4Alias.Text == Alias) AlphaBox4.Visible = false;
                    if (Alpha5Alias.Text == Alias) AlphaBox5.Visible = false;
                }


            }
        }
        private void SetBox(string Alias, int BoxNo, int TeamCode, bool Ping)
        {
            HideAlternativeBox(Alias, TeamCode);

            if (TeamCode == 1)//ALPHA TEAM
            {
                switch (BoxNo)
                {
                    case 1: ABoxSettings(AlphaBox1, Alpha1Alias, Alpha1Ping, Alias, Ping); break;
                    case 2: ABoxSettings(AlphaBox2, Alpha2Alias, Alpha2Ping, Alias, Ping); break;
                    case 3: ABoxSettings(AlphaBox3, Alpha3Alias, Alpha3Ping, Alias, Ping); break;
                    case 4: ABoxSettings(AlphaBox4, Alpha4Alias, Alpha4Ping, Alias, Ping); break;
                    case 5: ABoxSettings(AlphaBox5, Alpha5Alias, Alpha5Ping, Alias, Ping); break;
                }
            }
            if (TeamCode == 2)//BRAVO TEAM
            {
                switch (BoxNo)
                {
                    case 1: ABoxSettings(BravoBox1, Bravo1Alias, Bravo1Ping, Alias, Ping); break;
                    case 2: ABoxSettings(BravoBox2, Bravo2Alias, Bravo2Ping, Alias, Ping); break;
                    case 3: ABoxSettings(BravoBox3, Bravo3Alias, Bravo3Ping, Alias, Ping); break;
                    case 4: ABoxSettings(BravoBox4, Bravo4Alias, Bravo4Ping, Alias, Ping); break;
                    case 5: ABoxSettings(BravoBox5, Bravo5Alias, Bravo5Ping, Alias, Ping); break;
                }
            }
        }

        protected override void RefrehGridTimer_Tick(object sender, EventArgs e)
        {
            int[] DeviceList = new int[6];
            if (!SynchInProgress)
            {
                // HideAllPanels();
                Cursor.Current = Cursors.WaitCursor;
                //Update The screen from the database.

                if (connection.State == ConnectionState.Closed)
                {
                    connection.ConnectionString = Program.ConnectionString;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;


                    //SET ALL RUSH BOXES TO UNPINGED.
                    command.CommandText = "UPDATE SynchronisedDevice SET Ping = false  WHERE Device_Role_Code = 25 AND SynchronisedDevice.Enabled = True; ";
                    command.ExecuteNonQuery();

                    //PING EACH DEVICE
                    command.CommandText = "SELECT SATR_Unit_ID FROM SynchronisedDevice WHERE Device_Role_Code = 25 AND SynchronisedDevice.Enabled = True ORDER BY ALIAS ";//Find all RUSH Boxes
                    OleDbDataReader reader = command.ExecuteReader();
                    int RushBoxes = 0;
                    try
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (reader.Read())//Look at the first Domination box found 
                            {
                                RushBoxes++;
                                DeviceList[i] = reader.GetInt32(0);
                            }
                        }

                        reader.Close();
                        connection.Close();

                        //Monitor Details
                        for (int j = 0; j < RushBoxes; j++)
                        {
                            Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, DeviceList[j], 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                            SynchInProgress = true;
                            EnableWaitTimer();
                            while (SynchInProgress) { Application.DoEvents(); }//wait for the timer to complete 200ms for a response before sending next request

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
                    LoadScores();

                }
            }
        }

        private void TestSetAttackersDefendersTeam()
        {

            if (Program.RandomNumber(1, 100) > 49)
            {
                AttackersTeamCode = 2;
                DefendersTeamCode = 1;
            }
            else
            {
                DefendersTeamCode = 2;
                AttackersTeamCode = 1;
            }
        }
        private void TestDeviceResponse(int SATRID)
        {

            String StatusBar = "";
            int Captured = 0;
            if (Program.RandomNumber(0, 100) > 49)
                Captured = 1;

            Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode, SATRID, Program.ScoreboardID, (Int16)AttackersTeamCode, DefendersTeamCode, 0, 0, 0, Captured, 0, 0, 25, StatusBar);
        }

        private void LoadScores()
        {

            int AlphaBoxes = 0;
            int BravoBoxes = 0;

            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            int[] DeviceList = new int[6];
            bool[] PingList = new bool[6];
            int[] TeamCode = new int[6];

            //GET PING STATUS

            command.CommandText = "SELECT Ping, SATR_Unit_ID, ALIAS, Team_Code FROM SynchronisedDevice WHERE Device_Role_Code = 25  AND SynchronisedDevice.Enabled = True ORDER BY ALIAS ";//Find all the Rush Boxes
            OleDbDataReader reader = command.ExecuteReader();
            int i = 0;
            for (i = 0; i < 6; i++)
            {
                string BoxAlias = "";
                PingList[i] = false;
                DeviceList[i] = 0;
                TeamCode[i] = 0;
                if (reader.Read())
                {
                    PingList[i] = reader.GetBoolean(0);
                    DeviceList[i] = reader.GetInt32(1);
                    BoxAlias = reader[2].ToString();
                    TeamCode[i] = reader.GetInt32(3);//TEAM IN CONTROL
                    if (TeamCode[i] == 1)
                    {
                        AlphaBoxes++;
                        SetBox(BoxAlias, AlphaBoxes, TeamCode[i], PingList[i]);
                    }
                    else if (TeamCode[i] == 2)
                    {
                        BravoBoxes++;
                        SetBox(BoxAlias, BravoBoxes, TeamCode[i], PingList[i]);

                    }
                }
            }
            HideExcessBoxes(AlphaBoxes, BravoBoxes);
            reader.Close();
            connection.Close();
        }
        private void HideExcessBoxes(int AlphaBoxes, int BravoBoxes)
        {

            if (BravoBoxes == 0) BravoBox1.Visible = false;
            if (BravoBoxes < 2) BravoBox2.Visible = false;
            if (BravoBoxes < 3) BravoBox3.Visible = false;
            if (BravoBoxes < 4) BravoBox4.Visible = false;
            if (BravoBoxes < 5) BravoBox5.Visible = false;
            if (AlphaBoxes == 0) AlphaBox1.Visible = false;
            if (AlphaBoxes < 2) AlphaBox2.Visible = false;
            if (AlphaBoxes < 3) AlphaBox3.Visible = false;
            if (AlphaBoxes < 4) AlphaBox4.Visible = false;
            if (AlphaBoxes < 5) AlphaBox5.Visible = false;
        }
    }
}
