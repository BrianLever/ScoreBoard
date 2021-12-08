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
    public partial class FreeForAllForm : SATRScoreDisplay.BaseScoreboard
    {
        private int DeviceRowsDisplayed = 0;
        private int DeviceRecords = 0;
        private int CurrentPage = 0;
        private bool initialload = true;
        private OleDbConnection connection = new OleDbConnection();
        int[] colwidths = new int[20];

        public FreeForAllForm()
        {
            InitializeComponent();
        }

        private void SaveColumnWidths()
        {
            ResizeGrid();
            for (int i = 0; i < GamerGrid.Columns.Count; i++)
            {
                colwidths[i] = GamerGrid.Columns[i].Width;
            }
        }

        private void ResizeGrid()
        {
            if (Program.GenreCode == 3)//BF TAG
            {
                //ALIAS
                GamerGrid.Columns[0].MinimumWidth = 310;
                GamerGrid.Columns[0].Width = GamerGrid.Columns[0].MinimumWidth;
                //KILLWIN
                GamerGrid.Columns[1].MinimumWidth = 60;
                GamerGrid.Columns[1].Width = GamerGrid.Columns[1].MinimumWidth;

                //KILLS 265
                GamerGrid.Columns[2].MinimumWidth = 370;
                GamerGrid.Columns[2].Width = GamerGrid.Columns[2].MinimumWidth;
                //HITSWINNER 60
                GamerGrid.Columns[3].MinimumWidth = 60;
                GamerGrid.Columns[3].Width = GamerGrid.Columns[3].MinimumWidth;
                //HITS 210
                GamerGrid.Columns[4].MinimumWidth = 210;
                GamerGrid.Columns[4].Width = GamerGrid.Columns[4].MinimumWidth;

                //DEATHSWINNER 60
                GamerGrid.Columns[5].MinimumWidth = 60;
                GamerGrid.Columns[5].Width = GamerGrid.Columns[5].MinimumWidth;
                //Deaths 360
                GamerGrid.Columns[6].MinimumWidth = 360;
                GamerGrid.Columns[6].Width = GamerGrid.Columns[5].MinimumWidth;
                //KDWinner 60
                GamerGrid.Columns[7].MinimumWidth = 60;
                GamerGrid.Columns[7].Width = GamerGrid.Columns[7].MinimumWidth;

                //KtoD 185
                GamerGrid.Columns[8].MinimumWidth = 185;
                GamerGrid.Columns[8].Width = GamerGrid.Columns[5].MinimumWidth;

                //Padding1 180
                GamerGrid.Columns[9].MinimumWidth = 120;
                GamerGrid.Columns[9].Width = GamerGrid.Columns[9].MinimumWidth;

                //Ping 60
                GamerGrid.Columns[10].MinimumWidth = 60;
                GamerGrid.Columns[10].Width = GamerGrid.Columns[10].MinimumWidth;

                //Padding 2 195
                GamerGrid.Columns[11].MinimumWidth = 195;
                GamerGrid.Columns[11].Width = GamerGrid.Columns[11].MinimumWidth;
                //Emulation 375
                GamerGrid.Columns[12].MinimumWidth = 375;
                GamerGrid.Columns[12].Width = GamerGrid.Columns[12].MinimumWidth;

            }


        }

        protected override void ResizeScreen()
        {
            ControlDataViewPanel.Location = new Point(Program.xLocation(57, this.Width), Program.yLocation(335, this.Height));
            GamerGrid.Location = new Point(Program.xLocation(71, this.Width), Program.yLocation(0, this.Height));
            GamerGrid.Parent = ControlDataViewPanel;
            ControlDataViewPanel.Size = new Size(Program.xLocation(2456, this.Width), Program.yLocation(990, this.Height));//2456, 943
            GamerGrid.Size = new Size(Program.xLocation(2375, this.Width), Program.yLocation(990, this.Height));//2375, 934

            int fontheight = (int)(25 * this.Height / Program.ScreenHeight);
            GamerGrid.DefaultCellStyle.Font = new Font(GamerGrid.Font.FontFamily, fontheight);
            GamerGrid.RowsDefaultCellStyle.Font = new Font(GamerGrid.Font.FontFamily, fontheight);
            GamerGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;

           
            //resize the columnns
            for (int i = 0; i < GamerGrid.Columns.Count; i++)
            {
                int colw = colwidths[i];
                GamerGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colw = Program.xLocation(colw, this.Width);
                GamerGrid.Columns[i].MinimumWidth = colw;
                GamerGrid.Columns[i].Width = colw;
                GamerGrid.Columns[i].DefaultCellStyle.Font = new Font(GamerGrid.Font.FontFamily, fontheight);
            }

            progressBar1.Location = new Point(Program.xLocation(1050, this.Width), Program.yLocation(1350, this.Height));
            progressBar1.Size = new Size(Program.xLocation(624, this.Width), Program.yLocation(36, this.Height));
        }
        private void FreeForAllForm_Load(object sender, EventArgs e)
        {
            SetTimeLeftVisibility(false);
            SaveColumnWidths();
            ResizeScreen();
            ShowStatusText = false;
            RefreshGrid();
         //   UpdateDevices();
            //StartPageTimer();
            CheckTestMode();
            HideStatusLabel();


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

        private void StartPageTimer()
        {
            PageTimer.Enabled = false;
            PageTimer.Interval = Program.NextPageMS;
            PageTimer.Enabled = true;
        }

        private void UpdateDevices()
        {
            const int maxdevices = 2017;
        int[] DeviceList = new int[maxdevices];
        int i = 0;
        int GamersCount = 0;
            if (!SynchInProgress)
            {
                DisableRefreshGridTimer();
                PageTimer.Enabled = false;//Dont page through while uploading new devices
            }
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Visible = true;

            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
        command.Connection = connection;

                command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 1  AND SynchronisedDevice.Enabled = True ";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    GamersCount = reader.GetInt32(0);

                reader.Close();


                //SET ALL DEVICES TO UNPINGED.
                command.CommandText = "UPDATE SynchronisedDevice SET Ping = false; ";
                command.ExecuteNonQuery();

                //PING EACH DEVICE
                command.CommandText = "SELECT SATR_Unit_ID FROM SynchronisedDevice WHERE Device_Role_Code = 1  AND SynchronisedDevice.Enabled = True";
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

                    for (int j = 0; j<i; j++)
                    {
                        if (GamersCount > 0)
                            progressBar1.Value = (int) (j* 100 / GamersCount);
                        int SATRID = DeviceList[j];
    //Monitor Details

    Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");

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

        protected override void RefrehGridTimer_Tick(object sender, EventArgs e)
        {
            UpdateDevices();
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
                int Score = Program.RandomNumber(0, 400);
                int CurrentHP = Program.RandomNumber(0, 25);


                String StatusBar = "Ready";
                int Kills = Program.RandomNumber(0, 20);
                int Hits = Program.RandomNumber(0, 100);
                int Deaths = Program.RandomNumber(0, 12);
                int ShotsFired = Program.RandomNumber(200, 1000);
                int EmulationCode = Program.RandomNumber(1, 300);
                reader.Close();
                connection.Close();

                Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode, DeviceID, Program.ScoreboardID, TeamCode, CurrentHP, Hits, Kills, ShotsFired, Deaths, Score, EmulationCode, DeviceRoleCode, StatusBar);

            }

            connection.Close();
        }

        private void IndividualMedals()
        {
            

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 1  AND SynchronisedDevice.Enabled = True ";
            OleDbDataReader reader = command.ExecuteReader();
            int GamersCount = 0;
            if (reader.Read())
                GamersCount = reader.GetInt32(0);

            reader.Close();
            if (GamersCount > 0)
            {

                command.CommandText = "UPDATE SynchronisedDevice SET Leader_Kills=false, Leader_Hits=false, Lowest_Deaths=false, Leader_KD=false WHERE SynchronisedDevice.Enabled = True;";
                command.ExecuteNonQuery();


                command.CommandText = "SELECT Max(SynchronisedDevice.[Kills]) as Kills FROM SynchronisedDevice WHERE SynchronisedDevice.Enabled = True";
                reader = command.ExecuteReader();
                int BestKills = 0;
                if (reader.Read())
                    BestKills = (int)reader.GetInt32(0);
                reader.Close();
                if (BestKills > 0)
                {
                    command.CommandText = "UPDATE SynchronisedDevice SET Leader_Kills=true WHERE Kills =" + Convert.ToString(BestKills) + " AND SynchronisedDevice.Enabled = True;";
                    command.ExecuteNonQuery();
                }
                command.CommandText = "SELECT Max(SynchronisedDevice.[Hits]) as Hits FROM SynchronisedDevice";
                reader = command.ExecuteReader();
                int BestHits = 0;
                if (reader.Read())
                    BestHits = reader.GetInt32(0);
                reader.Close();
                if (BestHits > 0)
                {
                    command.CommandText = "UPDATE SynchronisedDevice SET Leader_Hits=true WHERE Hits =" + Convert.ToString(BestHits) + " AND SynchronisedDevice.Enabled = True;";
                    command.ExecuteNonQuery();
                }

                command.CommandText = "SELECT Max(SynchronisedDevice.[KD]) as KD FROM SynchronisedDevice WHERE SynchronisedDevice.Enabled = True";
                reader = command.ExecuteReader();

                float BestKD = 0;
                if (reader.Read())
                    BestKD = (float)reader.GetDouble(0);
                reader.Close();
                if (BestKD > 0)
                {
                    command.CommandText = "UPDATE SynchronisedDevice SET Leader_KD =true WHERE KD =" + Convert.ToString(BestKD) + " AND SynchronisedDevice.Enabled = True;";
                    command.ExecuteNonQuery();
                }

                command.CommandText = "SELECT Max(SynchronisedDevice.[Deaths]) as MaxDeaths FROM SynchronisedDevice WHERE SynchronisedDevice.Enabled = True";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)//At least one gamer has died
                    {
                        reader.Close();
                        command.CommandText = "SELECT Min(SynchronisedDevice.[Deaths]) as Deaths FROM SynchronisedDevice WHERE SynchronisedDevice.Enabled = True";
                        reader = command.ExecuteReader();

                        int LowestDeaths = 0;
                        if (reader.Read())
                            LowestDeaths = reader.GetInt32(0);
                        reader.Close();

                        command.CommandText = "UPDATE SynchronisedDevice SET Lowest_Deaths =true WHERE Deaths =" + Convert.ToString(LowestDeaths) + " AND SynchronisedDevice.Enabled = True;";
                        command.ExecuteNonQuery();
                    }
                }

            }
            
            connection.Close();
        }


        private void RefreshGrid()
        {
            if (!SynchInProgress)
            {

                //   DeviceRecords = 0;
                string directoryName = Program.rootdirectory;

                GamerGrid.Rows.Clear();
                foreach (DataGridViewRow myRow in GamerGrid.Rows)
                {
                    myRow.Cells[0].Value = null;
                    myRow.Cells[5].Value = null;
                }

                GamerGrid.Refresh();

                GamerGrid.AllowUserToAddRows = false;
                GamerGrid.ColumnHeadersVisible = false;
                GamerGrid.RowHeadersVisible = false;
           //     ((DataGridViewImageColumn)GamerGrid.Columns[6]).ImageLayout = DataGridViewImageCellLayout.Stretch;


                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                command.CommandText = "SELECT Count(*) as Cnt FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True AND Pinged_After_Start = True;";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (!(DeviceRecords == reader.GetInt32(0)))
                        initialload = true;//New gamers added so reload 
                    DeviceRecords = reader.GetInt32(0);
                }

                reader.Close();
                connection.Close();

                IndividualMedals();

                connection.Open();
                command.CommandText = "SELECT SynchronisedDevice.ALIAS, TeamsByGenre.Team_Name, SynchronisedDevice.Ping, SynchronisedDevice.Kills, SynchronisedDevice.Hits, SynchronisedDevice.Deaths, SynchronisedDevice.Shots_Fired, Emulation.Emulation_Name, SynchronisedDevice.Score, SynchronisedDevice.SATR_Unit_ID " +
                ", SynchronisedDevice.Leader_Kills, SynchronisedDevice.Leader_Hits,SynchronisedDevice.Lowest_Deaths,Leader_KD,SynchronisedDevice.KD " +
                "FROM(SynchronisedDevice LEFT JOIN Emulation ON SynchronisedDevice.Emulation_Code = Emulation.Emulation_Code) INNER JOIN(Config INNER JOIN TeamsByGenre ON Config.Genre_Code = TeamsByGenre.Genre_Code) ON SynchronisedDevice.Team_Code = TeamsByGenre.Team_Code " +
                "WHERE(((SynchronisedDevice.Device_Role_Code) = 1)) AND SynchronisedDevice.Enabled = True AND Pinged_After_Start = True ORDER BY SynchronisedDevice.Kills DESC; ";
                reader = command.ExecuteReader();


                int i = 0;
                int GridRow = 0;
                while (reader.Read())
                {
                    i++;


                    if (initialload || ((i >= (CurrentPage * DeviceRowsDisplayed)) && (i <= (CurrentPage * DeviceRowsDisplayed + DeviceRowsDisplayed))))
                    {

                        //dataGridView1.Rows[2].Cells[3].Value = true;

                        String Alias = reader[0].ToString();

                        Boolean Ping = reader.GetBoolean(2);
                        int Kills = reader.GetInt32(3);
                        int Hits = reader.GetInt32(4);
                        int Deaths = reader.GetInt32(5);
                        int ShotsFired = reader.GetInt32(6);
                        String EmulationName = reader[7].ToString();
                        //  String Score = Convert.ToString(reader.GetInt32(8));
                        String SATRID = Convert.ToString(reader.GetInt32(9));
                        bool LeaderKills = reader.GetBoolean(10);
                        bool LeaderHits = reader.GetBoolean(11);
                        bool LowestDeaths = reader.GetBoolean(12);
                        bool LeaderKD = reader.GetBoolean(13);

                        float KtoD = 0F;
                        if (Deaths > 0)
                            KtoD = ((float)Kills / (float)Deaths);
                        else if (Kills > 0)
                            KtoD = 99;
                        float Accuracy = 0F;
                        if (ShotsFired > 0)
                            Accuracy = ((float)Kills + (float)Hits) * 100 / (float)ShotsFired;


      
                        //LOAD THE PING IMAGE
                        Image PingImage = null;
                        string pingfile;
                        if (Ping)
                        {
                            if (!(GridRow % 2 == 0))
                                pingfile = "SilverRedPing.png";
                            else
                                pingfile = "RedPing.png";
                        }
                        else
                        {
                            if (!(GridRow % 2 == 0))
                                pingfile = "SilverNoPing.png";
                            else
                                pingfile = "NoPing.png";
                        }

                        String ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + pingfile;
                        if (!File.Exists(ImageFileName))//
                            ImageFileName = directoryName + @"\Images\" + pingfile;
                        PingImage = Image.FromFile(ImageFileName);

                        if (SATRID.Length > 0)
                        {
                            GamerGrid.Rows.Add(Alias, Program.MedalImage(LeaderKills, GridRow),Convert.ToString(Kills), Program.MedalImage(LeaderHits, GridRow), Convert.ToString(Hits), Program.MedalImage(LowestDeaths, GridRow), Convert.ToString(Deaths), Program.MedalImage(LeaderKD, GridRow), KtoD.ToString("n2"), "",PingImage,"", EmulationName);//PingImage,, EmulationNameAccuracy.ToString("n2")
                            if (!(GamerGrid == null))
                                if (!(this.GamerGrid.CurrentCell == null))
                                    this.GamerGrid.CurrentCell.Selected = false;
                            GridRow++;
                        }
                    }

                }
                reader.Close();
                connection.Close();

                for (i = 0; i < GamerGrid.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                        GamerGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    else
                        GamerGrid.Rows[i].Cells[3].Style.BackColor = Color.Silver;


                }
                foreach (DataGridViewRow row in GamerGrid.Rows)
                {
                    row.Height = Program.yLocation(50, this.Height);
                }
                //((DataGridViewImageColumn)GamerGrid.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Stretch;


                if (initialload)
                {
                    DeviceRowsDisplayed = GamerGrid.DisplayedRowCount(false);
                    initialload = false;
                }

                if (DeviceRowsDisplayed < DeviceRecords) //Need to page through
                {
                    CurrentPage++;
                    if (DeviceRowsDisplayed * CurrentPage > DeviceRecords)
                    {
                        CurrentPage = 0;
                    }
                    StartPageTimer();

                }


            }
        }

        private void PageTimer_Tick(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }

}
