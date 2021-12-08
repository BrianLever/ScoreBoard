using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace SATRScore
{
    public partial class ManageDevicesForm : Form
    {
        public string DBConnection;
        private OleDbConnection connection = new OleDbConnection();
        public string GenreFolder;
        public Int16 GenreCode;
        public Image BackButtonImage;
        public bool closeapp = false;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool loadingdevices = false;



        public ManageDevicesForm()
        {

            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void OpenConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection.ConnectionString = DBConnection;
                connection.Open();
            }
        }
        private void LoadDevices()
        {
            loadingdevices = true;
            int DeviceCount = 0;
            int GamerCount = 0;
            DeviceList.Items.Clear();
            OpenConnection();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT SynchronisedDevice.SATR_Unit_ID, SynchronisedDevice.ALIAS, TeamsByGenre.Team_Name, SynchronisedDevice.Device_Role_Code, SynchronisedDevice.Enabled " +
            "FROM SynchronisedDevice INNER JOIN(Config INNER JOIN TeamsByGenre ON Config.Genre_Code = TeamsByGenre.Genre_Code) ON SynchronisedDevice.Team_Code = TeamsByGenre.Team_Code " +
            "ORDER BY SynchronisedDevice.ALIAS;";
            OleDbDataReader reader = command.ExecuteReader();
            string ALIAS;
            while (reader.Read())
            {
                //GET THE DEVICE ROLE FROM
                OleDbCommand Devicecommand = new OleDbCommand();
                Devicecommand.Connection = connection;
                Devicecommand.CommandText = "SELECT Device_Name FROM DeviceRole WHERE Device_Role_Code = " + Convert.ToString(reader.GetInt32(3)) + " ;";
                OleDbDataReader DeviceRoleReader = Devicecommand.ExecuteReader();
                string DeviceRoleName;
                if (!DeviceRoleReader.Read())
                    DeviceRoleName = "NONE";
                else
                    DeviceRoleName = DeviceRoleReader[0].ToString();
                // create the subitems to add to the list ROLE and SATRID
                ALIAS = reader[1].ToString();
                string TeamName = reader[2].ToString();
                //   MessageBox.Show(ALIAS);
                ListViewItem item = new ListViewItem(ALIAS);
                item.Checked = reader.GetBoolean(4);
                item.SubItems.Add(DeviceRoleName + "(" + TeamName + ")");
                item.SubItems.Add(Convert.ToString(reader.GetInt32(0)));
                if (reader.GetInt32(3) == 1)//Gamer
                    GamerCount++;
                else
                    DeviceCount++;
                DeviceList.Items.Add(item);
            }
            reader.Close();

            connection.Close();
            DeviceCntLabel.Text = "DEVICES: " + Convert.ToString(DeviceCount);
            GamersLabel.Text = "GAMERS: " + Convert.ToString(GamerCount);
            loadingdevices = false;
        }

        private void ManageDevicesForm_Load(object sender, EventArgs e)
        {
            //LOAD THE RIGHT IMAGE FOR THE BACK BUTTON
            BackBtn.BackgroundImage = BackButtonImage;
            //LOAD BACKGROUND IMAGE
            System.Environment.CurrentDirectory = Program.rootdirectory;
            string directoryName = System.Environment.CurrentDirectory;
            string ImageFileName = directoryName + @"\Backgrounds\" + GenreFolder + @"\ManageDevices.png";
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Backgrounds\ManageDevices.png";
            this.BackgroundImage = Image.FromFile(ImageFileName);

            //DeviceList

            /*    ImageFileName = directoryName + @"\Backgrounds\" + GenreFolder + @"\ManageDevicesList.png";
                if (!File.Exists(ImageFileName))
                    ImageFileName = directoryName + @"\Backgrounds\ManageDevicesList.png";
                if (File.Exists(ImageFileName))
                    DeviceList.BackgroundImage = Image.FromFile(ImageFileName);
                else
                    DeviceList.BackgroundImage = null;
               */
            DeviceList.CheckBoxes = true;
            LoadDevices();
        }


        private void DeviceList_KeyDown(object sender, KeyEventArgs e)
        {

            int selectionindex = -1;
            if (DeviceList.SelectedIndices.Count > 0)
                selectionindex = DeviceList.SelectedIndices[0];
            if (selectionindex > -1)
            {
                Int32 SATRID = Convert.ToInt32(DeviceList.SelectedItems[0].SubItems[2].Text);
                if (e.KeyCode == Keys.Delete)
                {
                    //CONFIRM DELETE

                    DialogResult result1 = MessageBox.Show("Delete Device", "Manage Synchronised Devices",
     MessageBoxButtons.YesNo);
                    if (result1 == DialogResult.Yes)
                    {
                        //DELETE FROM DATABASE

                        OpenConnection();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "DELETE FROM SynchronisedDevice WHERE SATR_Unit_ID = " + Convert.ToString(SATRID) + ";";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        connection.Close();

                        //    DeviceList.SelectedItems[0].Remove();
                        LoadDevices();
                    }
                }
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.rf.rfPortOpen(false);
            SynchroniseForm sf = new SynchroniseForm();
            sf.DBConnection = DBConnection;
            sf.GenreFolder = GenreFolder;
            sf.GenreCode = GenreCode;
            sf.BackButtonImage = BackButtonImage;
            sf.ShowDialog();
            Program.rf.rfPortClose();
            closeapp = sf.closeapp;
            if (closeapp)
                this.Close();
            else
            if (sf.GoHome)
                this.Close();
            else
            {
                LoadDevices();
                this.Show();
            }

        }

        private void DeviceList_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            int selectionindex = -1;
            if (DeviceList.SelectedIndices.Count > 0)
                selectionindex = DeviceList.SelectedIndices[0];
            if (selectionindex > -1)
            {
                CTextBox tb = new CTextBox();
                tb.NewString = DeviceList.SelectedItems[0].SubItems[0].Text;
                tb.DeviceEnabled = !DeviceList.SelectedItems[0].Checked;
                tb.DBConnection = DBConnection;
                tb.TitleLabel = "New ALIAS/TEAM";
                tb.maxcharacters = 8;
                Int32 SATRID = 0;
                try
                {
                    SATRID = Convert.ToInt32(DeviceList.SelectedItems[0].SubItems[2].Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                }
                tb.SATRID = SATRID;
                tb.ShowDialog();

                DialogResult result1 = tb.DialogResult;
                if (result1 == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    OpenConnection();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "UPDATE SynchronisedDevice SET ALIAS = '" + tb.NewString + "' ,Team_Code = " + tb.TeamCode + ", Enabled = "+Convert.ToString(tb.DeviceEnabled)+" WHERE SATR_Unit_ID = " + Convert.ToString(SATRID) + ";";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    connection.Close();
                    //SEND RADIO PACKET TO UPDATE THE DEVICE ALIAS
                    Program.rf.rfPortOpen(false);
                    Program.SendRadioPacket(Program.DBLevel, 55, Program.ScoreboardBattleCode, SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, tb.NewString);
                    Program.SendRadioPacket(Program.DBLevel, 58, Program.ScoreboardBattleCode, SATRID, tb.TeamCode, 2, 0, 0, 0, 0, 0, 0, 0, "");
                    LoadDevices();
                    Program.rf.rfPortClose();
                    LoadDevices();
                    Cursor.Current = Cursors.Default;

                }
            }


        }

        private void DeleteAllBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete all Devices?", "DELETE DEVICES", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                OpenConnection();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "DELETE FROM SynchronisedDevice;";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                LoadDevices();
            }
        }
        struct Device
        {
            public Int32 SATRID;
            public Int32 DeviceRoleCode;
        }

        private void RandonBtnAllocation_Click(object sender, EventArgs e)
        {
            //const int maxdevices = 2047;
            const int maxtries = 30;


            int DeviceIndex = 0;
            Device[] DeviceList = new Device[Program.ScoreboardID];

            DialogResult dialogResult = MessageBox.Show("Assign random ALIASES", "RANDOM ALIAS ASSIGNMENT", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                OpenConnection();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                command.CommandText = "SELECT Count(*) FROM SynchronisedDevice;";
                OleDbDataReader countreader = command.ExecuteReader();
                int Devices = 1;
                if (countreader.Read())
                    Devices = countreader.GetInt32(0);

                countreader.Close();

                command.CommandText = "SELECT SATR_Unit_ID, Device_Role_Code FROM SynchronisedDevice;";
                OleDbDataReader DeviceListReader = command.ExecuteReader();
                while (DeviceListReader.Read())
                {
                    DeviceList[DeviceIndex].SATRID = DeviceListReader.GetInt32(0);
                    DeviceList[DeviceIndex].DeviceRoleCode = DeviceListReader.GetInt32(1);
                    DeviceIndex++;
                }
                int tries = 0;
                int RecordsProcessed = 0;
                DeviceListReader.Close();
                for (DeviceIndex = 0; DeviceIndex < Devices; DeviceIndex++)//FOR EACH DEVICE
                {
                    OpenConnection();
                    RecordsProcessed++;
                    progressBar1.Value = (int)(RecordsProcessed * 100 / Devices);

                    tries = 0;
                    //COUNT BASED ON DEVICE ROLE THE NUMBER OF ALIASES THAT SUIT
                    OpenConnection();
                    OleDbCommand loopcommand = new OleDbCommand();
                    loopcommand.Connection = connection;
                    loopcommand.CommandText = "SELECT COUNT(*) FROM ALIAS WHERE Device_Role_Code = " + Convert.ToString(DeviceList[DeviceIndex].DeviceRoleCode) + ";";
                    OleDbDataReader reader = loopcommand.ExecuteReader();
                    int SuitableAliases = 0;
                    if (reader.Read())
                        SuitableAliases = reader.GetInt32(0);

                    while (tries < maxtries)
                    {
                        tries++;
                        reader.Close();
                        if (SuitableAliases == 0)
                        {
                            tries = maxtries;//Give Up
                            MessageBox.Show("No Suitable Aliases");
                            break;
                        }
                        else
                        {
                            //FIND A SUITABLE RANDOM ALIAS
                            int recno = Program.RandomNumber(1, SuitableAliases);
                            loopcommand.CommandText = "SELECT Alias_Name FROM ALIAS WHERE Device_Role_Code = " + Convert.ToString(DeviceList[DeviceIndex].DeviceRoleCode) + ";";
                            reader = loopcommand.ExecuteReader();
                            int i = 0;
                            string AliasName = "";
                            while (reader.Read() && i < recno)
                            {
                                i++;
                                AliasName = reader[0].ToString();
                            }
                            reader.Close();

                            //CHECK TO ENSURE NOT ALREADY USED, IF USED, TRY AGAIN
                            loopcommand.CommandText = "SELECT COUNT(*) FROM SynchronisedDevice WHERE ALIAS = '" + AliasName + "';";
                            reader = loopcommand.ExecuteReader();
                            reader.Read();
                            if (reader.GetInt32(0) == 0)//UPDATE THE DEVICE ALIAS.
                            {
                                reader.Close();

                                loopcommand.CommandText = "UPDATE SynchronisedDevice SET ALIAS = '" + AliasName + "'  WHERE SATR_Unit_ID = " + Convert.ToString(DeviceList[DeviceIndex].SATRID) + ";";
                                loopcommand.ExecuteNonQuery();
                                //SEND RADIO PACKET TO UPDATE THE DEVICE ALIAS
                                Program.rf.rfPortOpen(false);
                                Program.SendRadioPacket(Program.DBLevel, 55, Program.ScoreboardBattleCode, DeviceList[DeviceIndex].SATRID, 0, 0, 0, 0, 0, 0, 0, 0, 0, AliasName);
                                WaitRF();

                                tries = maxtries;
                                break;
                            }
                            reader.Close();

                        }
                    }
                    reader.Close();
                }

                connection.Close();
                Program.rf.rfPortClose();
                LoadDevices();


            }
            progressBar1.Visible = false;
        }

        private void WaitRF()
        {
            WaitRFTransmissionTimer.Interval = Program.RFWaitTimeMS;
            WaitRFTransmissionTimer.Enabled = true;
            while (WaitRFTransmissionTimer.Enabled) Application.DoEvents();
            LoadDevices();
        }

        private void WaitRFTransmissionTimer_Tick(object sender, EventArgs e)
        {
            WaitRFTransmissionTimer.Enabled = false;
            Program.rf.rfPortClose();
        }

        private void CloseAppPicture_Click(object sender, EventArgs e)
        {
            ConfigData cd = new ConfigData();
            closeapp = cd.closeappcheck();
            if (closeapp)
                this.Close();
        }

        private void ManageDevicesForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void ManageDevicesForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void ManageDevicesForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void ManageDevicesForm_Shown(object sender, EventArgs e)
        {

        }

        private void ManageDevicesForm_Activated(object sender, EventArgs e)
        {
            LoadDevices();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.rf.getAdminConnected() == 0)
                MessageBox.Show("Admin not connected");
            else
                MessageBox.Show("Admin connected");

        }

        private void DeviceList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
        }

        private void DeviceList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!loadingdevices)
            {
                int SATRID = 0;
                try
                {
                  //  this.ListView1.Items[e.Index].SubItems[1].Text);
                    SATRID = Convert.ToInt32(DeviceList.Items[e.Index].SubItems[2].Text);
                    OpenConnection();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query;
                    if (e.CurrentValue != CheckState.Checked)
                    {
                        query = "UPDATE SynchronisedDevice SET Enabled = True WHERE SATR_Unit_ID = " + Convert.ToString(SATRID) + "; ";
                    }
                    else
                    {
                        query = "UPDATE SynchronisedDevice SET Enabled = False  WHERE SATR_Unit_ID = " + Convert.ToString(SATRID) + ";";
                    }
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);

                }
             
            }
        }
    }
    
}
