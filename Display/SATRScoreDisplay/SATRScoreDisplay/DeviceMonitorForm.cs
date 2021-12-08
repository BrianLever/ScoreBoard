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
    public partial class DeviceMonitorForm : SATRScoreDisplay.BaseScoreboard
    {
        
        private int DeviceRowsDisplayed = 0;
        private int DeviceRecords = 0;
        private int CurrentPage = 0;
        private bool initialload = true;
        int[] colwidths = new int[15];

        private OleDbConnection connection = new OleDbConnection();
        public DeviceMonitorForm()
        {
            InitializeComponent();
        }

        private void SaveColumnWidths()
        {

            for (int i = 0; i < DeviceGrid.Columns.Count; i++)
            {
                colwidths[i] = DeviceGrid.Columns[i].Width;
            }
        }

        private void RefreshGrid()
        {
           DeviceRecords = 0;
           
            string directoryName = Program.rootdirectory;

            DeviceGrid.Rows.Clear();
            foreach (DataGridViewRow myRow in DeviceGrid.Rows)
            {
                myRow.Cells[0].Value = null;
                myRow.Cells[5].Value = null;
            }

            DeviceGrid.Refresh();
           
            DeviceGrid.AllowUserToAddRows = false;
            DeviceGrid.ColumnHeadersVisible = false;
            DeviceGrid.RowHeadersVisible = false;
            DeviceGrid.Columns["RoleBadge"].DefaultCellStyle.NullValue = null;
            ((DataGridViewImageColumn)DeviceGrid.Columns[0]).ImageLayout = DataGridViewImageCellLayout.Stretch;
       //     ((DataGridViewImageColumn)DeviceGrid.Columns[5]).ImageLayout = DataGridViewImageCellLayout.Normal;


            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            //                Program.ScoreboardBattleCode = reader.GetInt16(1);

            command.CommandText = "SELECT Count(*) as Cnt FROM SynchronisedDevice WHERE NOT Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
                if (!(DeviceRecords == reader.GetInt32(0)))
                    initialload = true;//New gamers added so reload 
            DeviceRecords = reader.GetInt32(0);

            reader.Close();

            command.CommandText = "SELECT DeviceRole.Device_Name, SynchronisedDevice.ALIAS,  TeamsByGenre.Team_Name, SynchronisedDevice.Score, SynchronisedDevice.Ping, SynchronisedDevice.Status_Bar, SynchronisedDevice.SATR_Unit_ID, DeviceRole.Badge_FileName FROM(SynchronisedDevice INNER JOIN DeviceRole ON SynchronisedDevice.Device_Role_Code = DeviceRole.Device_Role_Code) INNER JOIN(TeamsByGenre INNER JOIN Config ON TeamsByGenre.Genre_Code = Config.Genre_Code) ON SynchronisedDevice.Team_Code = TeamsByGenre.Team_Code WHERE NOT SynchronisedDevice.Device_Role_Code = 1 AND SynchronisedDevice.Enabled = True ORDER BY SynchronisedDevice.ALIAS; ";
            reader = command.ExecuteReader();
 

            int i = 0;
            int GridRow = 0;

            while (reader.Read())
            {
                i++;


                if (initialload || ((i >=(CurrentPage * DeviceRowsDisplayed)) && (i <= (CurrentPage * DeviceRowsDisplayed + DeviceRowsDisplayed))))
                {
              
                    //dataGridView1.Rows[2].Cells[3].Value = true;
                    String DeviceName = reader[0].ToString();
                    String Alias = reader[1].ToString();
                    String TeamName = reader[2].ToString();
                    String Score = Convert.ToString(reader.GetInt32(3));
                    Boolean Ping = reader.GetBoolean(4);
                    String StatusBar = reader[5].ToString();
                    String SATRID = Convert.ToString(reader.GetInt32(6));
                    String badgefilename = reader[7].ToString();
                    Image image;
                    //LOAD THE IMAGE
                    string ImageFileName = directoryName + @"\Badges\" + Program.GenreFolder + @"\" + badgefilename;
                    if (!File.Exists(ImageFileName))//
                        ImageFileName = directoryName + @"\Badges\" + badgefilename;
                    image = Image.FromFile(ImageFileName);

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
                    ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + pingfile;
                    if (!File.Exists(ImageFileName))//
                        ImageFileName = directoryName + @"\Images\" + pingfile;
                    PingImage = Image.FromFile(ImageFileName);

                    if (SATRID.Length > 0)
                    {
                        DeviceGrid.Rows.Add(image, DeviceName, Alias, TeamName, Score, "",PingImage, "",StatusBar, SATRID);
                        if (!(this.DeviceGrid == null))
                            this.DeviceGrid.CurrentCell.Selected = false;
                        if (!(GridRow % 2 == 0))
                        {
                            DeviceGrid.Rows[GridRow].Cells[6].Style.BackColor = Color.Silver;
                        }
                        GridRow++;
                    }
                }
 
            }
            reader.Close();
            connection.Close();

            
            this.DeviceGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (i = 0; i < DeviceGrid.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    DeviceGrid.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                    DeviceGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    DeviceGrid.Rows[i].DefaultCellStyle.BackColor = Color.Silver;
                    DeviceGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                if (i % 2 == 0)
                    DeviceGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                else
                    DeviceGrid.Rows[i].Cells[6].Style.BackColor = Color.Silver;
            }

/*            for (i = 0; i < DeviceGrid.Rows.Count; i++)
            {
                DeviceGrid.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                DeviceGrid.Rows[i].DefaultCellStyle.ForeColor = Color.White;

            }
        */
            foreach (DataGridViewRow row in DeviceGrid.Rows)
            {
                row.Height = (int)(70*this.Height / Program.ScreenHeight);
            }
            ((DataGridViewImageColumn)DeviceGrid.Columns[0]).ImageLayout = DataGridViewImageCellLayout.Stretch;
            if (initialload)
            {
                DeviceRowsDisplayed = DeviceGrid.DisplayedRowCount(false);
                initialload = false;
            }

            if (DeviceRowsDisplayed < DeviceRecords) //Need to page through
            {
                CurrentPage++;
                if (DeviceRowsDisplayed * CurrentPage > DeviceRecords)
                {
                    CurrentPage = 0;
                }
            }
            

        }

        private void DataviewTransparent()
        {
            for (int x = 0; x < DeviceGrid.RowCount; x++)
                for (int y = 0; y < DeviceGrid.ColumnCount; y++)
                {
                    DeviceGrid.Rows[x].Cells[y].Style.BackColor = System.Drawing.Color.Transparent;
                }
        }

        protected override void ResizeScreen()
        {

            int x = (int)(49 * this.Width / Program.ScreenWidth);
            int y = (int)(313 * this.Height / Program.ScreenHeight);

            ControlDataViewPanel.Location = new Point(x, y);
            //2476, 873
            // this.Size = new Size(1366, 768);


            x = (int)(2476 * this.Width / Program.ScreenWidth);
            y = (int)(873 * this.Height / Program.ScreenHeight);
            ControlDataViewPanel.Size = new Size(x, y);
            DeviceGrid.Size = new Size(x, y);

            int fontheight = (int)(25 * this.Height / Program.ScreenHeight);
            DeviceGrid.DefaultCellStyle.Font = new Font(DeviceGrid.Font.FontFamily, fontheight);
            DeviceGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Silver;

            //resize the columnns
            for (int i = 0; i < DeviceGrid.Columns.Count; i++)
            {
                int colw = colwidths[i];//GET SAVED WIDTHS
                DeviceGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colw = (int)(colw * this.Width / Program.ScreenWidth);
                DeviceGrid.Columns[i].MinimumWidth = colw;
                DeviceGrid.Columns[i].Width = colw;
                DeviceGrid.Columns[i].DefaultCellStyle.Font = new Font(DeviceGrid.Font.FontFamily, fontheight);
            }
        }

        private void DeviceMonitorForm_Load(object sender, EventArgs e)
        {
            SaveColumnWidths();
            ResizeScreen();
            RefreshGrid();
            CheckTestMode();
            SetTimeLeftVisibility(false);
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
        private void DeviceGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeviceMonitorForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DeviceMonitorForm_Enter(object sender, EventArgs e)
        {
           // DataviewTransparent();

        }

        private void DeviceMonitorForm_Click(object sender, EventArgs e)
        {
          //  DataviewTransparent();
        }




        protected override void RefrehGridTimer_Tick(object sender, EventArgs e)
        {
            const int maxdevices = 2017;
            int[] DeviceList = new int[maxdevices];
            int i = 0;
            if (SynchInProgress)
                StatusText.Text = "Sync ALready in progress";
            else
            {
                StatusText.Text = "Refresh Grid";
                TestBox.Text = "";
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
                    command.CommandText = "UPDATE SynchronisedDevice SET Ping = false; ";
                    command.ExecuteNonQuery();

                    //PING EACH DEVICE EXCEPT GAMERS
                    command.CommandText = "SELECT SATR_Unit_ID FROM SynchronisedDevice WHERE NOT Device_Role_Code = 1  AND SynchronisedDevice.Enabled = True ORDER BY SATR_Unit_ID; ";
                    OleDbDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            DeviceList[i] = reader.GetInt32(0);
                            i++;
                        }
                        reader.Close();
                        connection.Close();

                        for (int j=0;j<i;j++)
                        {
                            int SATRID = DeviceList[j];  
                            StatusText.Text = "Monitor Device " + Convert.ToString(SATRID);
                            TestBox.Text += Convert.ToString(SATRID) + ":";
                            //Monitor Details

                            Program.SendRadioPacket(Program.DBLevel, 26, Program.ScoreboardBattleCode, SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");
                            StatusText.Text = "Monitor Sent ";
                            EnableWaitTimer();

                            while (SynchInProgress) { Application.DoEvents(); }//wait for the timer to complete 200ms for a response before sending next request
                            StatusText.Text = "Wait Complete ";
                            if (TestResponseChk.Checked)//DELETE IN PRODUCTION
                            {
                                StatusText.Text = "Respond " + Convert.ToString(SATRID);
                                TestDeviceResponse(SATRID);
                                EnableWaitTimer();
                                while (SynchInProgress)
                                { //wait for the timer to complete 200ms before generating a test response

                                    Application.DoEvents();
                                }
                            }
                        }
                    }
                    catch
                    {
                        StatusText.Text = "Reader Closed";
                    }
                    finally
                    {
                      
                        RefreshGrid();
                        StatusText.Text = "Refresh Complete";
                    }
                    Cursor.Current = Cursors.Default;

                }
            }
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

                switch (DeviceRoleCode)
                {

                    case 1: Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode, DeviceID, Program.ScoreboardID, TeamCode, CurrentHP, Hits, Kills, ShotsFired, Deaths, Score, EmulationCode, DeviceRoleCode, StatusBar); break;
                    case 4:
                    case 10: Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode, DeviceID, Program.ScoreboardID, TeamCode, 0, 0, 0, Score, 0, 0, 0, DeviceRoleCode, StatusBar); break;
                    default: Program.ReceiveRadioPacket(27, Program.ScoreboardBattleCode, DeviceID, Program.ScoreboardID, TeamCode, Score, 0, 0, 0, 0, 0, 0, DeviceRoleCode, StatusBar); break;

                }
                TestBox.Text += Convert.ToString(Score) + ", ";
            }
            reader.Close();
            connection.Close();
        }

        private void DeviceGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TestResponseChk_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    
}
