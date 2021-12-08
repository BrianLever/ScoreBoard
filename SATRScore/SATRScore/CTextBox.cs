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
    public partial class CTextBox : Form
    {
        public string NewString;
        public string TitleLabel;
        public int maxcharacters = 8;
        public string DBConnection;
        private OleDbConnection connection = new OleDbConnection();
        public Int16 TeamCode;
        public int SATRID;
        public bool DeviceEnabled;

        public CTextBox()
        {
            InitializeComponent();
        }
        private void OpenConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection.ConnectionString = DBConnection;
                connection.Open();
            }
        }

        private void CTextBox_Load(object sender, EventArgs e)
        {
            textBox1.Text = NewString;
            label1.Text = TitleLabel;
            textBox1.MaxLength = maxcharacters;

            TeamList.Items.Clear();
            OpenConnection();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT TeamsByGenre.Team_Name " +
            "FROM Config, TeamsByGenre WHERE Config.Genre_Code = TeamsByGenre.Genre_Code" +
            " ORDER BY Team_Code;";
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                TeamList.Items.Add(reader[0].ToString());
            }

            reader.Close();
            command.CommandText = "SELECT Team_Code " +
           "FROM SynchronisedDevice WHERE SATR_Unit_ID =" + Convert.ToString(SATRID);
         
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                TeamCode = (Int16)reader.GetInt32(0);
            }
            else
                TeamCode = 0;
            reader.Close();
            connection.Close();
            TeamList.SetSelected(TeamCode, true);
            EnabledChk.Checked = DeviceEnabled;
            //SynchronisedDevice.SATR_Unit_ID
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            NewString = textBox1.Text ;
            TeamCode = (Int16)TeamList.SelectedIndex;
            DeviceEnabled = EnabledChk.Checked;
        }
    }
}
