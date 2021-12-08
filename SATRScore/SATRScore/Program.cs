using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SATRScore
{
    static class Program
    {
        public static LoginForm lf = null;
      //  public static TimeSpan TimeLeftInGame;
        public const int ScoreboardID = 2047;
        public static Int16 ScoreboardBattleCode = 0;
        public static string ConnectionString;
        public static string textSQLstring;
        public const int DBLevel = 10;
        public static string rootdirectory;
        public static int RFWaitTimeMS = 200;
        public const string version = "SATR SCORE ADMIN v1.0 26 July 2018";
        public static bool TestMode = false;
        public static RFPacketDriver rf;
        public static int NewSyncDevices = 0;
        public static int UpdatedSyncDevices = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            rf = new RFPacketDriver();
 
            //bool isConnected = rf.rfPortOpen(); //true-connected, false - failed

            //if(isConnected == false)
            //    if (MessageBox.Show("Quit SATR Scoreboard?", "Question", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        return;
            //    }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            lf = new LoginForm();
            Application.Run(lf);
            //Application.Run(new LoginForm());
        }

        public static void ReceiveRadioPacket(int CommandCode, Int16 BattleCode, int SenderID, int ReceiverID, Int16 ByteParameter1, int IntegerParameter1,
        int IntegerParameter2, int IntegerParameter3, int IntegerParameter4, int IntegerParameter5, int IntegerParameter6, int IntegerParameter7, Int16 ByteParameter2, string StringParameter)
        {
       
            ConfigData cd = new ConfigData();
            if ((ReceiverID == ScoreboardID || ReceiverID == 0) && (ScoreboardBattleCode == BattleCode))
            {
             //   MessageBox.Show("Receive Packet CC " + Convert.ToString(CommandCode));
                switch (CommandCode)
                {
                    case 25: cd.DeviceMonitorResponse(SenderID, ByteParameter1, StringParameter, IntegerParameter1, IntegerParameter2); break;
                 //   case 17: TimeLeftInGame = TimeSpan.FromSeconds(IntegerParameter1); break;//time sync
                }
            }
          //  else
            //    MessageBox.Show("RF Invalid Battle or Receiver");
            
        }

        public static void SendRadioPacket(int DBLevel,int CommandCode,Int16 BattleCode, int ReceiverID,Int16 ByteParameter1,int IntegerParameter1, int IntegerParameter2,
           int IntegerParameter3, int IntegerParameter4, int IntegerParameter5, int IntegerParameter6, int IntegerParameter7,Int16 ByteParameter2,string StringParameter)
        {
          
            
            Program.rf.rfSendPacket(DBLevel, CommandCode, BattleCode, ScoreboardID, ReceiverID, ByteParameter1,
                IntegerParameter1, IntegerParameter2, IntegerParameter3, IntegerParameter4,
                IntegerParameter5, IntegerParameter6, IntegerParameter7, ByteParameter2, StringParameter);

        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random(); return random.Next(min, max);

        }
    }
}
