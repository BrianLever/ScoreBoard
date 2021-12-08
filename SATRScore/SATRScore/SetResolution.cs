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

namespace SATRScore
{
    public partial class SetResForm : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        int MonitorsFound = 0;
        const int maxscreens = 5;
        int[] Widths = new int[maxscreens];
        int[] Heights = new int[maxscreens];

        public SetResForm()
        {
            InitializeComponent();
        }

        private void SetResForm_Load(object sender, EventArgs e)
        {
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT Scoreboards_Width,Scoreboards_Height FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                x_Res.Text = Convert.ToString(reader.GetInt32(0));
                y_Res.Text = Convert.ToString(reader.GetInt32(1));
            }
            reader.Close();
            connection.Close();
            LoadMonitorList();
        }

        private void LoadMonitorList()
        {
   
            MonitorList.Items.Clear();


            foreach (Screen screen in Screen.AllScreens)
            {
                if (MonitorsFound < maxscreens)
                {
                    Widths[MonitorsFound] = screen.Bounds.Width;
                    Heights[MonitorsFound] = screen.Bounds.Height;
                    MonitorList.Items.Add(Widths[MonitorsFound].ToString() + " , " + Heights[MonitorsFound].ToString());
                }
                MonitorsFound++;


            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {

            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Config SET Scoreboards_Width = " + x_Res.Text + ", Scoreboards_Height = " + y_Res.Text + ";";
            command.ExecuteNonQuery();
            connection.Close();
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetCurrentBtn_Click(object sender, EventArgs e)
        {
            x_Res.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
            y_Res.Text = Screen.PrimaryScreen.Bounds.Height.ToString();
        }

        private void x_Res_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void y_Res_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void MaxResBtn_Click(object sender, EventArgs e)
        {
            x_Res.Text = Convert.ToString(2560);
            y_Res.Text = Convert.ToString(1440);
        }

        private void LapTopBtn_Click(object sender, EventArgs e)
        {
            x_Res.Text = Convert.ToString(1366);
            y_Res.Text = Convert.ToString(768);
        }

        private void MonitorList_DoubleClick(object sender, EventArgs e)
        {
            x_Res.Text = Widths[MonitorList.SelectedIndex].ToString();
            y_Res.Text = Heights[MonitorList.SelectedIndex].ToString();

        }
    }
}
