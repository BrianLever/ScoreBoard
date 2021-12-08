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
    public partial class MissionSelectForm : Form
    {
        public string DBConnection;
        public bool closeapp = false;
        private OleDbConnection connection = new OleDbConnection();
        public string GenreFolder;
        public Int16 GenreCode;
        public int MissionCode = 0;
        public Image BackButtonImage;

        int[] MissionCodes = new int[12];
        string[] MissionNames = new string[12];
        PictureBox[] MissionBoxes = new PictureBox[12];
        Label[] MissionLabels = new Label[12];

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public MissionSelectForm()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MissionBox_Click(object sender, System.EventArgs e)
        {
            // Add event handler code here.  
            String name = null;
            if (sender is PictureBox)
            {
                name = (sender as PictureBox).Name;
            }
            else if (sender is Label)
            {
                name = (sender as Label).Name;
            }
            else
                name = "Not picture box";
         //   MessageBox.Show(name);
            MissionCode = Convert.ToInt32(name);
            //MissionCodes
            connection.ConnectionString = DBConnection;
            connection.Open();
           OleDbCommand command = new OleDbCommand();
           command.Connection = connection;
           string query = "UPDATE Config SET Mission_Code = " + Convert.ToString(MissionCode) + ";";
        //   MessageBox.Show(query);
           command.CommandText = query;
           command.ExecuteNonQuery();
           connection.Close();

            this.Close();


        }

        private void MissionSelectForm_Load(object sender, EventArgs e)
        {
            
            //LOAD THE RIGHT IMAGE FOR THE BACK BUTTON
            BackBtn.BackgroundImage = BackButtonImage;


            //LOAD BACKGROUND IMAGE
            
            string directoryName = Program.rootdirectory;
            string ImageFileName = directoryName + @"\Backgrounds\" + GenreFolder+@"\SelectMission.png";
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Backgrounds\SelectMission.png";
            this.BackgroundImage = Image.FromFile(ImageFileName);

            //LOAD MISSION BADGES
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            List<Control> listControls = new List<Control>();

            foreach (Control control in MissionsPanel.Controls)
            {
                listControls.Add(control);
            }

            foreach (Control control in listControls)
            {
                MissionsPanel.Controls.Remove(control);
                control.Dispose();
            }

            int missions = 0;
            int missionindex = 0;

         
            command.CommandText = "SELECT * FROM MissionByGenre WHERE Genre_Code = " + Convert.ToString(GenreCode) + ";";
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read() && missions < 11)
            {
                missions++;
                MissionCodes[missions] = reader.GetInt32(0);
                MissionNames[missions] = reader[2].ToString();
            }
            reader.Close();
            //MissionText.Text = "";
            while (missionindex <= missions && missionindex < 11)
            {
                missionindex++;
                command.CommandText = "SELECT Badge_FileName FROM Mission WHERE Mission_Code = " + Convert.ToString(MissionCodes[missionindex]) + ";";
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string badgefilename = reader[0].ToString();
                    MissionLabels[missionindex] = new Label();
                    MissionBoxes[missionindex] = new PictureBox();
                    MissionBoxes[missionindex].Height = 100;
                    MissionBoxes[missionindex].Width = MissionBoxes[missionindex].Height;
                    
                    ImageFileName = directoryName + @"\Badges\" + GenreFolder + @"\" + badgefilename;
                    if (!File.Exists(ImageFileName))//
                        ImageFileName = directoryName + @"\Badges\" + badgefilename;
                    if (File.Exists(ImageFileName))
                    {
                        MissionBoxes[missionindex].SizeMode = PictureBoxSizeMode.StretchImage;
                        MissionBoxes[missionindex].ImageLocation = ImageFileName;
                        MissionBoxes[missionindex].Click += new EventHandler(MissionBox_Click);
                        MissionBoxes[missionindex].Name = Convert.ToString(MissionCodes[missionindex]);
                     //   MissionText.Text +=  MissionBoxes[missionindex].Name +", ";
                        MissionsPanel.Controls.Add(MissionBoxes[missionindex]);

                    }
                    //lab.Font = new Font("Arial", 20);
                    MissionLabels[missionindex].AutoSize = false;
                    MissionLabels[missionindex].Font = new Font(FontFamily.GenericSansSerif,
                    20F, FontStyle.Bold);
                    //Roboto, 14.25pt, style=Bold
                    MissionLabels[missionindex].Text = MissionNames[missionindex];
                    MissionLabels[missionindex].MinimumSize = new Size(280, 100);
                    MissionLabels[missionindex].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    MissionLabels[missionindex].Click += new EventHandler(MissionBox_Click);
                    MissionLabels[missionindex].Name = Convert.ToString(MissionCodes[missionindex]);
                 //   MissionText.Text += MissionBoxes[missionindex].Name + ", ";
                    MissionsPanel.Controls.Add(MissionLabels[missionindex]);
                }
                reader.Close();

            }
            connection.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MissionsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CloseAppPicture_Click(object sender, EventArgs e)
        {
            ConfigData cd = new ConfigData();
            closeapp = cd.closeappcheck();
            if (closeapp)
                this.Close();
        }

        private void MissionSelectForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void MissionSelectForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void MissionSelectForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
