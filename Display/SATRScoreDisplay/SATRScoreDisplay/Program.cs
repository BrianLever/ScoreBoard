using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace SATRScoreDisplay
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static TimeSpan TimeLeftInGame = TimeSpan.Zero;
        public const int ScoreboardID = 2047;

        public static Int16 ScoreboardBattleCode = 0;
        public static string ConnectionString;
        public static int MissionCode;
        public static int DBLevel = 10;
        public static string GenreFolder;
        public static string LanguageFolder;
        public static string LanguagePrefix;
        public static string StatusText = "";
        public static int UpdateFrequency = 10;
        public const int ScreenWidth = 2560;//DEFAULT SCREEN SIZE
        public const int ScreenHeight = 1440;

        public static int ScoreboardWidth = 2560;//CURRENT SCREEN SIZE
        public static int ScoreboardHeight = 1440;

        public const int MaxTeams = 8;
        public static string rootdirectory;
        public const string version = "SATR SCORE v1.1 06 August 2018";
        public static int RFWaitTimeMS = 3000;
        public static int NextPageMS = 5000;
        public static bool TestMode = false;
        public static int GenreCode = 2;

        public static RFPacketDriver rf;
        public static string LastException;
        

        [STAThread]
        static void Main()
        {
            Program.rf = new RFPacketDriver();

            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SATRScoreDisplayMain());
        }

        public static void ReceiveRadioPacket(int CommandCode, Int16 BattleCode, int SenderID, int ReceiverID, Int16 ByteParameter1, int IntegerParameter1,
int IntegerParameter2, int IntegerParameter3, int IntegerParameter4, int IntegerParameter5, int IntegerParameter6, int IntegerParameter7, Int16 ByteParameter2, string StringParameter)
        {
            String msg =
                " CommandCode       = " + CommandCode.ToString() + "\r\n" +
                " BattleCode        = " + BattleCode.ToString() + "\r\n" +
                " SenderID          = " + SenderID.ToString() + "\r\n" +
                " ReceiverID        = " + ReceiverID.ToString() + "\r\n" +
                " ByteParameter1    = " + ByteParameter1.ToString() + "\r\n" +
                " ByteParameter2    = " + ByteParameter2.ToString() + "\r\n" +
                " IntegerParameter1 = " + IntegerParameter1.ToString() + "\r\n" +
                " IntegerParameter2 = " + IntegerParameter2.ToString() + "\r\n" +
                " IntegerParameter3 = " + IntegerParameter3.ToString() + "\r\n" +
                " IntegerParameter4 = " + IntegerParameter4.ToString() + "\r\n" +
                " IntegerParameter5 = " + IntegerParameter5.ToString() + "\r\n" +
                " IntegerParameter6 = " + IntegerParameter6.ToString() + "\r\n" +
                " IntegerParameter7 = " + IntegerParameter7.ToString() + "\r\n" +
                " StringParameter   = " + StringParameter + "\r\n";

         //   MessageBox.Show(msg); 
            
        
            ConfigData cd = new ConfigData();
            if ((ReceiverID == ScoreboardID || ReceiverID == 0) && (ScoreboardBattleCode == BattleCode))
            {
                switch (CommandCode)
                {
                        
                    case 23: StatusText = cd.NewPhrase(IntegerParameter1);   break;//Receive the Phrase
                    case 16: Program.TimeLeftInGame = TimeSpan.FromSeconds(1); break;//End of game received
                    case 17: cd.TimeSynch(IntegerParameter1);    break;//time sync
                    case 25: cd.MonitorResponse(SenderID, ReceiverID, ByteParameter1, IntegerParameter1, IntegerParameter2, StringParameter); break;
                    case 27: cd.MonitorDetailResponse(SenderID, ReceiverID, ByteParameter1, IntegerParameter1,IntegerParameter2, IntegerParameter3, IntegerParameter4, IntegerParameter5, IntegerParameter6, IntegerParameter7, ByteParameter2, StringParameter); break;
                        
                }
            }
            
        }

        public static void SendRadioPacket(int DBLevel, int CommandCode, Int16 BattleCode, int ReceiverID, Int16 ByteParameter1, int IntegerParameter1, int IntegerParameter2,
           int IntegerParameter3, int IntegerParameter4, int IntegerParameter5, int IntegerParameter6, int IntegerParameter7, Int16 ByteParameter2, string StringParameter)
        {

            //  MessageBox.Show("Send Radio Packet CC " + Convert.ToString(CommandCode)+" Byte 1 " + Convert.ToString(ByteParameter1));
            rf.StarttimerPortCtl();
            if (rf.serialPort1.IsOpen)
            {
               
                Program.rf.rfSendPacket(DBLevel, CommandCode, BattleCode, ScoreboardID, ReceiverID, ByteParameter1,
                    IntegerParameter1, IntegerParameter2, IntegerParameter3, IntegerParameter4,
                    IntegerParameter5, IntegerParameter6, IntegerParameter7, ByteParameter2, StringParameter);
            }            
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random(); return random.Next(min, max);

        }
        public static Int16 RandomNumber16(int min, int max)
        {
            Random random = new Random(); return Convert.ToInt16(random.Next(min, max));

        }

        public static int xLocation(int x, int ScreenWidth)
        {
            return (int)(x * ScreenWidth / Program.ScreenWidth);
        }

        public static int yLocation(int y, int ScreenHeight)
        {
            return (int)(y * ScreenHeight / Program.ScreenHeight);
        }

        public static int fontheight(int fontsize, int ScreenHeight)
        {
            return (int)(fontsize * ScreenHeight / Program.ScreenHeight);
        }

        public static Image MedalImage(bool medalwinner, int GridRow)
        {
            Image MedalImage = null;
            Image NoMedalImage = null;
            string Medalfile;
            string NoMedalFile;
            if (!(GridRow % 2 == 0))
            {
                NoMedalFile = "SilverBox.png";
                Medalfile = "MedalSilver.png";//MedalSilver.png
            }
            else
            {
                NoMedalFile = "BlackBox.png";
                Medalfile = "Medal.png";
            }

            string directoryName = Program.rootdirectory;

            String MedalFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + Medalfile;
            if (!File.Exists(MedalFileName))
                MedalFileName = directoryName + @"\Images\" + Medalfile;
            if (File.Exists(MedalFileName))
                MedalImage = Image.FromFile(MedalFileName);
            else
                MessageBox.Show("No Medal Image " + MedalFileName);

            String NoMedalFileName = directoryName + @"\Images\" + Program.GenreFolder + @"\" + NoMedalFile;
            if (!File.Exists(NoMedalFileName))
                NoMedalFileName = directoryName + @"\Images\" + NoMedalFile;
            if (File.Exists(NoMedalFileName))
                NoMedalImage = Image.FromFile(NoMedalFileName);
            if (medalwinner)
                return MedalImage;
            else
                return NoMedalImage;
        }
    }
   
}
