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
    
    public partial class LoginForm : Form
    {

        bool skiplogin = true;
        private OleDbConnection connection = new OleDbConnection();
        
        
        public LoginForm()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\SATRScore\SATRScore.accdb;
Persist Security Info=False;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Program.rootdirectory = Directory.GetCurrentDirectory();
            string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
            string DBPath = Directory.GetCurrentDirectory() + "\\SATRScore.accdb";
            if (File.Exists(DBPath))
            {
                Program.rootdirectory = Directory.GetCurrentDirectory();
                Program.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBPath + ";Persist Security Info=False;";
            }
            else
            {
                DBPath = driveLetter + "\\SATRScore\\SATRScore.accdb";
                Program.rootdirectory = driveLetter + "SATRScore";
            }
            if (File.Exists(DBPath))
            {
                Program.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DBPath + ";Persist Security Info=False;";
                System.Environment.CurrentDirectory = Program.rootdirectory;
            }
            else
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
                    Program.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dialog.FileName + ";Persist Security Info=False;";

                    Program.rootdirectory = dialog.FileName.Substring(0, dialog.FileName.Length - 16);
                    if (!System.IO.Directory.Exists(Program.rootdirectory))
                        Program.rootdirectory = driveLetter + "SATRScore";
                    System.Environment.CurrentDirectory = Program.rootdirectory;
                    connection.ConnectionString = Program.ConnectionString;
                }
                else
                {
                    MessageBox.Show("Database not found");
                    Application.Exit();
                }
            }

            try
            {
                // connection.Open();
                SplashScreen ss = new SplashScreen();
                ss.DBConnection = Program.ConnectionString;
                ss.ShowDialog();
                if (ss.CloseApp)
                {
                    Program.rf.rfPortClose();
                    this.Close();
                }
                else
                if (skiplogin)
                    LoadBootScreen();
                //connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
                Program.rf.rfPortClose();
                Application.Exit();
            }

        }

        private void LoadBootScreen()
        {
            connection.Close();
            connection.Dispose();
            this.Hide();
            BootScreen bs = new BootScreen();
            bs.DBConnection = Program.ConnectionString;
            bs.ShowDialog();
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
                    LoadBootScreen();
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
