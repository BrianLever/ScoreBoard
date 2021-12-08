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
    public partial class GamersMonitorForm : SATRScoreDisplay.BaseScoreboard
    {
    
        private int DeviceRowsDisplayed = 0;
        private int DeviceRecords = 0;
        private int CurrentPage = 0;
        private bool initialload = true;
        private OleDbConnection connection = new OleDbConnection();
        int[] colwidths = new int[15];

        public GamersMonitorForm()
        {
            InitializeComponent();
        }

        private void SaveColumnWidths()
        {
  
            for (int i = 0; i < GamerGrid.Columns.Count; i++)
            {
                colwidths[i] = GamerGrid.Columns[i].Width;
            }
        }

        protected override void ResizeScreen()
        {
            ControlDataViewPanel.Location = new Point(Program.xLocation(42, this.Width), Program.yLocation(300, this.Height));//42, 284
            ControlDataViewPanel.Size = new Size(Program.xLocation(2476, this.Width), Program.yLocation(1040, this.Height));//2476, 1040
            GamerGrid.Size = new Size(Program.xLocation(2476, this.Width), Program.yLocation(1040, this.Height));//2476, 1023
            GamerGrid.Location = new Point(0, 0);

            int fontheight = (int)(25 * this.Height / Program.ScreenHeight);
            GamerGrid.DefaultCellStyle.Font = new Font(GamerGrid.Font.FontFamily, fontheight);
            GamerGrid.RowsDefaultCellStyle.Font = new Font(GamerGrid.Font.FontFamily, fontheight);

            //resize the columnns
            for (int i = 0; i < GamerGrid.Columns.Count; i++)
            {
                int colw = colwidths[i];//GET SAVED WIDTHS
                GamerGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //colw = (int)(colw * this.Width / Program.ScreenWidth);
                colw = Program.xLocation(colw, this.Width);
                GamerGrid.Columns[i].MinimumWidth = colw;
                GamerGrid.Columns[i].Width = colw;
                GamerGrid.Columns[i].DefaultCellStyle.Font = new Font(GamerGrid.Font.FontFamily, fontheight);
            }
            GamerGrid.RowsDefaultCellStyle.BackColor = Color.Black;
            GamerGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;
            progressBar1.Location = new Point(Program.xLocation(1000, this.Width), Program.yLocation(1350, this.Height));
            progressBar1.Size = new Size(Program.xLocation(624, this.Width), Program.yLocation(36, this.Height));
       
        }

        private void RefreshGrid()
        {
            if (!(SynchInProgress))
            {
                PageTimer.Enabled = false;
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
          //      ((DataGridViewImageColumn)GamerGrid.Columns[3]).ImageLayout = DataGridViewImageCellLayout.Normal;


                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                command.CommandText = "SELECT Count(*) as Cnt FROM SynchronisedDevice WHERE Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True;";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (!(DeviceRecords == reader.GetInt32(0)))
                        initialload = true;//New gamers added so reload 
                    DeviceRecords = reader.GetInt32(0);
                }

                reader.Close();

                command.CommandText = "SELECT SynchronisedDevice.ALIAS, TeamsByGenre.Team_Name, SynchronisedDevice.Ping, SynchronisedDevice.Kills, SynchronisedDevice.Hits, SynchronisedDevice.Deaths, SynchronisedDevice.Shots_Fired, Emulation.Emulation_Name, SynchronisedDevice.Score, SynchronisedDevice.SATR_Unit_ID " +
                "FROM(SynchronisedDevice LEFT JOIN Emulation ON SynchronisedDevice.Emulation_Code = Emulation.Emulation_Code) INNER JOIN(Config INNER JOIN TeamsByGenre ON Config.Genre_Code = TeamsByGenre.Genre_Code) ON SynchronisedDevice.Team_Code = TeamsByGenre.Team_Code " +
                "WHERE(((SynchronisedDevice.Device_Role_Code) = 1)) AND SynchronisedDevice.Enabled = True ORDER BY SynchronisedDevice.ALIAS; ";
                reader = command.ExecuteReader();


                int i = 0;
                int GridRow = 0;

                while (reader.Read())
                {
                    i++;


                    if (initialload || ((i >= (CurrentPage * DeviceRowsDisplayed)) && (i <= (CurrentPage * DeviceRowsDisplayed + DeviceRowsDisplayed))))
                    {

                        
                        
                        String Alias = reader[0].ToString();
                        String TeamName = reader[1].ToString();
                        Boolean Ping = reader.GetBoolean(2);
                        int Kills = reader.GetInt32(3);
                        int Hits = reader.GetInt32(4);
                        int Deaths = reader.GetInt32(5);
                        int ShotsFired = reader.GetInt32(6);
                        String EmulationName = reader[7].ToString();
                        String Score = Convert.ToString(reader.GetInt32(8));
                        String SATRID = Convert.ToString(reader.GetInt32(9));
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
                            GamerGrid.Rows.Add(Alias, TeamName,"", PingImage,"", Convert.ToString(Kills), Convert.ToString(Hits), Convert.ToString(Deaths), Convert.ToString(ShotsFired), EmulationName, KtoD.ToString("n2"), Score, Accuracy.ToString("n2"), SATRID);
                            if (!(GamerGrid == null))
                                try
                                {
                                    this.GamerGrid.CurrentCell.Selected = false;
                                }
                                finally { };
                            if (!(GridRow % 2 == 0))
                            {
                                GamerGrid.Rows[GridRow].Cells[3].Style.BackColor = Color.Silver;
                            }
                            GridRow++;
                        }
                    }

                }
                reader.Close();
                connection.Close();


                //this.GamerGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                for (i = 0; i < GamerGrid.Rows.Count; i++)
                {
            //        GamerGrid.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                    if (i%2==0)
                      GamerGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    else
                        GamerGrid.Rows[i].Cells[3].Style.BackColor = Color.Silver;


                }
                foreach (DataGridViewRow row in GamerGrid.Rows)
                {
                    row.Height = (int)(50 * this.Height / Program.ScreenHeight);
                }
                


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

              //  ShowSyncPicture();
                //Update The screen from the database.
                //MessageBox.Show("Update Grid");
            if (connection.State == ConnectionState.Closed)
                {
                    connection.ConnectionString = Program.ConnectionString;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT Count(*) FROM SynchronisedDevice WHERE Device_Role_Code = 1  AND SynchronisedDevice.Enabled = True";
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

                    for (int j = 0; j < i; j++)
                    {
                        if (GamersCount > 0)
                            progressBar1.Value = (int)(j * 100 / GamersCount);
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

        private void SetLaserTagColWidths()//Background widths different for Battlefield Tag genre.
        {
            if (Program.GenreCode == 3)
            {
                this.GamerGrid.Columns[0].MinimumWidth = 355;
                this.GamerGrid.Columns[1].MinimumWidth = 190;
                this.GamerGrid.Columns[2].MinimumWidth = 60;
                this.GamerGrid.Columns[3].MinimumWidth = 50;
                this.GamerGrid.Columns[4].MinimumWidth = 65;
                this.GamerGrid.Columns[5].MinimumWidth = 270;
                this.GamerGrid.Columns[6].MinimumWidth = 117;
                this.GamerGrid.Columns[7].MinimumWidth = 180;
                this.GamerGrid.Columns[8].MinimumWidth = 245;
                this.GamerGrid.Columns[9].MinimumWidth = 345;
                this.GamerGrid.Columns[10].MinimumWidth = 146;
                this.GamerGrid.Columns[11].MinimumWidth = 140;
                this.GamerGrid.Columns[12].MinimumWidth = 150;
                this.GamerGrid.Columns[13].MinimumWidth = 200;
                for (int i = 0; i < 14; i++)
                    this.GamerGrid.Columns[i].Width = this.GamerGrid.Columns[i].MinimumWidth;
            }
        }

        private void GamersMonitorForm_Load(object sender, EventArgs e)
        {
            SetLaserTagColWidths();
            SaveColumnWidths();
            ResizeScreen();
            RefreshGrid();
            SetTimeLeftVisibility(false);
            CheckTestMode();
            HideStatusLabel();
           // StartPageTimer();
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

        private void PageTimer_Tick(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void GamerGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    
}
