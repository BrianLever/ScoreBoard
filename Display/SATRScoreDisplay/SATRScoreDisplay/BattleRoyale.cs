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
    public partial class BattleRoyale : SATRScoreDisplay.BaseScoreboard
    {
        int[] colwidths = new int[23];
        private OleDbConnection connection = new OleDbConnection();
        int DeviceRecords = 0;
        int GridRow = 0;
        const int maxteams = 8;

        struct TeamStatsType
        {
            public String TeamName;
            public int Pings;
            public int Kills;
            public int Hits;
            public int Deaths;
            public int Survivors;
            public int ShotsFired;
            public float KtoD;
            public bool GamerFound;
            public bool BestSurvivor;
            public bool LeastDeaths;
            public bool MostKills;
            public bool MostHits;
            public bool MostShots;
            public bool BestKD;
            public bool Leader;

        }

        TeamStatsType[] TeamStat = new TeamStatsType[maxteams];

        struct leadertype
        {
            public int teamid;
            public int score;
        }

        public BattleRoyale()
        {
            InitializeComponent();
        }
        private void SaveColumnWidths()
        {

            for (int i = 0; i < TeamGrid.Columns.Count; i++)
            {
                colwidths[i] =TeamGrid.Columns[i].Width;
            }
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

        protected override void ResizeScreen()
        {
            ControlDataViewPanel.Location = new Point(Program.xLocation(46, this.Width), Program.yLocation(369, this.Height));//46, 
            ControlDataViewPanel.Size = new Size(Program.xLocation(2476, this.Width), Program.yLocation(560, this.Height));//2476, 600
            TeamGrid.Size = new Size(Program.xLocation(2476, this.Width), Program.yLocation(1040, this.Height));//2476, 1023
            TeamGrid.Location = new Point(0, 0);
            TeamGrid.RowTemplate.MinimumHeight = Program.yLocation(70, this.Height);
            int fontheight = (int)(48 * this.Height / Program.ScreenHeight);
            TeamGrid.DefaultCellStyle.Font = new Font(TeamGrid.Font.FontFamily, fontheight);
            TeamGrid.RowsDefaultCellStyle.Font = new Font(TeamGrid.Font.FontFamily, fontheight);

            //resize the columnns
            for (int i = 0; i < TeamGrid.Columns.Count; i++)
            {
                int colw = colwidths[i];//GET SAVED WIDTHS
                TeamGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //colw = (int)(colw * this.Width / Program.ScreenWidth);
                colw = Program.xLocation(colw, this.Width);
                TeamGrid.Columns[i].MinimumWidth = colw;
                TeamGrid.Columns[i].Width = colw;
                TeamGrid.Columns[i].DefaultCellStyle.Font = new Font(TeamGrid.Font.FontFamily, fontheight);
            }
            TeamGrid.RowsDefaultCellStyle.BackColor = Color.Black;
            TeamGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;
            progressBar1.Location = new Point(Program.xLocation(1000, this.Width), Program.yLocation(1350, this.Height));
            progressBar1.Size = new Size(Program.xLocation(624, this.Width), Program.yLocation(36, this.Height));
            foreach (DataGridViewRow row in TeamGrid.Rows)
            {
                row.Height = Program.yLocation(70, this.Height);
            }

        }


        private void TeamPositions()
        {
            int BestSurvivors = -1;
           // int WinningTeams = 0;
      
            //SURVIVORS LEADER/S
            for (int i = 1; i < maxteams; i++)
            {
                //DETERMINE TEAMS THAT ARE EQUAL FIRST ON SURVIVORS
                if (TeamStat[i].Survivors > BestSurvivors)
                    BestSurvivors = TeamStat[i].Survivors;
            }
            for (int i = 1; i < maxteams; i++)
            {
                //Mark the best survivor teams
                if (TeamStat[i].Survivors == BestSurvivors && TeamStat[i].Survivors > 0)
                {
                    TeamStat[i].BestSurvivor = true;
            //        WinningTeams++;
                }
                else
                    TeamStat[i].BestSurvivor = false;
            }
            int LowestDeaths = 10000;
            //LOWEST DEATHS LEADER
            bool AllZeroDeaths = true;
            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].Deaths < LowestDeaths)
                    LowestDeaths = TeamStat[i].Deaths;
                if (TeamStat[i].Deaths > 0)
                    AllZeroDeaths = false;
            }

            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].Deaths == LowestDeaths && !AllZeroDeaths)
                {
                    TeamStat[i].LeastDeaths = true;
                }
                else
                    TeamStat[i].LeastDeaths = false;
            }
            int MostKills = -1;
            //MOST KILLS
            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].Kills > MostKills && TeamStat[i].Kills > 0)
                    MostKills = TeamStat[i].Kills;
            }
            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].Kills == MostKills)
                {
                    TeamStat[i].MostKills = true;
                }
                else
                    TeamStat[i].MostKills = false;
            }
            //MOST HITS
            int MostHits = -1;
            
            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].Hits > MostHits)
                    MostHits = TeamStat[i].Hits;
            }
            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].Hits == MostHits && TeamStat[i].Hits > 0)
                {
                    TeamStat[i].MostHits = true;
                }
                else
                    TeamStat[i].MostHits = false;
            }
            //MOST SHOTS
            int MostShots = -1;

            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].ShotsFired > MostShots )
                    MostShots = TeamStat[i].ShotsFired;
            }
            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].ShotsFired == MostShots && TeamStat[i].ShotsFired > 0)
                {
                    TeamStat[i].MostShots = true;
                }
                else
                    TeamStat[i].MostShots = false;
            }
            //BEST K/D
            float BestKD = 0;

            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].KtoD > BestKD)
                    BestKD = TeamStat[i].KtoD;
            }
            for (int i = 1; i < maxteams; i++)
            {
                if (TeamStat[i].KtoD > (BestKD-0.0099) && TeamStat[i].KtoD > 0)
                {
                    TeamStat[i].BestKD = true;
                }
                else
                    TeamStat[i].BestKD = false;
            }
        }

        private void CalculateWinner()
        {
        
            leadertype[] Leaders = new leadertype[maxteams];
            int NoLeaders = 0;
            //First Add leaders that are best on Survivors

            for (int i = 0; i < maxteams; i++)
            {
                Leaders[i].score = 0;
                if (TeamStat[i].BestSurvivor)
                {
                    NoLeaders++;
                    Leaders[NoLeaders].teamid = i;
                }
            }
            if (NoLeaders == 1)//There is only one team with the minimum number of survivors
            {
                TeamStat[Leaders[NoLeaders].teamid].Leader = true;
            }
            else//check lowest deaths
            {
                int NewNoLeaders = 0;
                int LowestScore = 10000;
                for (int i = 1; i <= NoLeaders; i++)
                {
                    Leaders[i].score = TeamStat[Leaders[i].teamid].Deaths;
                    if (Leaders[i].score < LowestScore)
                        LowestScore = Leaders[i].score;
                }

                for (int i = 1; i <= NoLeaders; i++)
                {
                    if (TeamStat[Leaders[i].teamid].Deaths == LowestScore)
                    {
                        NewNoLeaders++;
                        Leaders[NewNoLeaders].teamid = Leaders[i].teamid;
                    }
                }
                NoLeaders = NewNoLeaders;
                if (NoLeaders == 1)//There is only one team with the minimum number of deaths from the best survivors teams
                {
                    TeamStat[Leaders[NoLeaders].teamid].Leader = true;
                }
                else//Check Most Kills
                {
                    NewNoLeaders = 0;
                    int BestScore = 0;
                    for (int i = 1; i <= NoLeaders; i++)
                    {
                        Leaders[i].score = TeamStat[Leaders[i].teamid].Kills;
                        if (Leaders[i].score > BestScore)
                            BestScore = Leaders[i].score;
                    }

                    for (int i = 1; i <= NoLeaders; i++)
                    {
                        if (TeamStat[Leaders[i].teamid].Kills == BestScore)
                        {
                            NewNoLeaders++;
                            Leaders[NewNoLeaders].teamid = Leaders[i].teamid;
                        }
                    }
                    NoLeaders = NewNoLeaders;
                    if (NoLeaders == 1)//There is only one team with most kills from the best teams
                    {
                        TeamStat[Leaders[NoLeaders].teamid].Leader = true;
                    }
                    else//Most Hits
                    {
                        NewNoLeaders = 0;
                        BestScore = 0;
                        for (int i = 1; i <= NoLeaders; i++)
                        {
                            Leaders[i].score = TeamStat[Leaders[i].teamid].Hits;
                            if (Leaders[i].score > BestScore)
                                BestScore = Leaders[i].score;
                        }

                        for (int i = 1; i <= NoLeaders; i++)
                        {
                            if (TeamStat[Leaders[i].teamid].Hits == BestScore)
                            {
                                NewNoLeaders++;
                                Leaders[NewNoLeaders].teamid = Leaders[i].teamid;
                            }
                        }
                        NoLeaders = NewNoLeaders;
                        if (NoLeaders == 1)//There is only one team with most hits from the best teams
                        {
                            TeamStat[Leaders[NoLeaders].teamid].Leader = true;
                        }
                    }
                }

            }

        }
        protected override void TriggerResultDisplay()
        {
            Cursor.Current = Cursors.Default;
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
                //       Int16 TeamCode = Program.RandomNumber16(1, 8);
                Int16 TeamCode = (Int16)reader.GetInt32(3);
                if (TeamCode > 7)
                    TeamCode = 7;
                String StatusBar = "Ready";
                int Kills = Program.RandomNumber(0, 20);
                int Hits = Program.RandomNumber(0, 100);
                int Deaths = Program.RandomNumber(0, 12);
                int ShotsFired = Program.RandomNumber(200, 1000);
                int EmulationCode = Program.RandomNumber(1, 300);
                int HitPoints = 0;
                if (Program.RandomNumber(1,100)< 80)
                    HitPoints = Program.RandomNumber(1, 25);
              //  testBox.AppendText(Convert.ToString(TeamCode) + " " + Convert.ToString(HitPoints)+"\r\n");

                Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode, DeviceID, Program.ScoreboardID, TeamCode, HitPoints, Hits, Kills, ShotsFired, Deaths, Kills, EmulationCode, DeviceRoleCode, StatusBar);

            }
            reader.Close();
            connection.Close();
        }

        private void RefreshGrid()
        {
        
            for (int i=0;i<maxteams;i++)
            {
                TeamStat[i].TeamName = "";
                TeamStat[i].Hits = 0;
                TeamStat[i].Kills = 0;
                TeamStat[i].KtoD = 0;
                TeamStat[i].Pings = 0;
                TeamStat[i].ShotsFired = 0;
                TeamStat[i].Deaths = 0;
                TeamStat[i].Survivors = 0;
                TeamStat[i].GamerFound = false;
                TeamStat[i].BestSurvivor = false;
                TeamStat[i].LeastDeaths = false;
                TeamStat[i].MostKills = false;
                TeamStat[i].MostHits = false;
                TeamStat[i].Leader = false;
                TeamStat[i].MostShots = false;
                TeamStat[i].BestKD = false;
            }

            if (!(SynchInProgress))
            {
                //   DeviceRecords = 0;
                string directoryName = Program.rootdirectory;

                TeamGrid.Rows.Clear();
                foreach (DataGridViewRow myRow in TeamGrid.Rows)
                {
                    myRow.Cells[0].Value = null;
                    myRow.Cells[5].Value = null;
                }

                TeamGrid.Refresh();

                TeamGrid.AllowUserToAddRows = false;
                TeamGrid.ColumnHeadersVisible = false;
                TeamGrid.RowHeadersVisible = false;
                //      ((DataGridViewImageColumn)TeamGrid.Columns[3]).ImageLayout = DataGridViewImageCellLayout.Normal;

                if (!(connection.State == ConnectionState.Open))
                {
                    connection.ConnectionString = Program.ConnectionString;
                    connection.Open();
                }
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                command.CommandText = "SELECT Count(*) as Cnt FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True;";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DeviceRecords = reader.GetInt32(0);
                }

                reader.Close();

                command.CommandText = "SELECT TeamsByGenre.Team_Name, SynchronisedDevice.Ping, SynchronisedDevice.Kills, SynchronisedDevice.Hits, SynchronisedDevice.Deaths, SynchronisedDevice.Hit_Points, SynchronisedDevice.SATR_Unit_ID,SynchronisedDevice.Shots_Fired,SynchronisedDevice.Team_Code  " +
                "FROM SynchronisedDevice INNER JOIN (Config INNER JOIN TeamsByGenre ON Config.Genre_Code = TeamsByGenre.Genre_Code) ON SynchronisedDevice.Team_Code = TeamsByGenre.Team_Code WHERE SynchronisedDevice.Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True ORDER BY SynchronisedDevice.Team_Code  ;";
                reader = command.ExecuteReader();
                
                GridRow = 0;
                int TeamCode = 0;
                
                while (reader.Read())
                {
                
                    int SATRID = reader.GetInt32(6);
                    if (SATRID > 0)
                    {
                        TeamCode = reader.GetInt32(8);
                        if (TeamCode >= 0 && TeamCode < maxteams)
                        {
                            TeamStat[TeamCode].TeamName = reader[0].ToString();
                            Boolean Ping = reader.GetBoolean(1);
                            if (Ping)
                                TeamStat[TeamCode].Pings++;
                            TeamStat[TeamCode].Kills += reader.GetInt32(2);
                            TeamStat[TeamCode].Hits += reader.GetInt32(3);
                            TeamStat[TeamCode].Deaths += reader.GetInt32(4);
                            if (reader.GetInt32(5) > 0)
                                TeamStat[TeamCode].Survivors++;
                            TeamStat[TeamCode].ShotsFired += reader.GetInt32(7);
                            if (TeamStat[TeamCode].Deaths > 0)
                                TeamStat[TeamCode].KtoD = ((float)TeamStat[TeamCode].Kills / (float)TeamStat[TeamCode].Deaths);
                            else if (TeamStat[TeamCode].Kills > 0)
                                TeamStat[TeamCode].KtoD = 99;
                            TeamStat[TeamCode].GamerFound = true;
                        }
                       
                    }
                }
                reader.Close();
                connection.Close();
                TeamPositions();
                CalculateWinner();
                for (int i = 1; i < maxteams; i++)
                    if (TeamStat[i].GamerFound)
                        InsertTeamRow(i);

                //this.TeamGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                for (int i = 0; i < TeamGrid.Rows.Count; i++)
                {
                    //        TeamGrid.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                    if (i % 2 == 0)
                        TeamGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    else
                        TeamGrid.Rows[i].Cells[3].Style.BackColor = Color.Silver;


                }
                foreach (DataGridViewRow row in TeamGrid.Rows)
                {
                    row.Height = Program.yLocation(80, this.Height);
                }


            }
        }



        private Image WinnerImage(bool medalwinner)
        {
            Image MedalImage = null;
            Image NoMedalImage = null;
            string Medalfile;
            string NoMedalFile;
            if (!(GridRow % 2 == 0))
            {
                NoMedalFile = "SilverBox.png";
                Medalfile = "starSilver.png";//MedalSilver.png
            }
            else
            {
                NoMedalFile = "BlackBox.png";
                Medalfile = "star.png";
            }
            

            string directoryName = Program.rootdirectory;

            String MedalFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + Medalfile;
            if (!File.Exists(MedalFileName))
                MedalFileName = directoryName + @"\Images\" + Medalfile;
            if (File.Exists(MedalFileName))
                MedalImage = Image.FromFile(MedalFileName);
            else
                MessageBox.Show("No Medal Image " + MedalFileName);

            String NoMedalFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + NoMedalFile;
            if (!File.Exists(NoMedalFileName))
                NoMedalFileName = directoryName + @"\Images\" + NoMedalFile;
            if (File.Exists(NoMedalFileName))
                NoMedalImage = Image.FromFile(NoMedalFileName);
            if (medalwinner)
                return MedalImage;
            else
                return NoMedalImage;
        }
        void InsertTeamRow(int TeamCode)
        {
         
        

            /*
Team
Trophy
Space1
Survivors
SWinner
Space2
Pings
Space3
Kills
KillsWinner
Space4
Hits
HitsWinner
Space5
Deaths
DeathsWinner
Space6
ShotsFired
K/D*/


            TeamGrid.Rows.Add(WinnerImage(TeamStat[TeamCode].Leader), TeamStat[TeamCode].TeamName, "", Program.MedalImage(TeamStat[TeamCode].BestSurvivor, GridRow), Convert.ToString(TeamStat[TeamCode].Survivors),  "", Convert.ToString(TeamStat[TeamCode].Pings), "",
                Program.MedalImage(TeamStat[TeamCode].MostKills, GridRow),Convert.ToString(TeamStat[TeamCode].Kills), "", Program.MedalImage(TeamStat[TeamCode].MostHits, GridRow), Convert.ToString(TeamStat[TeamCode].Hits), "",
                     Program.MedalImage(TeamStat[TeamCode].LeastDeaths, GridRow), Convert.ToString(TeamStat[TeamCode].Deaths), "", Program.MedalImage(TeamStat[TeamCode].MostShots, GridRow), Convert.ToString(TeamStat[TeamCode].ShotsFired), Program.MedalImage(TeamStat[TeamCode].BestKD, GridRow), TeamStat[TeamCode].KtoD.ToString("n2"));
            if (!(TeamGrid == null))
                try
                {
                    this.TeamGrid.CurrentCell.Selected = false;
                }
                finally { };

            if (!(GridRow % 2 == 0))
            {
                TeamGrid.Rows[GridRow].Cells[3].Style.BackColor = Color.Silver;
            }
            GridRow++;
        }

        protected override void RefrehGridTimer_Tick(object sender, EventArgs e)
        {
            const int maxdevices = 2017;
            int[] DeviceList = new int[maxdevices];
            int i = 0;
            int GamersCount = 0;
            if (!SynchInProgress)
                DisableRefreshGridTimer();
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Visible = true;
            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True ";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    GamersCount = reader.GetInt32(0);

                reader.Close();


                //SET ALL DEVICES TO UNPINGED.
                command.CommandText = "UPDATE SynchronisedDevice SET Ping = false; ";
                command.ExecuteNonQuery();

                //PING EACH DEVICE
                command.CommandText = "SELECT SATR_Unit_ID FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True";
                reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        DeviceList[i] = reader.GetInt32(0);
                        i++;
                    }
                    reader.Close();
                    connection.Close();

                    for (int j = 0; j < i; j++)
                    {
                        if (GamersCount > 0)
                            progressBar1.Value = (int)(j * 100 / GamersCount);
                        int SATRID = DeviceList[j];
                        //Monitor Details

                        Program.SendRadioPacket(0, 26, Program.ScoreboardBattleCode, SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");
                        //Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

                  //      int TimeofWait = GetWaitInterval();
                        SynchInProgress = true;
                        
                        EnableWaitTimer();

                        while (SynchInProgress) { Application.DoEvents(); }//wait for the timer to complete 200ms for a response before sending next request

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
                        if (CloseApp)
                        {
                            this.Close();
                            break;
                        }
                    }
                }

                finally
                {

                    RefreshGrid();

                }
                Cursor.Current = Cursors.Default;
                EnableRefreshGridTimer();
            }
            HideSyncPicture();
            progressBar1.Visible = false;

         }

        private void BattleRoyale_Load(object sender, EventArgs e)
        {
            SaveColumnWidths();
            ResizeScreen();
            RefreshGrid();
            SetTimeLeftVisibility(false);
            CheckTestMode();
            HideStatusLabel();
           
        }

        private void TeamGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
