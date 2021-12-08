using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SATRScore
{
    public class ConfigData
    {
        
        private OleDbConnection connection = new OleDbConnection();
        public Int16 BattleCode=0;
        public int AfterMissionSync=30;
        public int DBLevel=10;

        public ConfigData()
        {
            LoadConfig();
        }

        public bool closeappcheck()
        {
            DialogResult dialogResult = MessageBox.Show("Close Application", "Would you like to close SATR Admin", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Program.rf.rfPortClose();
                return true;
            }
            else
                return false;
        }

        public void LoadConfig()
        {
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Battle_Code,After_Mission_Sync_Time,DBLevel, Test_Mode FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                BattleCode = reader.GetInt16(0);
                Program.ScoreboardBattleCode = BattleCode;
                AfterMissionSync = reader.GetInt32(1);
                DBLevel = reader.GetInt32(2);
                Program.TestMode = reader.GetBoolean(3);
                reader.Close();
            }
            else
            {
                BattleCode = 0;
                AfterMissionSync = 30;
                DBLevel = 10;
            }
            connection.Close();
        }

        public void DeviceMonitorResponse(int SenderID,Int16 DeviceRoleCode, string Alias, int IntegerParameter1, int IntegerParameter2)
        {
            bool RecordFound = false;
            //Check if SATR ID already exists, then update it
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();
            string query;
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT Count(*) as DeviceCnt FROM SynchronisedDevice WHERE SynchronisedDevice.SATR_Unit_ID = " + Convert.ToString(SenderID)+";";
           
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetInt32(0) > 0)
                    RecordFound = true;
                else
                    RecordFound = false;
                reader.Close();

                if (RecordFound)
                {
                    query = "UPDATE SynchronisedDevice SET Device_Role_Code = " + Convert.ToString(DeviceRoleCode) + 
                        ", Team_Code = " + Convert.ToString(IntegerParameter1) + " , Score =" 
                        + Convert.ToString(IntegerParameter2) + ", Alias = '" + Alias +
                      "', Ping = True"+ " WHERE SynchronisedDevice.SATR_Unit_ID = " + Convert.ToString(SenderID) + "; ";
                    //         MessageBox.Show("Update Device" + Convert.ToString(SenderID));
                    Program.UpdatedSyncDevices++;
                }
                else//if does not exist, add to database
                {
                    query = "INSERT INTO SynchronisedDevice VALUES (" + Convert.ToString(SenderID) + ",'" + Alias + "'," + Convert.ToString(DeviceRoleCode) + "," + Convert.ToString(IntegerParameter1) + "," + Convert.ToString(IntegerParameter2) + ",True, '', 0, 0, 0, 0, 0, 0, False, False, False, False, 0, True, True);"; //July 25 2018
                                                                                                                                                                                                                                                                                                                      //         MessageBox.Show("New Device" + Convert.ToString(SenderID));
                    Program.NewSyncDevices++;
                }
               // MessageBox.Show(query);
                Program.textSQLstring = query;
                command.CommandText = query;
                command.ExecuteNonQuery();
               
            }
            else MessageBox.Show("Synchronise Table Not Found");

            connection.Close();

        }
    }
}
