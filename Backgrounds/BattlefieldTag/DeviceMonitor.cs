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

namespace SATRScoreDisplay
{
    public partial class MonitorOnlyForm : Form
    {
        public bool CloseApp = false;
        private OleDbConnection connection = new OleDbConnection();
        public int FormMissionCode;


        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        ConfigData cd = new ConfigData();

        public MonitorOnlyForm()
        {
            InitializeComponent();
        }

        private void LoadTimeButtons()
        {
            System.Environment.CurrentDirectory = "\\SATRScore";
            string directoryName = System.Environment.CurrentDirectory;
            //SEARCH GENRE FOLDER FIRST
            string ImageFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\TimeBtn.png";
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Images\TimeBtn.png";
            //MessageBox.Show(ImageFileName);
            GameTimeBtn.BackgroundImage = Bitmap.FromFile(ImageFileName);
            TimeofDayBtn.BackgroundImage = Bitmap.FromFile(ImageFileName);

        }

        private void MonitorOnlyForm_Load(object sender, EventArgs e)
        {
            //LOAD BACKGROUND IMAGE
            System.Environment.CurrentDirectory = "\\SATRScore";
            string directoryName = System.Environment.CurrentDirectory;
            string ImageFileName = directoryName + @"\Backgrounds\" + Program.GenreFolder + @"\DeviceMonitor.png";
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Backgrounds\DeviceMonitor.png";
            this.BackgroundImage = Image.FromFile(ImageFileName);
            LoadTimeButtons();
        }

        private void CloseAppPicture_Click(object sender, EventArgs e)
        {
            CloseApp = true;
            this.Close();
        }

        private void CheckMissionChangeTimer_Tick(object sender, EventArgs e)
        {
            
            int NewMissionCode = 0;
            NewMissionCode = cd.CheckMission();
            if (FormMissionCode != NewMissionCode)
            { //Mission has changed and therefore load a new scoreboard
                Program.MissionCode = NewMissionCode;
                this.Close();
            }
        }

        private void MonitorOnlyForm_MouseClick(object sender, MouseEventArgs e)
        {
          
        }

        private void MonitorOnlyForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void MonitorOnlyForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void MonitorOnlyForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void SecondTickTimer_Tick(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            TimeofDayBtn.Text = localDate.ToLongTimeString();

            Program.TimeLeftInGame -= TimeSpan.FromSeconds(1);
            if (Program.TimeLeftInGame.CompareTo(TimeSpan.Zero) < 0)
                Program.TimeLeftInGame = TimeSpan.Zero;
            
            GameTimeBtn.Text = Program.TimeLeftInGame.ToString();

        }

        private void GameTimeSetBtn_Click(object sender, EventArgs e)
        {
            Program.ReceiveRadioPacket(17, cd.BattleCode, 2000, 0, 0, (15 * 60), 0, 0, 0, 0, 0, 0, 0, "CONTROL1");
        }
    }
}
