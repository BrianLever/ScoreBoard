using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace SATRScore
{
    public class ConfigData
    {
        public string DBConnection;
        private OleDbConnection connection = new OleDbConnection();
        public int BattleCode=0;
        public int AfterMissionSync=30;
        public int DBLevel=10;

        public void LoadConfig()
        {
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Battle_Code,After_Mission_Sync_Time,DBLevel FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                BattleCode = reader.GetInt32(0);
                AfterMissionSync = reader.GetInt32(1);
                DBLevel = reader.GetInt32(2);
                reader.Close();
            }
            else
            {
                BattleCode = 0;
                AfterMissionSync = 30;
                DBLevel = 10;
            }


        }
    }
}
