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

        public MonitorOnlyForm()
        {
            InitializeComponent();
        }

        private void MonitorOnlyForm_Load(object sender, EventArgs e)
        {

        }

        private void CloseAppPicture_Click(object sender, EventArgs e)
        {
            CloseApp = true;
            this.Close();
        }

        private void CheckMissionChangeTimer_Tick(object sender, EventArgs e)
        {
            ConfigData cd = new ConfigData();
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
    }
}
