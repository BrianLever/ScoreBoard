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
    public partial class ConfigForm : Form
    {
        public string DBConnection;
        private OleDbConnection connection = new OleDbConnection();
        public Int32 GenreCode;
        private Int32[] GenreCodes = new Int32[10];
        bool GenresLoaded = false;
        bool BattlesLoaded = false;
        bool LanguagesLoaded = false;
        private int LanguageCode, UpdateFrequency;
        private Int32[] LanguageCodes = new Int32[30];
        public Image BackButtonImage;
        public bool closeapp = false;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public ConfigForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UpdateTheFrequency();
            this.Close();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            UpdateTheFrequency();
            this.Close();
        }

        private void HideAllControls(int screenno)
        {
            if (screenno != 1)
            {
                GenreList.Visible = false;
                GenreLabel.Visible = false;
                GenrePanel.Visible = false;
                LanguageList.Visible = false;
            }
            
            if (screenno != 2)
                BattlePanel.Visible = false;
            if (screenno != 3)
            {
                LanguagePanel.Visible = false;
                LanguageList.Visible = false;
            }
            if (screenno != 4)
                FrequencyPanel.Visible = false;
        }

        private void GenreBtn_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show("Show Genre");
            HideAllControls(1);
            GenrePanel.Visible = true;
            GenreList.Visible = true;
            GenreLabel.Visible = true;
            LoadButtonImages(1);
        }

        private void LoadButtonImages(int CurrentButton)
        { //2 battle 3 Voice Over 4 Update Frequency
            
            string directoryName = Program.rootdirectory;
            string ImageFileName;

            if (CurrentButton == 1)
                  ImageFileName = directoryName + @"\Images\GenreWhiteBtn.png";
            else
                ImageFileName = directoryName + @"\Images\GenreGreenBtn.png";
            if (File.Exists(ImageFileName))
                GenreBtn.BackgroundImage = Bitmap.FromFile(ImageFileName);

            if (CurrentButton == 2)
                ImageFileName = directoryName + @"\Images\BattleWhiteBtn.png";
            else
                ImageFileName = directoryName + @"\Images\BattleGreenBtn.png";
            if (File.Exists(ImageFileName))
                BattleBtn.BackgroundImage = Bitmap.FromFile(ImageFileName);

            if (CurrentButton == 3)
                ImageFileName = directoryName + @"\Images\VoiceOverWhiteBtn.png";
            else
                ImageFileName = directoryName + @"\Images\VoiceOverGreenBtn.png";
            if (File.Exists(ImageFileName))
                VoiceOverBtn.BackgroundImage = Bitmap.FromFile(ImageFileName);

            if (CurrentButton == 4)
                ImageFileName = directoryName + @"\Images\FrequencyWhiteBtn.png";
            else
                ImageFileName = directoryName + @"\Images\FrequencyGreenBtn.png";
            if (File.Exists(ImageFileName))
                UpdateFrequencyButton.BackgroundImage = Bitmap.FromFile(ImageFileName);
        }

        private void BattleBtn_Click(object sender, EventArgs e)
        {
            HideAllControls(2);
            BattleListBox.SetSelected(Program.ScoreboardBattleCode, true);
            BattlePicture.Visible = true;
            BattlePanel.Visible = true;
            BattleListBox.Visible = true;
            LoadButtonImages(2);
            //MessageBox.Show("Battle");
        }

        private void VoiceOverBtn_Click(object sender, EventArgs e)
        {
            LoadButtonImages(3);
            HideAllControls(3);
            LanguagePanel.Visible = true;
            LanguageList.Visible = true;
          //  MessageBox.Show("Voice Over");
        }

        private void UpdateFrequencyButton_Click(object sender, EventArgs e)
        {

            HideAllControls(4);
            LoadButtonImages(4);
            FrequencyPanel.Visible = true;
        }
        
        private void LanguageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LanguagesLoaded)
            {
                Int32 idx = LanguageList.Items.IndexOf(LanguageList.SelectedItem);
                LanguageCode = LanguageCodes[idx];
                connection.ConnectionString = DBConnection;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "UPDATE Config SET Language_Code = " + Convert.ToString(LanguageCode) + ";";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Language Updated");
            }
        }

        private void LoadFromDB()
        {
           // MessageBox.Show("Load DB");
            GenreList.Items.Clear();
            int GenreIndex = 0;

            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT Genre_Code, Genre FROM Genre;";
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                GenreCodes[GenreIndex] = reader.GetInt32(0);
                string GenreString = reader[1].ToString();
                GenreList.Items.Add(GenreString);
                if (GenreCodes[GenreIndex] == GenreCode)
                {
                    GenreList.SetSelected(GenreIndex, true);
                    //        MessageBox.Show(GenreString + " SELECTED");
                }
                //else
                //    MessageBox.Show(GenreString + " NOT SELECTED");
                GenreIndex++;
            }
            reader.Close();

            //GET CONFIG
            command.CommandText = "SELECT Battle_Code, Language_Code, Update_Frequency, Radio_Repeater FROM Config;";
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                Program.ScoreboardBattleCode = reader.GetInt16(0);
                BattlesLoaded = true;
                LanguageCode = reader.GetInt32(1);
                UpdateFrequency = reader.GetInt32(2);
                if (UpdateFrequency < 10)
                    UpdateFrequency = 10;
                FrequencySpin.Value = UpdateFrequency;
                RadioRepeaterChk.Checked = reader.GetBoolean(3);

            }
            else
                MessageBox.Show("Config table not found");
            reader.Close();

            //Load Languages
            LanguageList.Items.Clear();
            command.CommandText = "SELECT [Language_Code], [Languages].[Language_Name] FROM [Languages];";
            reader = command.ExecuteReader();
            int LanguageIndex = 0;

            while (reader.Read())
            {

                LanguageCodes[LanguageIndex] = reader.GetInt32(0);
                string Languagestring = reader[1].ToString();
                LanguageList.Items.Add(Languagestring);
                if (LanguageCodes[LanguageIndex] == LanguageCode)
                {
                    LanguageList.SetSelected(LanguageIndex, true);
                    //        MessageBox.Show(GenreString + " SELECTED");
                }
                //else
                //    MessageBox.Show(GenreString + " NOT SELECTED");
                LanguageIndex++;
            }
            reader.Close();
            LanguagesLoaded = true;


            connection.Close();
            GenresLoaded = true;
        }

        private void ConfigForm_Enter(object sender, EventArgs e)
        {
            LoadFromDB();
            LoadButtonImages(0);//make all buttons green
        }

        private void BattleListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (BattlesLoaded)
            {
                if (Program.ScoreboardBattleCode != Convert.ToInt16(BattleListBox.Items.IndexOf(BattleListBox.SelectedItem)))
                {
                    Program.ScoreboardBattleCode = Convert.ToInt16(BattleListBox.Items.IndexOf(BattleListBox.SelectedItem));
                    connection.ConnectionString = DBConnection;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "UPDATE Config SET Battle_Code = " + Convert.ToString(Program.ScoreboardBattleCode) + ";";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Battle updated to " + Convert.ToString(Program.ScoreboardBattleCode + 1));
                }
            }
        }

        private void UpdateTheFrequency()
        {
            if (UpdateFrequency != Decimal.ToInt32(FrequencySpin.Value))
            { UpdateFrequency = Decimal.ToInt32(FrequencySpin.Value);
                // MessageBox.Show("Update Frequency " + Convert.ToString(UpdateFrequency));
                connection.ConnectionString = DBConnection;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "UPDATE Config SET Update_Frequency = " + Convert.ToString(UpdateFrequency) + ";";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Frequency changed to " + Convert.ToString(UpdateFrequency));
            }

        }
        private void FrequencySpin_Leave(object sender, EventArgs e)
        {
            UpdateTheFrequency();
        }

        private void GenreList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (GenresLoaded)
            {
                Int32 idx = GenreList.Items.IndexOf(GenreList.SelectedItem);
                if (GenreCode != GenreCodes[idx])
                {
                    GenreCode = GenreCodes[idx];
                    connection.ConnectionString = DBConnection;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "UPDATE Config SET Genre_Code = " + Convert.ToString(GenreCode) + ";";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Genre Changed ");
                }
            }
        }

        private void ScreenResPictureBox_Click(object sender, EventArgs e)
        {
            SetResForm sr = new SetResForm();
            sr.ShowDialog();
        }

        private void CloseAppPicture_Click(object sender, EventArgs e)
        {
            ConfigData cd = new ConfigData();
            closeapp = cd.closeappcheck();
            if (closeapp)
                this.Close();
        }

        private void ConfigForm_MouseDown(object sender, MouseEventArgs e)
        {
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }

        }

        private void ConfigForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void ConfigForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void RadioRepeaterChk_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void RadioRepeaterChk_Click(object sender, EventArgs e)
        {
            connection.Close();
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query;
            if (RadioRepeaterChk.Checked)
                query = "UPDATE Config SET Radio_Repeater = True";
            else
                query = "UPDATE Config SET Radio_Repeater = False";
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1366, 768);
            LanguagePanel.Location = new Point(792,214);
            BattlePanel.Location = new Point(792, 214);
            GenrePanel.Location = new Point(792, 214);
            FrequencyPanel.Location = new Point(792, 214);
            LanguageList.Location = new Point(803, 300);
            LoadFromDB();
            //LOAD THE RIGHT IMAGE FOR THE BACK BUTTON
            BackBtn.BackgroundImage = BackButtonImage;
        }
  
    }
}
