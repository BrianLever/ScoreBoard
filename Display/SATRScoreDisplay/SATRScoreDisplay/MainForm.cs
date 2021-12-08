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
    public partial class SATRScoreDisplayMain : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        private Int16 GenreCode;
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        ConfigData cd = new ConfigData();

        public SATRScoreDisplayMain()

        
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\SATRScore\SATRScore.accdb;
Persist Security Info=False;";
        }

        private void CloseApplication()
        {
            Program.rf.rfPortClose();
            Application.Exit();
        }
        private void LoadLanguageString()
        {
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            int LanguageCode = 0;
            command.CommandText = "SELECT Language_Code FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                LanguageCode = reader.GetInt32(0);
                reader.Close();
            }

            command.CommandText = "SELECT Prefix, Folder FROM Languages WHERE Language_Code = " + Convert.ToString(LanguageCode) + ";";
         //   MessageBox.Show(command.CommandText);
            reader = command.ExecuteReader();
          
            if (reader.Read())
            { 
                Program.LanguagePrefix = reader[0].ToString();
                Program.LanguageFolder = reader[1].ToString();
                reader.Close();
            }
          
            connection.Close();
        }

        private string GenreFolder()
        {

            string folder = "";
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Genre_Code, Battle_Code FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                GenreCode = reader.GetInt16(0);
                Program.ScoreboardBattleCode = reader.GetInt16(1);
                //   MessageBox.Show(Convert.ToString(GenreCode));
                reader.Close();
            }
            else
                MessageBox.Show("Config  file closed");

            command.CommandText = "SELECT * FROM Genre WHERE Genre_Code = " + Convert.ToString(GenreCode) + ";";
            reader = command.ExecuteReader();


            if (reader.Read())
            {
                folder = reader[4].ToString();
                reader.Close();
            }
            connection.Close();
            return folder;
        }

        private void GetScreenDimensions()
        {
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Scoreboards_Width, Scoreboards_Height FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Program.ScoreboardWidth = reader.GetInt32(0);
                Program.ScoreboardHeight = reader.GetInt32(1);

                reader.Close();
            }
            else
                MessageBox.Show("Config  file closed");
            connection.Close();
        }

        private void GetUpdateFrequency()
        {
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Update_Frequency FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Program.UpdateFrequency = reader.GetInt32(0);
                reader.Close();
            }
            else
                MessageBox.Show("Config  file closed");
            connection.Close();
        }
        private void LoadScoreboard()
        {
            bool CloseApp = false;
            int LeftPos, TopPos;
            //LeftPos = this.Left;
            //TopPos = this.Top;
            LeftPos = 0;
            TopPos = 0;

          //  ResultForm RF = new ResultForm();
          //  RF.ShowDialog();

            //GET THE MISSION from the database
            Program.MissionCode=8;
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Mission_Code FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Program.MissionCode = reader.GetInt32(0);
                reader.Close();
            }
            else
                MessageBox.Show("Config  file closed");
            connection.Close();

            GetScreenDimensions();
            GetUpdateFrequency();

            do
            {
                Program.GenreFolder = GenreFolder();
                LoadLanguageString();
                switch (Program.MissionCode)
                {
                    case 1: DeathMatch2Form dm = new DeathMatch2Form(); dm.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); dm.backgroundfilename = "DeathMatch.png"; dm.StartPosition = FormStartPosition.Manual; dm.Left = LeftPos; dm.Top = TopPos; dm.FormMissionCode = Program.MissionCode; dm.ShowDialog(); CloseApp = dm.CloseApp; LeftPos = dm.Left; TopPos = dm.Top; dm.Dispose(); break;
                    case 2: OnePointDomination df = new OnePointDomination(); df.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); df.backgroundfilename = "Domination.png"; df.StartPosition = FormStartPosition.Manual; df.Left = LeftPos; df.Top = TopPos; df.FormMissionCode = Program.MissionCode; df.ShowDialog(); CloseApp = df.CloseApp; LeftPos = df.Left; TopPos = df.Top; df.Dispose(); break;
                    case 3: ThreePointDominationForm pd = new ThreePointDominationForm(); pd.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); pd.backgroundfilename = "3PointDomination.png"; pd.StartPosition = FormStartPosition.Manual; pd.Left = LeftPos; pd.Top = TopPos; pd.FormMissionCode = Program.MissionCode; pd.ShowDialog(); CloseApp = pd.CloseApp; LeftPos = pd.Left; TopPos = pd.Top; pd.Dispose(); break;
                    case 4: HeistForm hf = new HeistForm(); hf.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); hf.backgroundfilename = "Heist.png"; hf.StartPosition = FormStartPosition.Manual; hf.Left = LeftPos; hf.Top = TopPos; hf.FormMissionCode = Program.MissionCode; hf.ShowDialog(); CloseApp = hf.CloseApp; LeftPos = hf.Left; TopPos = hf.Top; hf.Dispose(); break;
                    case 5: RushForm rf = new RushForm(); rf.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); rf.backgroundfilename = "Rush.png"; rf.StartPosition = FormStartPosition.Manual; rf.Left = LeftPos; rf.Top = TopPos; rf.FormMissionCode = Program.MissionCode; rf.ShowDialog(); CloseApp = rf.CloseApp; LeftPos = rf.Left; TopPos = rf.Top; rf.Dispose(); break;
                    case 6: CaptureTheFlagForm cf = new CaptureTheFlagForm(); cf.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); cf.backgroundfilename = "CaptureTheFlag.png"; cf.StartPosition = FormStartPosition.Manual; cf.Left = LeftPos; cf.Top = TopPos; cf.FormMissionCode = Program.MissionCode; cf.ShowDialog(); CloseApp = cf.CloseApp; LeftPos = cf.Left; TopPos = cf.Top; cf.Dispose(); break;
                    case 7: FreeForAllForm ffa = new FreeForAllForm(); ffa.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); ffa.backgroundfilename = "FreeForAll.png"; ffa.StartPosition = FormStartPosition.Manual; ffa.Left = LeftPos; ffa.Top = TopPos; ffa.FormMissionCode = Program.MissionCode; ffa.ShowDialog(); CloseApp = ffa.CloseApp; LeftPos = ffa.Left; TopPos = ffa.Top; ffa.Dispose(); break;
                    case 8: DeviceMonitorForm mf = new DeviceMonitorForm(); mf.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); mf.backgroundfilename = "DeviceMonitor.png"; mf.StartPosition = FormStartPosition.Manual; mf.Left = LeftPos; mf.Top = TopPos; mf.FormMissionCode = Program.MissionCode; mf.ShowDialog(); CloseApp = mf.CloseApp; LeftPos = mf.Left; TopPos = mf.Top; mf.Dispose(); break;
                    case 9:GamersMonitorForm gm = new GamersMonitorForm(); gm.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); gm.backgroundfilename = "GamersMonitor.png"; gm.StartPosition = FormStartPosition.Manual; gm.Left = LeftPos; gm.Top = TopPos; gm.FormMissionCode = Program.MissionCode; gm.ShowDialog(); CloseApp = gm.CloseApp; LeftPos = gm.Left; TopPos = gm.Top; gm.Dispose(); break;
                    case 10: BattleRoyale br = new BattleRoyale(); br.Size = new Size(Program.ScoreboardWidth, Program.ScoreboardHeight); br.backgroundfilename = "BattleRoyal.png"; br.StartPosition = FormStartPosition.Manual; br.Left = LeftPos; br.Top = TopPos; br.FormMissionCode = Program.MissionCode; br.ShowDialog(); CloseApp = br.CloseApp; LeftPos = br.Left; TopPos = br.Top; br.Dispose(); break;
                    default: MessageBox.Show("Mission not found " + Convert.ToString(Program.MissionCode)); CloseApp = true; break;
                }
            } while (!CloseApp);
            CloseApplication();
        }

        private void SATRScoreDisplayMain_Load(object sender, EventArgs e)
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
                //MessageBox.Show("After Open DB");
                if (dr == DialogResult.OK)
                {
                    Program.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dialog.FileName + ";Persist Security Info=False;";
                    connection.ConnectionString = Program.ConnectionString;

                    Program.rootdirectory = dialog.FileName.Substring(0, dialog.FileName.Length - 16);
                    if (!System.IO.Directory.Exists(Program.rootdirectory))
                        Program.rootdirectory = driveLetter + "SATRScore"; ;
                    System.Environment.CurrentDirectory = Program.rootdirectory;
                }
                else
                {
                    MessageBox.Show("Database not found");
                    CloseApplication();
                }
            }
            try
            {
                this.Hide();
                Program.GenreFolder = GenreFolder();
                LoadLanguageString();

                SplashScreen ss = new SplashScreen();
                ss.DBConnection = Program.ConnectionString;
                ss.ShowDialog();
                if (ss.CloseApp)
                    this.Close();
                else
                {
                    LoadScoreboard();
                    this.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
                CloseApplication();
            }
            cd.LoadConfig();
         
        }

        private void SATRScoreDisplayMain_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void SATRScoreDisplayMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void SATRScoreDisplayMain_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void CheckMissionCodeTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
