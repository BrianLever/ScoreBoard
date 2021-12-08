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
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\SATRScore\SATRScore.accdb;
Persist Security Info=False;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Access 2000-2003 (*.mdb)|*.mdb|Access 2007 (*.accdb)|*accdb";
            dialog.Title = "SATRScore database";
            dialog.FilterIndex = 2;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            DialogResult dr = dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dialog.FileName + ";Persist Security Info=False;";
                try
                {
                    connection.Open();
                   // CheckConnection.Text = "Connection Success";
                   //SHOW SPLASH SCREEN

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Database not found");
                Application.Exit();
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                toolStripStatusLabel1.Text= "SELECT * FROM ScoreboardUsers WHERE Alias = '" + txt_UserName.Text + "' AND Password='" + txt_Password.Text + "'";
               // toolStripStatusLabel1.Text = "SELECT * FROM User";
   
                command.CommandText = toolStripStatusLabel1.Text;
               
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                if (count == 1)
                {
                    //                    MessageBox.Show("Login OK");
                    connection.Close();
                    connection.Dispose();
                    this.Hide();
                    BootScreen bs = new BootScreen();
                    bs.ShowDialog();
                }
                else if (count > 1)
                {
                    MessageBox.Show("Duplicate Alias/Password");
                }
                else
                    MessageBox.Show("Login Fail!");


                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }


        }
        private void CheckLoginChange()
        {
            if (txt_UserName.TextLength > 0 && txt_Password.TextLength > 0)
                BtnLogin.Enabled = true;
            else
                BtnLogin.Enabled = false;
        }

        private void txt_UserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckLoginChange();
        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            CheckLoginChange();
        }

        private void txt_UserName_TextChanged(object sender, EventArgs e)
        {
            CheckLoginChange();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
