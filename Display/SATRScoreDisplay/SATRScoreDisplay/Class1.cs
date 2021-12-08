using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace SATRScoreDisplay
{
    public class ConfigData
    {
        private OleDbConnection connection = new OleDbConnection();
        
        public Int16 BattleCode = 0;
        public int AfterMissionSync = 30;
       

        public ConfigData()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            if (!String.IsNullOrEmpty(Program.ConnectionString))
                if (Program.ConnectionString.Length > 1)
                {
                    connection.ConnectionString = Program.ConnectionString;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT Battle_Code,After_Mission_Sync_Time,DBLevel, RF_Transmission_Time_MS, Page_Grid_Time_MS, Test_Mode, Genre_Code FROM Config;";
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        BattleCode = reader.GetInt16(0);
                        AfterMissionSync = reader.GetInt32(1);
                        Program.DBLevel = reader.GetInt32(2);
                     //   Program.RFWaitTimeMS = reader.GetInt32(3);
                        Program.NextPageMS = reader.GetInt32(4);
                        Program.TestMode = reader.GetBoolean(5);
                        Program.GenreCode = reader.GetInt16(6);
                        reader.Close();
                    }
                    else
                    {
                        BattleCode = 0;
                        AfterMissionSync = 30;
                        Program.DBLevel = 10;
                    }
                    connection.Close();
                }
        }

   
        public int CheckMission()
        {
           
            int NewMissionCode = 0;
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Mission_Code FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                NewMissionCode = reader.GetInt32(0);
                reader.Close();
                connection.Close();
            }
            connection.Close();
            return NewMissionCode;

        }
        public void TimeSynch(int TimeInSeconds)
        {
            if (Program.TimeLeftInGame.CompareTo(TimeSpan.Zero) < 1 && TimeInSeconds > 1)//Check if the current timer has expired and now a new mission has started.
            {
                connection.ConnectionString = Program.ConnectionString;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE Config SET New_Mission = true";
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE SynchronisedDevice SET Pinged_After_Start=false;";
                command.ExecuteNonQuery();
                connection.Close();
                command.Dispose();
            }
                
            Program.TimeLeftInGame = TimeSpan.FromSeconds(TimeInSeconds); 
        }

        public void playSound(string path)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = path;
            player.Load();
            player.Play();
        }

        public string NewPhrase(int PhraseCode)
        {
            string Phrase = "";
            string filename = "";
            
            if (!String.IsNullOrEmpty(Program.ConnectionString))
                if (Program.ConnectionString.Length > 1)
                {
                    connection.ConnectionString = Program.ConnectionString;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT Description, Filename FROM Phrase WHERE Phrase_Code = " + Convert.ToString(PhraseCode)+ ";";
                    
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Phrase = reader[0].ToString();
                        filename = reader[1].ToString();
                      
                        reader.Close();

                        //IF THERE IS A FILENAME PLAY THE VOICE OVER.

                        string directoryName = Program.rootdirectory;
                        string Path = directoryName + @"\Phrases\" + Program.LanguageFolder + @"\" + Program.LanguagePrefix+filename;
                        
                        if (!File.Exists(Path))
                            Path = directoryName + @"\Phrases\" + @"\" + filename;
                        if (File.Exists(Path))
                            playSound(Path);

                    }
                    connection.Close();
                }
            return Phrase;

        }

        public void MonitorResponse(int SATRUnitID, int ReceiverID, Int16 DeviceRoleCode, int TeamCode, int Score, string Alias)
        {
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            //CHECK IF GAMER RECORD ECISTS
            command.CommandText = "SELECT Count(*) as CNT FROM SynchronisedDevice WHERE SATR_Unit_ID = " + Convert.ToString(SATRUnitID) + ";";
            OleDbDataReader reader = command.ExecuteReader();
            int GamerRecordCount = 0;
            if (reader.Read())
            {
                GamerRecordCount = reader.GetInt32(0);
            }
            reader.Close();
            if (GamerRecordCount > 0)//IF YES DEVICE EXISTS in DB, THEN UPDATE THE SCORE
            {
                command.CommandText = "UPDATE SynchronisedDevice SET Pinged_After_Start = True,Ping = True, Score = " + Convert.ToString(Score) + ", Device_Role_Code = " +Convert.ToString(DeviceRoleCode) + ", Team_Code = " + Convert.ToString(TeamCode) +", ALIAS = '"+Alias + "' WHERE SATR_Unit_ID = " + Convert.ToString(SATRUnitID) + ";";
                command.ExecuteNonQuery();

            }
            else                //ELSE INSERT A NEW GAMER
            {
                command.CommandText = "INSERT INTO SynchronisedDevice VALUES (" + Convert.ToString(SATRUnitID) + ",'" + Alias +"',"+Convert.ToString(DeviceRoleCode)+", "+Convert.ToString(TeamCode)+","+Convert.ToString(Score)+",True,'',0,0,0,0,0,0, false, false, false, false,0, true, true);";
                command.ExecuteNonQuery();
            }
            connection.Close();


        }
        public void MonitorDetailResponse(int SATRUnitID, int ReceiverID, Int16 TeamCode, int IntegerParameter1, int IntegerParameter2, int IntegerParameter3, int IntegerParameter4,
            int IntegerParameter5, int IntegerParameter6, int IntegerParameter7, Int16 DeviceRoleCode, string StatusLine)
        {
            //Work out Score based on Device Role
            int Score = 0;
            int Hits = 0;
            int Kills = 0;
            int Deaths = 0;
            int ShotsFired = 0;
            int EmulationCode = 0;
            int HitPoints = 0;
            float KD = 0;

         
            switch (DeviceRoleCode)
            {
                case 1: Score = IntegerParameter6; Kills = IntegerParameter3; Hits = IntegerParameter2; Deaths = IntegerParameter5; ShotsFired = IntegerParameter4; EmulationCode = IntegerParameter7; HitPoints = IntegerParameter1;
                    if (Deaths > 0) KD = Kills / Deaths; else if (Kills > 0) KD = 99; else KD = 0; break;
                case 4:
                case 10: Score = IntegerParameter4; break;
                case 16: Score = IntegerParameter1; break;
                case 25: bool BoxCaptured = false;
                    int ScoringTeam = 0;
                    if (IntegerParameter5 == 1) BoxCaptured = true;
                    if (BoxCaptured)
                        ScoringTeam = TeamCode;//The attacking team
                    else
                        ScoringTeam = IntegerParameter1;//The Defenders have the box still
                    TeamCode = (Int16)(ScoringTeam);
                    break;//RUSH BOX
                case 27: Score = IntegerParameter1; break;
            }
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE SynchronisedDevice SET Pinged_After_Start = True, Status_Bar = '" + StatusLine+"', Device_Role_Code = " + Convert.ToString(DeviceRoleCode) + ", Team_Code = " + Convert.ToString(TeamCode) + ", Score = " + Convert.ToString(Score) + ", Ping = True, Hits = "+Convert.ToString(Hits)
                +", Kills = " + Convert.ToString(Kills)+", Deaths = "+Convert.ToString(Deaths)+", Shots_Fired = "+Convert.ToString(ShotsFired)+ ", Hit_Points = " + Convert.ToString(HitPoints) + ", Emulation_Code = " + Convert.ToString(EmulationCode) + ", KD = "+Convert.ToString(KD)+ " WHERE SynchronisedDevice.SATR_Unit_ID = " + Convert.ToString(SATRUnitID) + "; "; 
            command.ExecuteNonQuery();


            if (DeviceRoleCode == 12)//Update the database with a Domination Box response)
            {

                for (int i = 1; i < Program.MaxTeams; i++)//Don't get scores for Team X 
                {
                    //CHECK IF TEAM RECORD FOR THIS DOMINATION BOX EXISTS
                    command.CommandText = "SELECT Count(*) as CNT FROM TeamScore WHERE Team_Code = " + Convert.ToString(i) + " AND SATR_Unit_ID = " + Convert.ToString(SATRUnitID) + ";";
                    OleDbDataReader reader = command.ExecuteReader();
                    int TeamScoreRecordCount = 0;
                    if (reader.Read())
                    {
                        TeamScoreRecordCount = reader.GetInt32(0);
                    }
                    reader.Close();
                    switch (i)
                    {
                        case 1: Score = IntegerParameter1; break;
                        case 2: Score = IntegerParameter2; break;
                        case 3: Score = IntegerParameter3; break;
                        case 4: Score = IntegerParameter4; break;
                        case 5: Score = IntegerParameter5; break;
                        case 6: Score = IntegerParameter6; break;
                        case 7: Score = IntegerParameter7; break;

                    }
                    if (TeamScoreRecordCount > 0)//IF YES BOX EXISTS, THEN UPDATE THE SCORE
                    {
                        command.CommandText = "UPDATE TeamScore SET Score = " + Convert.ToString(Score) + " WHERE Team_Code = " + Convert.ToString(i) + " AND SATR_Unit_ID = " + Convert.ToString(SATRUnitID) + ";";
                        command.ExecuteNonQuery();

                    }
                    else //ELSE INSERT A NEW SCORE if score greater than 0;
                    {
                        command.CommandText = "INSERT INTO TeamScore VALUES (" + Convert.ToString(SATRUnitID)+","+ Convert.ToString(i)+","+ Convert.ToString(Score) + ");";
                        command.ExecuteNonQuery();
                    }
                    reader.Close();
                }

            }
            connection.Close();
        }
        

    }
}
