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

    public partial class BootScreen : Form
    {
        public string DBConnection;
        private OleDbConnection connection = new OleDbConnection();
        Int16 GenreCode = 0;
        public Image BackButtonImage;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public ConfigData cd = new ConfigData();


        public BootScreen()
        {
            InitializeComponent();

        }

        private void BootScreen_Load(object sender, EventArgs e)
        {
            this.Width = 1285;
            this.Height = 700;//1285, 700

            LoadMissionBadge();
            LoadBackButtonBitMap();
            LoadTimeLeftButtonImage();
            GetRFWaitTime();
        }

        private void GetRFWaitTime()
        {
            connection.Close();
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT RF_Transmission_Time_MS FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int WaitTime = (int)(reader.GetInt32(0));
                Program.RFWaitTimeMS = WaitTime;
                reader.Close();
            }
            else
                MessageBox.Show("Config  file closed");
            reader.Close();
           connection.Close();
        }

        private void CloseApplication()
        {
            Program.rf.rfPortClose();
            Application.Exit();
        }
        private void BootScreen_FormClosed(object sender, FormClosedEventArgs e)
        {

            CloseApplication();
        }


        private void GoLiveBTN_Click(object sender, EventArgs e)
        {

            //TimeLeftInGame = TimeSpan.Zero;

            //  Program.ReceiveRadioPacket(17, cd.BattleCode, 2000, 0, 0, (15*60), 0, 0, 0, 0, 0, 0, 0, "CONTROL1");  //DELETE IN PRODUCTION
          //  GameTimer.Enabled = true;

            connection.Close();
            connection.ConnectionString = Program.ConnectionString;
            connection.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE SynchronisedDevice SET Score = 0, Ping = False, Status_Bar = '', Kills = 0, Hits = 0, Deaths = 0, Shots_Fired = 0, Emulation_Code = 0, Hit_Points = 0, Leader_Hits = false"+
                ", Lowest_Deaths = false, Leader_KD = false, KD=0, Leader_Kills=false, Pinged_After_Start=false;";
            command.ExecuteNonQuery();

            command.CommandText = "DELETE FROM TeamScore;";
            command.ExecuteNonQuery();

            command.CommandText = "UPDATE Config SET New_Mission = true";
            command.ExecuteNonQuery();

            connection.Close();
            if (MessageBox.Show("Send Start signal?", "RADIO SIGNAL", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.rf.rfPortOpen(false);
                Program.SendRadioPacket(cd.DBLevel, 15, cd.BattleCode, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "");//Send Start Signal
                Program.rf.rfPortClose();
            }
           
            MessageBox.Show("Scores and pings cleared.");

        }

        private string GenreFolder()
        {

            string folder = "";
            connection.Close();
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            command.CommandText = "SELECT * FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                GenreCode = reader.GetInt16(0);
                Program.ScoreboardBattleCode = reader.GetInt16(3);
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


        private void LoadMissionBadge()
        {
            //MessageBox.Show(DBConnection);
            connection.ConnectionString = DBConnection;
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            int MissionCode = 0;


            command.CommandText = "SELECT * FROM Config;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                MissionCode = reader.GetInt32(2);
                reader.Close();
            }
            else
                MessageBox.Show("Config  file closed");

            command.CommandText = "SELECT * FROM Mission WHERE Mission_Code = " + Convert.ToString(MissionCode) + ";";
            reader = command.ExecuteReader();

            string directoryName = Program.rootdirectory;
            //  MessageBox.Show(directoryName);

            if (reader.Read())
            {
                string Smallfilename = reader[7].ToString();
                string Largefilename = reader[8].ToString();
                reader.Close();

                //SEARCH GENRE FOLDER FIRST
                string ImageFileName = directoryName + @"\Badges\" + GenreFolder() + @"\" + Largefilename;
                if (File.Exists(ImageFileName))
                {
                    MissionBadge.BackgroundImageLayout = ImageLayout.None;
                }
                else
                {
                    //              MessageBox.Show("Not Found " + ImageFileName);
                    ImageFileName = directoryName + @"\Badges\" + Largefilename;
                    //             MessageBox.Show("Genre Folder " + ImageFileName);
                }

                //                MissionBadge.ImageLocation = ImageFileName;
                if (File.Exists(ImageFileName))
                {
                        MissionBadge.BackgroundImage = Bitmap.FromFile(ImageFileName);
                        MissionBadge.BackgroundImageLayout = ImageLayout.None;
                        MissionBadge.Refresh();

                 //   MissionBadge.Image = Bitmap.FromFile(ImageFileName);
                  

                }
                else//Large version not found so try to find small version
                {
                    MissionBadge.BackgroundImageLayout = ImageLayout.Stretch;
                    ImageFileName = directoryName + @"\Badges\" + GenreFolder() + @"\" + Smallfilename;
                    if (File.Exists(ImageFileName))
                    {
                        //                MessageBox.Show("Found "+ImageFileName);
                    }
                    else
                    {
                        //              MessageBox.Show("Not Found " + ImageFileName);
                        ImageFileName = directoryName + @"\Badges\" + Smallfilename;
                        //             MessageBox.Show("Genre Folder " + ImageFileName);
                    }

                    //                MissionBadge.ImageLocation = ImageFileName;
                    if (File.Exists(ImageFileName))
                        MissionBadge.BackgroundImage = Bitmap.FromFile(ImageFileName);
                    else
                        MissionBadge.BackgroundImage = null;
                }
                    
                //       MessageBox.Show(ImageFileName);
                reader.Close();
            }

            connection.Open();
            command.CommandText = "SELECT Mission_Name FROM MissionByGenre WHERE Mission_Code = " + Convert.ToString(MissionCode) + " AND Genre_Code = " + Convert.ToString(GenreCode) + "; ";
            reader = command.ExecuteReader();
            if (reader.Read())
                MissionLabel.Text = reader[0].ToString();
            else
            {
                reader.Close();
                command.CommandText = "SELECT Mission FROM Mission WHERE Mission_Code = " + Convert.ToString(MissionCode) + "; ";
                reader = command.ExecuteReader();
                if (reader.Read())
                    MissionLabel.Text = reader[0].ToString();
                reader.Close();
            }

            reader.Close();
          
            connection.Close();
        }

        private void MissionBadge_Paint(object sender, PaintEventArgs e)
        {
            MissionBadge.BackColor = Color.Transparent;
        }

        private void MissionPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ChangeMissionBtn_Click(object sender, EventArgs e)
        {
            connection.Dispose();
            this.Hide();
            MissionSelectForm ms = new MissionSelectForm();
            ms.DBConnection = DBConnection;
            ms.GenreFolder = GenreFolder();
            ms.GenreCode = GenreCode;
            ms.BackButtonImage = BackButtonImage;
            ms.StartPosition = FormStartPosition.Manual;
            ms.Left = this.Left;
            ms.Top = this.Top;

            ms.ShowDialog();
            if (ms.closeapp) this.Close();
            LoadMissionBadge();
            this.Show();
            GenreFolder();
        }

        private void ConfigBtn_Click(object sender, EventArgs e)
        {
            connection.Dispose();
            this.Hide();
            ConfigForm cs = new ConfigForm();
            cs.DBConnection = DBConnection;
            cs.GenreCode = GenreCode;
            cs.BackButtonImage = BackButtonImage;
            cs.StartPosition = FormStartPosition.Manual;
            cs.Left = this.Left;
            cs.Top = this.Top;
            cs.ShowDialog();
            if (cs.closeapp) this.Close();
            else
            {
                LoadMissionBadge();
                LoadBackButtonBitMap();
                LoadTimeLeftButtonImage();
                this.Show();
            }

        }

        private void LoadBackButtonBitMap()
        {

            string directoryName = Program.rootdirectory;
            //SEARCH GENRE FOLDER FIRST
            string ImageFileName = directoryName + @"\Images\" + GenreFolder() + @"\BackButton.png";
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Images\BackButton.png";
            BackButtonImage = Bitmap.FromFile(ImageFileName);

        }

        private void LoadTimeLeftButtonImage()
        {
            //System.Environment.CurrentDirectory = "\\SATRScore";
            string directoryName = Program.rootdirectory;
            //SEARCH GENRE FOLDER FIRST
            string ImageFileName = directoryName + @"\Images\" + GenreFolder() + @"\TimeBtn.png";
            if (!File.Exists(ImageFileName))
                ImageFileName = directoryName + @"\Images\TimeBtn.png";
           // GameTimeBtn.BackgroundImage = Bitmap.FromFile(ImageFileName);

        }



        private void InsertKeys()
        {

            int i;
            int j;
            string buffer1;
            for (i = 1; i < 13; i++)
            {
                j = 285 + i;
                buffer1 = "Key " + Convert.ToString(i);
                InsertCodeList(j, buffer1);
            }

        }

        private void InsertPhrase(int code, string shortname, string description, string filename)
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "INSERT INTO Phrase VALUES (" + Convert.ToString(code) + ",'" + shortname + "','" + description + "','" + filename + "');";
            //  MessageBox.Show(query);
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private void NewPhrases()
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            connection.ConnectionString = DBConnection;
            connection.Open();
            string query = "DELETE FROM Phrase;";
            command.CommandText = query;
            command.ExecuteNonQuery();

            InsertPhrase(0, "No Phrase", "", "");
            InsertPhrase(1, "Victory", "You are victorious", "Yav.wav");
            InsertPhrase(2, "Defeated", "You have been defeated", "YHBD.wav");
            InsertPhrase(3, "Alpha Victory", "Alpha Team is victorious", "ATIV.wav");
            InsertPhrase(4, "Bravo Victory", "Bravo Team is victorious", "BTIV.wav");
            InsertPhrase(5, "Charlie Victory", "Charlie Team is victorious", "CTIV.wav");
            InsertPhrase(6, "Delta Victory", "Delta Team is victorious", "DTIV.wav");
            InsertPhrase(7, "Echo Victory", "Echo Team is victorious", "ETIV.wav");
            InsertPhrase(8, "Foxtrot Victory", "Foxtrot Team is victorious", "FTIV.wav");
            InsertPhrase(9, "Golf Victory", "Golf Team is victorious", "GTIV.wav");
            InsertPhrase(10, "Draw", "Draw", "Draw.wav");
            InsertPhrase(11, "Change Ends", "Change Ends", "ChgEnds.wav");
            InsertPhrase(12, "Good Job", "Good Job", "GoodJob.wav");
            InsertPhrase(13, "Go! Go! Go!", "Go! Go! Go!", "GoGoGo.wav");
            InsertPhrase(14, "Base Lost", "Base Lost", "BaseLst.wav");
            InsertPhrase(15, "Obj. Control.", "Objective Controlled", "ObjCtl.wav");
            InsertPhrase(16, "Alpha Secured", "Alpha Secured", "AlphaSe.wav");
            InsertPhrase(17, "Bravo Secured", "Bravo Secured", "BravoSe.wav");
            InsertPhrase(18, "Charlie Secured", "Charlie Secured", "CharSe.wav");
            InsertPhrase(19, "Delta Secured", "Delta Secured", "DeltaSe.wav");
            InsertPhrase(20, "Alpha Control", "Alpha Team in control", "ATICtl.wav");
            InsertPhrase(21, "Bravo Control", "Bravo Team in control", "BTICtl.wav");
            InsertPhrase(22, "Charlie Control", "Charlie Team in control", "CTICtl.wav");
            InsertPhrase(23, "Delta Control", "Delta Team in control", "DTICtl.wav");
            InsertPhrase(24, "Echo Control", "Echo Team in control", "ETICtl.wav");
            InsertPhrase(25, "Foxtrot Control", "Foxtrot Team in control", "FTICtl.wav");
            InsertPhrase(26, "Golf Control", "Golf Team in control", "GTICtl.wav");
            InsertPhrase(27, "VIP is Dead", "VIP is Dead", "VIPDead.wav");
            InsertPhrase(28, "LZ Reached", "LZ Reached", "LZRched.wav");
            InsertPhrase(29, "Pt. 1 Captured", "Point one Captured", "Pt1Cpt.wav");
            InsertPhrase(30, "Pt. 1 Fallen", "Point one fallen", "Pt1Fal.wav");
            InsertPhrase(31, "Pt. 2 Captured", "Point two captured", "Pt2Cpt.wav");
            InsertPhrase(32, "Pt. 2 Fallen", "Point two fallen", "Pt2Fal.wav");
            InsertPhrase(33, "Pt. 3 Captured", "Point three captured", "Pt3Cpt.wav");
            InsertPhrase(34, "Pt. 3 Fallen", "Point three fallen", "Pt3Fal.wav");
            InsertPhrase(35, "Pt. 4 Captured", "Point four captured", "Pt4Cpt.wavh");
            InsertPhrase(36, "Pt. 4 Fallen", "Point four fallen", "Pt4Fal.wav");
            InsertPhrase(37, "Charge", "Charge", "Charge.wav");
            InsertPhrase(38, "Dominating", "Dominating", "Doming.wav");
            InsertPhrase(39, "Dominated", "Dominated", "Domated.wav");
            InsertPhrase(40, "Alpha Lost", "Alpha Lost", "AlphaLT.wav");
            InsertPhrase(41, "Bravo Lost", "Bravo Lost", "BravoLT.wav");
            InsertPhrase(42, "Charlie Lost", "Charlie Lost", "CharLT.wav");
            InsertPhrase(43, "Delta Lost", "Delta Lost", "DeltaLT.wav");
            InsertPhrase(44, "Echo Lost", "Echo Lost", "EchoLT.wav");
            InsertPhrase(45, "Foxtrot Lost", "Foxtrot Lost", "FoxTrLT.wav");
            InsertPhrase(46, "Golf Lost", "Golf Lost", "GolfLT.wav");
            InsertPhrase(47, "Pt. Capt. Alpha", "Tactical point captured by Alpha team", "ACaptur.wav");
            InsertPhrase(48, "Pt. Capt. Bravo", "Tactical point captured by Bravo team", "BCaptur.wav");
            InsertPhrase(49, "Pt. Capt.Charlie", "Tactical point captured by Charlie team", "CCaptur.wav");
            InsertPhrase(50, "Pt. Capt. Delta", "Tactical point captured by Delta team", "DCaptur.wav");
            InsertPhrase(51, "Pt. Capt. Echo", "Tactical point captured by Echo team", "ECaptur.wav");
            InsertPhrase(52, "Pt. Capt.Foxtrot", "Tactical point captured by Foxtrot team", "FCaptur.wav");
            InsertPhrase(53, "Drinks Break", "Drink’s break", "DBreak.wav");
            InsertPhrase(54, "Return Base", "Return to base", "RBase.wav");
            InsertPhrase(55, "Briefing Area", "Come to the briefing area", "CBriefing.wav");
            InsertPhrase(56, "Follow CO", "Follow the commanding officer", "FCO.wav");
            InsertPhrase(57, "Follow me", "All gamers, all teams, follow me.   Move out!", "FME.wav");
            InsertPhrase(58, "Battle Middle", "Head to the middle of the battlefield", "MField.wav");
            InsertPhrase(59, "Read Stats", "Scroll through your statistics with your trigger.   Mission stats are your stats from the last mission. Session stats are all your missions combined.   A/W is the Assist to Wound ratio, K/D is the kill to death ratio.   O is for objective.", "GStats.wav");
            InsertPhrase(60, "Return Equip.", "Session over, return your equipment to the armoury", "SOver.wav");
            InsertPhrase(61, "Clothes Ret.", "Return your hats and hired clothing to the dirty clothes bag.", "Clothes.wav");
            InsertPhrase(62, "Stay", "Safety warning; stay where you are", "SWarnin.wav");
            InsertPhrase(63, "Evacuate", "Emergency situation;  evacuate the area", "Evacute.wav");
            InsertPhrase(64, "Snake", "A Snake has been found, follow staff directions.   Do not touch or try to handle the snake", "Snake.wav");

            connection.Close();
        }

        private void InsertCodeList(int code, string description)
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "INSERT INTO Emulation VALUES (" + Convert.ToString(code) + ",'" + description + "');";
            //  MessageBox.Show(query);
            command.CommandText = query;
            command.ExecuteNonQuery();
        }



        private void NewEmulationList()
        {

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            connection.ConnectionString = DBConnection;
            connection.Open();
            string query = "DELETE FROM Emulation;";
            command.CommandText = query;
            command.ExecuteNonQuery();

            InsertCodeList(1, "Colt M-1911");
            InsertCodeList(2, "Beretta 92FS(M9)");
            InsertCodeList(3, "Browning GP35-HP");
            InsertCodeList(4, "H&K USP Match");
            InsertCodeList(5, "9mm Makarov");
            InsertCodeList(6, "Tokarev TT-33");
            InsertCodeList(7, "Luger Pistole 08");
            InsertCodeList(8, "Walther P38");
            InsertCodeList(9, "Nambu 14 Shiko");
            InsertCodeList(10, "Smith & Wesson  M & P");
            InsertCodeList(11, "Nagrant M-1895");
            InsertCodeList(12, "Enfield Revolver");
            InsertCodeList(13, "Flint Lock Pistol");
            InsertCodeList(14, "Wheel-Lock Pist.");
            InsertCodeList(15, "S&W 44 Magnum");
            InsertCodeList(16, "Zip Gun");
            InsertCodeList(17, "PSS Pistol.");
            InsertCodeList(18, "H&K MP5");
            InsertCodeList(19, "H&K MP5 SD");
            InsertCodeList(20, "UZI");
            InsertCodeList(21, "M1928A1 Thompson");
            InsertCodeList(22, "M3 Grease Gun");
            InsertCodeList(23, "Owen SMG");
            InsertCodeList(24, "Sten Mark 2");
            InsertCodeList(25, "MP38/40");
            InsertCodeList(26, "MP18");
            InsertCodeList(27, "MP28-50");
            InsertCodeList(28, "MAT 49-20");
            InsertCodeList(29, "MAT 49-32");
            InsertCodeList(30, "PPS-43");
            InsertCodeList(31, "PPSch-41");
            InsertCodeList(32, "Paintball Marker");
            InsertCodeList(33, "FN P90");
            InsertCodeList(34, "H&K MP7");
            InsertCodeList(35, "HB Supersonic");
            InsertCodeList(36, "HB Subsonic");
            InsertCodeList(37, "M1 Carbine");
            InsertCodeList(38, "M2 Carbine");
            InsertCodeList(39, "M16A2");
            InsertCodeList(40, "M16A1 -1970");
            InsertCodeList(41, "M16 - 1967");
            InsertCodeList(42, "Colt Commando");
            InsertCodeList(43, "M14 Rifle");
            InsertCodeList(44, "M24 Sniper Rifle");
            InsertCodeList(45, "Barrett M82A1");
            InsertCodeList(46, "Simonov SKS");
            InsertCodeList(47, "AK47/AKM");
            InsertCodeList(48, "AK74");
            InsertCodeList(49, "Dragunov SVD");
            InsertCodeList(50, "M-1903 Rifle");
            InsertCodeList(51, "H&K G3");
            InsertCodeList(52, "M1 Garand");
            InsertCodeList(53, "Lee-Enfield SMLE");
            InsertCodeList(54, "Mosin-Nagant");
            InsertCodeList(55, "Steyr AUG");
            InsertCodeList(56, "FN-FAL/L1A1 SLR");
            InsertCodeList(57, "L85A1-2 /SA80");
            InsertCodeList(58, "H&K G36");
            InsertCodeList(59, "MP43 MP44 Stg.44");
            InsertCodeList(60, "Kar 98K");
            InsertCodeList(61, "Musket");
            InsertCodeList(62, "Greener Elephant");
            InsertCodeList(63, "Rifled Musket");
            InsertCodeList(64, "Spencer Rifle");
            InsertCodeList(65, "Henry Rifle");
            InsertCodeList(66, "Custom");
            InsertCodeList(67, "FN Minimi / M249");
            InsertCodeList(68, "M60 GPMG");
            InsertCodeList(69, "RPD/Type 56 LMG");
            InsertCodeList(70, "RPK LMG");
            InsertCodeList(71, "RPK 74 LMG");
            InsertCodeList(72, "M1919A4 MMG");
            InsertCodeList(73, "M-1918A2 BAR");
            InsertCodeList(74, "Maxim MG08 MMG");
            InsertCodeList(75, "Degtyarev DP LMG");
            InsertCodeList(76, "DShK M1938 HMG");
            InsertCodeList(77, "Lewis MK1 LMG");
            InsertCodeList(78, "Bren Mk2 LMG");
            InsertCodeList(79, "Vickers Mk 1 MMG");
            InsertCodeList(80, "MG34 LMG");
            InsertCodeList(81, "MG42 LMG");
            InsertCodeList(82, "M2 0.5 BMG");
            InsertCodeList(83, "M134 Minigun");
            InsertCodeList(84, "Axe");
            InsertCodeList(85, "Base ball Bat");
            InsertCodeList(86, "Baton");
            InsertCodeList(87, "Bayonet");
            InsertCodeList(88, "Brass Knuckle");
            InsertCodeList(89, "Cattle Prod");
            InsertCodeList(90, "Chain Saw");
            InsertCodeList(91, "Crow Bar");
            InsertCodeList(92, "Combat Knife");
            InsertCodeList(93, "Frying Pan");
            InsertCodeList(94, "Golf Club");
            InsertCodeList(95, "Hammer");
            InsertCodeList(96, "Katana");
            InsertCodeList(97, "Electrified Machete");
            InsertCodeList(98, "Meat Cleaver");
            InsertCodeList(99, "Pitch Fork");
            InsertCodeList(100, "Plasma Torch");
            InsertCodeList(101, "Power Drill");
            InsertCodeList(102, "Ripper");
            InsertCodeList(103, "Sledge Hammer");
            InsertCodeList(104, "Spade");
            InsertCodeList(105, "Unarmed Combat");
            InsertCodeList(106, "M1A1 Bazooka");
            InsertCodeList(107, "Flame Thrower");
            InsertCodeList(108, "Panzerfaust");
            InsertCodeList(109, "RPG HEAT");
            InsertCodeList(110, "RPG HE");
            InsertCodeList(111, "MK19 Launcher");
            InsertCodeList(112, "Winchester M97");
            InsertCodeList(113, "Remington 1100");
            InsertCodeList(114, "Riot Shotgun");
            InsertCodeList(115, "Double Barrel");
            InsertCodeList(116, "Blunderbuss");
            InsertCodeList(117, "Werewolf Bite");
            InsertCodeList(118, "Orc  Great Axe");
            InsertCodeList(119, "Orc Javelin");
            InsertCodeList(120, "Goblin Morning Star");
            InsertCodeList(121, "Goblin  Javelin");
            InsertCodeList(122, "Hobgoblin Long Sword");
            InsertCodeList(123, "Hobgoblin Bow");
            InsertCodeList(124, "Bug Bear Morning Star");
            InsertCodeList(125, "Bug Bear-Javelin");
            InsertCodeList(126, "Giants Club");
            InsertCodeList(127, "Wraiths Touch");
            InsertCodeList(128, "Ghouls Fingers");
            InsertCodeList(129, "Ghouls Bite");
            InsertCodeList(130, "Ghasts Fingers");
            InsertCodeList(131, "Ghasts Bite");
            InsertCodeList(132, "Shadows Touch");
            InsertCodeList(133, "Wights Fist");
            InsertCodeList(134, "Spectres Touch");
            InsertCodeList(135, "Zombie Crush");
            InsertCodeList(136, "Vampire Bite");
            InsertCodeList(137, "Vampire Crush");
            InsertCodeList(138, "Skeleton Melee");
            InsertCodeList(139, "Ghost Touch");
            InsertCodeList(140, "Mummy Crush");
            InsertCodeList(141, "Dragon Claws");
            InsertCodeList(142, "Dragon Bite");
            InsertCodeList(143, "Fire Breath");
            InsertCodeList(144, "Energy Bolt");
            InsertCodeList(145, "Fire Ball");
            InsertCodeList(146, "Hold");
            InsertCodeList(147, "Burning Hands");
            InsertCodeList(148, "Cure Serious");
            InsertCodeList(149, "Cure Light");
            InsertCodeList(150, "Lightning Bolt");
            InsertCodeList(151, "Raise Dead");
            InsertCodeList(152, "Light");
            InsertCodeList(153, "Turn Undead");
            InsertCodeList(154, "Repair");
            InsertCodeList(155, "Knife, Throwing");
            InsertCodeList(156, "Short Bow");
            InsertCodeList(157, "Long Bow");
            InsertCodeList(158, "Cross Bow");
            InsertCodeList(159, "Axe,Throwing");
            InsertCodeList(160, "Javelin");
            InsertCodeList(161, "Magic Speed Bow");
            InsertCodeList(162, "Magic Long Bow");
            InsertCodeList(163, "Magic Javelin");
            InsertCodeList(164, "Laser Pistol");
            InsertCodeList(165, "Scorpion");
            InsertCodeList(166, "Cobra");
            InsertCodeList(167, "Commando");
            InsertCodeList(168, "Pulse Rifle");
            InsertCodeList(169, "Laser Rifle");
            InsertCodeList(170, "Particle Beam");
            InsertCodeList(171, "Plasma Rifle");
            InsertCodeList(172, "Morita");
            InsertCodeList(173, "Plasma Gatling");
            InsertCodeList(174, "Phaser Stun");
            InsertCodeList(175, "Phaser Kill");
            InsertCodeList(176, "Disruptor");
            InsertCodeList(177, "Ray Gun");
            InsertCodeList(178, "Short Bow");
            InsertCodeList(179, "Long Bow");
            InsertCodeList(180, "Cross Bow");
            InsertCodeList(181, "Molotov Cocktail");
            InsertCodeList(182, "Nail Gun");
            InsertCodeList(183, "Sling Shot");
            InsertCodeList(184, "Taser");
            InsertCodeList(185, "Axe, Throwing");
            InsertCodeList(186, "Battle Axe");
            InsertCodeList(187, "Knife");
            InsertCodeList(188, "Unarmed Combat");
            InsertCodeList(189, "Pitch Fork");
            InsertCodeList(190, "Short Sword");
            InsertCodeList(191, "Long Sword");
            InsertCodeList(192, "War Hammer");
            InsertCodeList(193, "Torch Flame");
            InsertCodeList(194, "Halberd");
            InsertCodeList(195, "Wooden Shield");
            InsertCodeList(196, "Iron Shield");
            InsertCodeList(197, "Shield/Spike");
            InsertCodeList(198, "Magic Shield");
            InsertCodeList(199, "Ring of Prot.");
            InsertCodeList(200, "Healing Potion");
            InsertCodeList(201, "Regen. Ring");
            InsertCodeList(202, "Ring-Fire Res.");
            InsertCodeList(203, "Ring-Undead Prot");
            InsertCodeList(204, "Adrenaline");
            InsertCodeList(205, "First Aid");
            InsertCodeList(206, "Medical Pack");
            InsertCodeList(207, "Armor Repair");
            InsertCodeList(208, "Force Shield");
            InsertCodeList(209, "Regen Suit");
            InsertCodeList(210, "Saber Shield");
            InsertCodeList(211, "7.5 cm Pak 40 AP");
            InsertCodeList(212, "7.5 cm Pak 40 HE");
            InsertCodeList(213, "7.5cm Pak40 HEAT");
            InsertCodeList(214, "7.5cm L/40 M3 AP");
            InsertCodeList(215, "7.5cm L/40 M3 HE");
            InsertCodeList(216, "7.5cm L70 AP");
            InsertCodeList(217, "7.5cm L70 HE");
            InsertCodeList(218, "7.6 L55 17pdr AP");
            InsertCodeList(219, "7.6cm L55 APDS");
            InsertCodeList(220, "7.6cm L41 AP");
            InsertCodeList(221, "7.6cm L41 HE");
            InsertCodeList(222, "7.6cm L41 HVAP");
            InsertCodeList(223, "8.5cm ZiS-S-53 AP");
            InsertCodeList(224, "8.5ZiS-S-53 APCR");
            InsertCodeList(225, "8.5cm ZiS-S-53HE");
            InsertCodeList(226, "10cm L54M44 AP");
            InsertCodeList(227, "10cm L54M44 HVAP");
            InsertCodeList(228, "10cm L54M44 HE");
            InsertCodeList(229, "10.5cm L7 APDS");
            InsertCodeList(230, "10.5cm L7 HE");
            InsertCodeList(231, "10.5cm L7 HESH");
            InsertCodeList(232, "12cm L44 APFSDS");
            InsertCodeList(233, "12cm L44 Canist.");
            InsertCodeList(234, "Toxic Zombie");
            InsertCodeList(235, "Screech");
            InsertCodeList(236, "Robot-Melee");
            InsertCodeList(237, "Alien Melee");
            InsertCodeList(238, "Light Saber");
            InsertCodeList(239, "Bless");
            InsertCodeList(240, "Courage");
            InsertCodeList(241, "Silence");
            InsertCodeList(242, "Invulnerable");
            InsertCodeList(243, "Sleep");
            InsertCodeList(244, "Armor");

            InsertCodeList(245, "1842 Musket");
            InsertCodeList(246, "S. Rifle Musket");
            InsertCodeList(247, "E. Rifle Musket");
            InsertCodeList(248, "Lorenze Rifle");
            InsertCodeList(249, "Whitworth Sniper");
            InsertCodeList(250, "M1855 Carbine");
            InsertCodeList(251, "Spencer Repeating");
            InsertCodeList(252, "Henry Rifle");
            InsertCodeList(253, "Kentucky Rifle");
            InsertCodeList(254, "Sharps Rifle");
            InsertCodeList(255, "Colt Army M1860");
            InsertCodeList(256, "Colt Navy M1861");
            InsertCodeList(257, "Colt Dragoon");
            InsertCodeList(258, "Remington M1858");
            InsertCodeList(259, "M1862 Gatling");
            InsertCodeList(260, "M1841 6pdr Shot");
            InsertCodeList(261, "M1841 6pdr Case");
            InsertCodeList(262, "M1841 6pdr Canister");
            InsertCodeList(263, "M1857 Nap. Shot");
            InsertCodeList(264, "M1857 Nap. Case");
            InsertCodeList(265, "M1857 Nap. Canister");
            InsertCodeList(266, "10pdr Parr. Bolt");
            InsertCodeList(267, "10pdr Parr. Canister");
            InsertCodeList(268, "20pdr Parr. Bolt");
            InsertCodeList(269, "20pdr Parr. Caniniser");
            InsertCodeList(270, "3inch Ord. Bolt");
            InsertCodeList(271, "3inch Ord. Canister");
            InsertCodeList(272, "12pdr Whit. Bolt");
            InsertCodeList(273, "12pdr Whit. Canister");
            InsertCodeList(274, "12pdr Whit. Shell");
            InsertCodeList(275, "Felling Axe");
            InsertCodeList(276, "Bowie Knife");
            InsertCodeList(277, "Socket Bayonet");
            InsertCodeList(278, "Hammer");
            InsertCodeList(279, "Meat Cleaver");
            InsertCodeList(280, "Sledge Hammer");
            InsertCodeList(281, "Spade");
            InsertCodeList(282, "Unarmed Combat");
            InsertCodeList(283, "1840 Cav. Saber");
            InsertCodeList(284, "1860 Cav. Saber");
            InsertCodeList(285, "Blaster");
            InsertKeys();
            InsertCodeList(298, "Pick Pocket");
            InsertCodeList(299, "Find Traps");
            InsertCodeList(300, "Remove Traps");
            InsertCodeList(301, "Remove Charm");
            InsertCodeList(302, "Pacifism Charm");
            connection.Close();

        }

        private void SyncBtn_Click(object sender, EventArgs e)
        {
            ManageDevicesForm md = new ManageDevicesForm();
            md.DBConnection = DBConnection;
            md.GenreFolder = GenreFolder();
            md.GenreCode = GenreCode;
            md.BackButtonImage = BackButtonImage;

            this.Hide();
            md.StartPosition = FormStartPosition.Manual;
            md.Left = this.Left;
            md.Top = this.Top;
            md.ShowDialog();
            if (md.closeapp) this.Close();
            else
            this.Show();
        }

        private void REbuildDBBtn_Click(object sender, EventArgs e)
        {
            NewEmulationList();
            NewPhrases();
            InitialiseAliases();
            MessageBox.Show("Update Complete");
            this.Refresh();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
         
        }

        private void CloseAppBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseAppPicture_Click(object sender, EventArgs e)
        {
            Program.rf.rfPortClose();
            this.Close();
        }

        private void BootScreen_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void BootScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void BootScreen_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void InsertAlias(int AliasCode, string AliasName, int DeviceRoleCode)
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            string query = "INSERT INTO Alias VALUES (" + Convert.ToString(AliasCode)+","+ Convert.ToString(DeviceRoleCode) + ",'" + AliasName + "'); ";
            //  MessageBox.Show(query);
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
        private void InitialiseAliases()
        {

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            connection.ConnectionString = DBConnection;
            connection.Open();
            string query = "DELETE FROM ALIAS;";
            command.CommandText = query;
            command.ExecuteNonQuery();

            InsertAlias(0, "NO ALIAS", 1);
            InsertAlias(1, "GAMER", 1);
          
            InsertAlias(2, "AALIYAH", 1);
            InsertAlias(3, "AARON", 1);
            InsertAlias(4, "ABIGAIL", 1);
            InsertAlias(5, "ADAM", 1);
            InsertAlias(6, "ADDISON", 1);
            InsertAlias(7, "ADRIAN", 1);
            InsertAlias(8, "AGENT", 1);
            InsertAlias(9, "AIDEN", 1);
            InsertAlias(10, "AIDYN", 1);
            InsertAlias(11, "AJ", 1);
            InsertAlias(12, "AKUJI", 1);
            InsertAlias(13, "ALEX", 1);
            InsertAlias(14, "ALEXA", 1);
            InsertAlias(15, "ALEXIS", 1);
            InsertAlias(16, "ALICE", 1);
            InsertAlias(17, "ALLISON", 1);
            InsertAlias(18, "ALPHA", 1);
            InsertAlias(19, "ALUCARD", 1);
            InsertAlias(20, "ALYSSA", 1);
            InsertAlias(21, "AMELIA", 1);
            InsertAlias(22, "ANDREA", 1);
            InsertAlias(23, "ANDREW", 1);
            InsertAlias(24, "ANGEL", 1);
            InsertAlias(25, "ANNA", 1);
            InsertAlias(26, "ANNABELL", 1);
            InsertAlias(27, "ANTHONY", 1);
            InsertAlias(28, "APOLLO", 1);
            InsertAlias(29, "ARAN", 1);
            InsertAlias(30, "ARC", 1);
            InsertAlias(31, "ARIA", 1);
            InsertAlias(32, "ARIANA", 1);
            InsertAlias(33, "ARIANNA", 1);
            InsertAlias(34, "ARTHUR", 1);
            InsertAlias(35, "ARTHUS", 1);
            InsertAlias(36, "ASHLEY", 1);
            InsertAlias(37, "ASSASIN", 1);
            InsertAlias(38, "AUBREE", 1);
            InsertAlias(39, "AUBREY", 1);
            InsertAlias(40, "AUDREY", 1);
            InsertAlias(41, "AUZ", 1);
            InsertAlias(42, "AUSTIN", 1);
            InsertAlias(43, "AUTUMN", 1);
            InsertAlias(44, "AVA", 1);
            InsertAlias(45, "AVERY", 1);
            InsertAlias(46, "AYDEN", 1);
            InsertAlias(47, "BABAK", 1);
            InsertAlias(48, "BAILEY", 1);
            InsertAlias(49, "BANJO", 1);
            InsertAlias(50, "BATHAZAR", 1);
            InsertAlias(51, "BATTLE", 1); 
            InsertAlias(52, "BELLA", 1);
            InsertAlias(53, "BEN", 1);
            InsertAlias(54, "BENJAMIN", 1);
            InsertAlias(55, "BENTLEY", 1); 
            InsertAlias(56, "BILKO", 1);
            InsertAlias(57, "BLADE", 1);
            InsertAlias(58, "BLAKE", 1);
            InsertAlias(59, "BLOCK", 1);
            InsertAlias(60, "BLUNT", 1);
            InsertAlias(61, "BORIS", 1);
            InsertAlias(62, "BOUNTY", 1);
            InsertAlias(63, "BOWIE", 1);
            InsertAlias(64, "BOX", 1);
            InsertAlias(65, "BRAGG", 1);
            InsertAlias(66, "BRANDON", 1);
            InsertAlias(67, "BRAT", 1);
            InsertAlias(68, "BRAVO", 1);
            InsertAlias(69, "BRAYDEN", 1);
            InsertAlias(70, "BRIANNA", 1);
            InsertAlias(71, "BRODY", 1);
            InsertAlias(72, "BROOKLYN", 1);
            InsertAlias(73, "BRYSON", 1);
            InsertAlias(74, "BUFFY", 1);
            InsertAlias(75, "BUGBEAR", 1);
            InsertAlias(76, "BULL", 1);
            InsertAlias(77, "CALEB", 1);
            InsertAlias(78, "CAMERON", 1);
            InsertAlias(79, "CAMILA", 1);
            InsertAlias(80, "CARLOS", 1);
            InsertAlias(81, "CAROLINE", 1);
            InsertAlias(82, "CARSON", 1);
            InsertAlias(83, "CARTER", 1);
            InsertAlias(84, "CASPER", 1);
            InsertAlias(85, "CELGB", 1);
            InsertAlias(86, "CHAOS", 1);
            InsertAlias(87, "CHARLES", 1);
            InsertAlias(88, "CHARLIE", 1);
            InsertAlias(89, "CHARLOTT", 1);
            InsertAlias(90, "CHASE", 1);
            InsertAlias(91, "CHICKEN", 1);
            InsertAlias(92, "CHIP", 1);
            InsertAlias(93, "CHLOE", 1);
            InsertAlias(94, "CHOPPER", 1);
            InsertAlias(95, "CHRISTIA", 1);
            InsertAlias(96, "CLAIRE", 1);
            InsertAlias(97, "CLARA", 1);
            InsertAlias(98, "CLOUD", 1);
            InsertAlias(99, "COL", 1);
            InsertAlias(100, "COLTON", 1);
            InsertAlias(101, "COMRADE", 1);
            InsertAlias(102, "CONNOR", 1);
            InsertAlias(103, "COOPER", 1);
            InsertAlias(104, "COWBOY", 1);
            InsertAlias(105, "CRAFTY", 1);
            InsertAlias(106, "CRASH", 1);
            InsertAlias(107, "CROW", 1);
            InsertAlias(108, "CUB", 1);
            InsertAlias(109, "CYBORG", 1);
            InsertAlias(110, "DADDY", 1);
            InsertAlias(111, "DAMIAN", 1);
            InsertAlias(112, "DANCER", 1);
            InsertAlias(113, "DANIEL", 1);
            InsertAlias(114, "DARKWOLF", 1);
            InsertAlias(115, "DAVID", 1);
            InsertAlias(116, "DAXTER", 1);
            InsertAlias(117, "DELTA", 1);
            InsertAlias(118, "DEZZY", 1);
            InsertAlias(119, "DIEHARD", 1);
            InsertAlias(120, "DING", 1);
            InsertAlias(121, "DIRK", 1);
            InsertAlias(122, "DIZZY", 1);
            InsertAlias(123, "DOMINIC", 1);
            InsertAlias(124, "DOVE", 1);
            InsertAlias(125, "DRAKE", 1);
            InsertAlias(126, "DUKE", 1);
            InsertAlias(127, "DUTCH", 1);
            InsertAlias(128, "DYLAN", 1);
            InsertAlias(129, "EAGLE", 1);
            InsertAlias(130, "EARL", 1);
            InsertAlias(131, "EASTON", 1);
            InsertAlias(132, "ELI", 1);
            InsertAlias(133, "ELIJAH", 1);
            InsertAlias(134, "ELIZABET", 1);
            InsertAlias(135, "ELLA", 1);
            InsertAlias(136, "ELLIE", 1);
            InsertAlias(137, "EMILY", 1);
            InsertAlias(138, "EMMA", 1);
            InsertAlias(139, "EASY", 1);
            InsertAlias(140, "ETHAN", 1);
            InsertAlias(141, "EVA", 1);
            InsertAlias(142, "EVAN", 1);
            InsertAlias(143, "EVELYN", 1);
            InsertAlias(144, "FAITH", 1);
            InsertAlias(145, "FATAL", 1);
            InsertAlias(146, "FENIX", 1);
            InsertAlias(147, "FINNISH", 1);
            InsertAlias(148, "FIREELEM", 1);
            InsertAlias(149, "FLAWLESS", 1);
            InsertAlias(150, "FLEA", 1);
            InsertAlias(151, "FOO", 1);
            InsertAlias(152, "FOXTAIL", 1);
            InsertAlias(153, "GABE", 1);
            InsertAlias(154, "GABRIEL", 1);
            InsertAlias(155, "GARRETT", 1);
            InsertAlias(156, "GAVIN", 1);
            InsertAlias(157, "GECKO", 1);
            InsertAlias(158, "GENESIS", 1);
            InsertAlias(159, "GHOST", 1);
            InsertAlias(160, "GIANNA", 1);
            InsertAlias(161, "GOBLIN", 1);
            InsertAlias(162, "GHOUL", 1);
            InsertAlias(163, "GIANT", 1);
            InsertAlias(164, "GOEMON", 1);
            InsertAlias(165, "GOKU", 1);
            InsertAlias(166, "GOLIATH", 1);
            InsertAlias(167, "GOONER", 1);
            InsertAlias(168, "GRACE", 1);
            InsertAlias(169, "GRAYSON", 1);
            InsertAlias(170, "GRIM", 1);
            InsertAlias(171, "G-UNIT", 1);
            InsertAlias(172, "GUNNER", 1);
            InsertAlias(173, "HADES", 1);
            InsertAlias(174, "HAILEY", 1);
            InsertAlias(175, "HAMMER", 1);
            InsertAlias(176, "HANNAH", 1);
            InsertAlias(177, "HARMAN", 1);
            InsertAlias(178, "HARPER", 1);
            InsertAlias(179, "HAWK", 1);
            InsertAlias(180, "HENRY", 1);
            InsertAlias(181, "HERO", 1);
            InsertAlias(182, "HOBGOBLI", 1);
            InsertAlias(183, "HUDSON", 1);
            InsertAlias(184, "HUMMER", 1);
            InsertAlias(185, "HUNTER", 1);
            InsertAlias(186, "HUNTSMAN", 1);
            InsertAlias(187, "IAN", 1);
            InsertAlias(188, "ICO", 1);
            InsertAlias(189, "ILLIDAN", 1);
            InsertAlias(190, "ISAAC", 1);
            InsertAlias(191, "ISABELLA", 1);
            InsertAlias(192, "ISAIAH", 1);
            InsertAlias(193, "IZMALI", 1);
            InsertAlias(194, "JACE", 1);
            InsertAlias(195, "JACK", 1);
            InsertAlias(196, "JACKAL", 1);
            InsertAlias(197, "JACKSON", 1);
            InsertAlias(198, "JACOB", 1);
            InsertAlias(199, "JAK", 1);
            InsertAlias(200, "JAMES", 1);
            InsertAlias(201, "JASMINE", 1);
            InsertAlias(202, "JASON", 1);
            InsertAlias(203, "JAXON", 1);
            InsertAlias(204, "JAYDEN", 1);
            InsertAlias(205, "JEREMIAH", 1);
            InsertAlias(206, "JOCELYN", 1);
            InsertAlias(207, "JOEY", 1);
            InsertAlias(208, "JOHN", 1);
            InsertAlias(209, "JOJO", 1);
            InsertAlias(210, "JONATHAN", 1);
            InsertAlias(211, "JORDAN", 1);
            InsertAlias(212, "JOSE", 1);
            InsertAlias(213, "JOSEPH", 1);
            InsertAlias(214, "JOSHUA", 1);
            InsertAlias(215, "JOSIAH", 1);
            InsertAlias(216, "JUAN", 1);
            InsertAlias(217, "JUGGLO", 1);
            InsertAlias(218, "JULIA", 1);
            InsertAlias(219, "JULIAN", 1);
            InsertAlias(220, "JUSTIN", 1);
            InsertAlias(221, "KAGE", 1);
            InsertAlias(222, "KAIN", 1);
            InsertAlias(223, "KANE", 1);
            InsertAlias(224, "KATHERIN", 1);
            InsertAlias(225, "KATIE", 1);
            InsertAlias(226, "KAYDEN", 1);
            InsertAlias(227, "KAYLA", 1);
            InsertAlias(228, "KAYLEE", 1);
            InsertAlias(229, "KAZOOIE", 1);
            InsertAlias(230, "KENNEDY", 1);
            InsertAlias(231, "KEVIN", 1);
            InsertAlias(232, "KHLOE", 1);
            InsertAlias(233, "KIALOS", 1);
            InsertAlias(234, "KIMBERLY", 1);
            InsertAlias(235, "KIN", 1);
            InsertAlias(236, "KLONOA", 1);
            InsertAlias(237, "KNIGHT", 1);
            InsertAlias(238, "KOALA", 1);
            InsertAlias(239, "KONKA", 1);
            InsertAlias(240, "KOVAC", 1);
            InsertAlias(241, "KRATOS", 1);
            InsertAlias(242, "KYLIE", 1);
            InsertAlias(243, "LAMB", 1);
            InsertAlias(244, "LANDON", 1);
            InsertAlias(245, "LARA", 1);
            InsertAlias(246, "LAUREN", 1);
            InsertAlias(247, "LAYLA", 1);
            InsertAlias(248, "LC", 1);
            InsertAlias(249, "LEAH", 1);
            InsertAlias(250, "LEET", 1);
            InsertAlias(251, "LEGO", 1);
            InsertAlias(252, "LEMMING", 1);
            InsertAlias(253, "LEVI", 1);
            InsertAlias(254, "LIAM", 1);
            InsertAlias(255, "LILLIAN", 1);
            InsertAlias(256, "LILY", 1);
            InsertAlias(257, "LINK", 1);
            InsertAlias(258, "LITTLE", 1);
            InsertAlias(259, "LOBSTER", 1);
            InsertAlias(260, "LOGAN", 1);
            InsertAlias(261, "LONDON", 1);
            InsertAlias(262, "LUCAS", 1);
            InsertAlias(263, "LUCY", 1);
            InsertAlias(264, "LUIS", 1);
            InsertAlias(265, "LUKE", 1);
            InsertAlias(266, "LYDIA", 1);
            InsertAlias(267, "MADELINE", 1);
            InsertAlias(268, "MADELYN", 1);
            InsertAlias(269, "MADISON", 1);
            InsertAlias(270, "MAJORA", 1);
            InsertAlias(271, "MAKAYLA", 1);
            InsertAlias(272, "MARTY", 1);
            InsertAlias(273, "MASON", 1);
            InsertAlias(274, "MASTER", 1);
            InsertAlias(275, "MATTHEW", 1);
            InsertAlias(276, "MAXIMO", 1);
            InsertAlias(277, "MAYA", 1);
            InsertAlias(278, "MCDEATH", 1);
            InsertAlias(279, "MELANIE", 1);
            InsertAlias(280, "MERC", 1);
            InsertAlias(281, "MIA", 1);
            InsertAlias(282, "MICHAEL", 1);
            InsertAlias(283, "MINE", 1);
            InsertAlias(284, "MINI", 1);
            InsertAlias(285, "MOLLY", 1);
            InsertAlias(286, "MONSTER", 1);
            InsertAlias(287, "MORGAN", 1);
            InsertAlias(288, "MORIATY", 1);
            InsertAlias(289, "MOSES", 1);
            InsertAlias(290, "MR BLACK", 1);
            InsertAlias(291, "MUMMY", 1);
            InsertAlias(292, "MUNCH", 1);
            InsertAlias(293, "MUNROE", 1);
            InsertAlias(294, "NAMELESS", 1);
            InsertAlias(295, "NANIEL", 1);
            InsertAlias(296, "NAOMI", 1);
            InsertAlias(297, "NARISKO", 1);
            InsertAlias(298, "NATALIE", 1);
            InsertAlias(299, "NATHAN", 1);
            InsertAlias(300, "NECRO", 1);
            InsertAlias(301, "NEO", 1);
            InsertAlias(302, "NERO", 1);
            InsertAlias(303, "NEVAEH", 1);
            InsertAlias(304, "NEWTON", 1);
            InsertAlias(305, "NICHOLAS", 1);
            InsertAlias(306, "NICOLE", 1);
            InsertAlias(307, "NOAH", 1);
            InsertAlias(308, "NOD", 1);
            InsertAlias(309, "NOLAN", 1);
            InsertAlias(310, "NUKEM", 1);
            InsertAlias(311, "OLIVER", 1);
            InsertAlias(312, "OLIVIA", 1);
            InsertAlias(313, "ORC", 1);
            InsertAlias(314, "OWEN", 1);
            InsertAlias(315, "PARAPPA", 1);
            InsertAlias(316, "PARKER", 1);
            InsertAlias(317, "PASTEY", 1);
            InsertAlias(318, "PAWN", 1);
            InsertAlias(319, "PEYTON", 1);
            InsertAlias(320, "PHANTOM", 1);
            InsertAlias(321, "PHILLO", 1);
            InsertAlias(322, "PHOENIX", 1);
            InsertAlias(323, "PIPER", 1);
            InsertAlias(324, "PIRANHA", 1);
            InsertAlias(325, "PIZZABOY", 1);
            InsertAlias(326, "PLAN", 1);
            InsertAlias(327, "PLO", 1);
            InsertAlias(328, "POLLARD", 1);
            InsertAlias(329, "PREDATOR", 1);
            InsertAlias(330, "PYTHON", 1);
            InsertAlias(331, "RABBIT", 1);
            InsertAlias(332, "RAIDEN", 1);
            InsertAlias(333, "RANGER", 1);
            InsertAlias(334, "RAT", 1);
            InsertAlias(335, "RATTIS", 1);
            InsertAlias(336, "RAVEN", 1);
            InsertAlias(337, "REAGAN", 1);
            InsertAlias(338, "REAPER", 1);
            InsertAlias(339, "REPO", 1);
            InsertAlias(340, "RHINO", 1);
            InsertAlias(341, "RILEY", 1);
            InsertAlias(342, "ROB", 1);
            InsertAlias(343, "ROBERT", 1);
            InsertAlias(344, "ROGUE", 1);
            InsertAlias(345, "ROPEY", 1);
            InsertAlias(346, "RYAN", 1);
            InsertAlias(347, "RYDER", 1);
            InsertAlias(348, "RYGAR", 1);
            InsertAlias(349, "RYU", 1);
            InsertAlias(350, "SABRE", 1);
            InsertAlias(351, "SAGO", 1);
            InsertAlias(352, "SALMON", 1);
            InsertAlias(353, "SAMANTHA", 1);
            InsertAlias(354, "SAMOS", 1);
            InsertAlias(355, "SAMUEL", 1);
            InsertAlias(356, "SARAH", 1);
            InsertAlias(357, "SAVANNAH", 1);
            InsertAlias(358, "SCARLETT", 1);
            InsertAlias(360, "SCRAPZ", 1);
            InsertAlias(361, "SEBASTAN", 1);
            InsertAlias(362, "SERENITY", 1);
            InsertAlias(363, "SHADE", 1);
            InsertAlias(364, "SHADOW", 1);
            //InsertAlias(364, "SHODUN", 1);
            InsertAlias(365, "SIREN", 1);
            InsertAlias(366, "SKELETON", 1);
            InsertAlias(367, "SKIPPY", 1);
            InsertAlias(368, "SKYLAR", 1);
            InsertAlias(369, "SLATER", 1);
            InsertAlias(370, "SMAUG", 1);
            InsertAlias(371, "SMITH", 1);
            InsertAlias(372, "SMITHY", 1);
            InsertAlias(373, "SNAKE", 1);
            InsertAlias(374, "SOFIA", 1);
            InsertAlias(375, "SOOK", 1);
            InsertAlias(376, "SOPHIA", 1);
            InsertAlias(377, "SOPHIE", 1);
            InsertAlias(378, "SPACEMAN", 1);
            InsertAlias(379, "SPARKE", 1);
            InsertAlias(380, "SPARROW", 1);
            InsertAlias(381, "SPENG", 1);
            InsertAlias(382, "SPOOKY", 1);
            InsertAlias(383, "SPYRO", 1);
            InsertAlias(384, "SQUALL", 1);
            InsertAlias(385, "SQUEAKY", 1);
            InsertAlias(386, "STALK", 1);
            InsertAlias(387, "STALKER", 1);
            InsertAlias(388, "STAR", 1);
            InsertAlias(389, "STELLA", 1);
            InsertAlias(390, "STRIFE", 1);
            InsertAlias(391, "SYDNEY", 1);
            InsertAlias(392, "SYREN", 1);
            InsertAlias(393, "TAIPAN", 1);
            InsertAlias(394, "TAK", 1);
            InsertAlias(395, "TAYLOR", 1);
            InsertAlias(396, "TBA", 1);
            InsertAlias(397, "TECH", 1);
            InsertAlias(398, "TERM", 1);
            InsertAlias(399, "THEMAN", 1);
            InsertAlias(400, "THOMAS", 1);
            InsertAlias(401, "THOR", 1);
            InsertAlias(402, "TIDUS", 1);
            InsertAlias(403, "TINY", 1);
            InsertAlias(404, "TOAN", 1);
            InsertAlias(405, "TOFU", 1);
            InsertAlias(406, "TOM", 1);
            InsertAlias(407, "TOWNY", 1);
            InsertAlias(408, "TRINITY", 1);
            InsertAlias(409, "TRISTAN", 1);
            InsertAlias(410, "TURTLE", 1);
            InsertAlias(411, "TWINKLE", 1);
            InsertAlias(412, "TYLER", 1);
            InsertAlias(413, "VAMPIRE", 1);
            InsertAlias(414, "VICTORIA", 1);
            InsertAlias(415, "VIOLET", 1);
            InsertAlias(416, "VIPER", 1);
            InsertAlias(417, "VIRUS", 1);
            InsertAlias(418, "VYSE", 1);
            InsertAlias(420, "WALLY", 1);
            InsertAlias(421, "WARDON", 1);
            InsertAlias(422, "WASP", 1);
            InsertAlias(423, "WEAPON", 1);
            InsertAlias(424, "WEREWOLF", 1);
            InsertAlias(425, "WEEMAN", 1);
            InsertAlias(426, "WEETBIX", 1);
            InsertAlias(427, "WENIZAN", 1);
            InsertAlias(428, "WILD CAT", 1);
            InsertAlias(429, "WILLIAM", 1);
            InsertAlias(430, "WILLY", 1);
            InsertAlias(431, "WINGNUT", 1);
            InsertAlias(432, "WISP", 1);
            InsertAlias(433, "WOLF", 1);
            InsertAlias(434, "WRAITH", 1);
            InsertAlias(435, "WUSCHKE", 1);
            InsertAlias(436, "WYATT", 1);
            InsertAlias(437, "XAVIER", 1);
            InsertAlias(438, "XENA", 1);
            InsertAlias(439, "XENIA", 1);
            InsertAlias(440, "YEAH MAN", 1);
            InsertAlias(441, "YOSHI", 1);
            InsertAlias(442, "ZABRYNN", 1);
            InsertAlias(443, "ZACHARY", 1);
            InsertAlias(444, "ZAIA", 1);
            InsertAlias(445, "ZELL", 1);
            InsertAlias(446, "ZEUS", 1);
            InsertAlias(447, "ZEV-VA", 1);
            InsertAlias(448, "ZIDAN", 1);
            InsertAlias(449, "ZOE", 1);
            InsertAlias(450, "ZOEY", 1);
            InsertAlias(451, "ZOMBIE", 1);
            InsertAlias(452, "ZOOLAND", 1);

            InsertAlias(453, "AMMOBOX1", 5);
            InsertAlias(454, "AMMOBOX2", 5);
            InsertAlias(455, "AMMOBOX3", 5);
            InsertAlias(456, "AMMOBOX4", 5);
            InsertAlias(457, "AMMOBOX5", 5);
            InsertAlias(458, "AMMOBOX6", 5);
            InsertAlias(459, "AMMOBOX7", 5);
            InsertAlias(460, "ARMOR 1", 7);
            InsertAlias(461, "ARMOR 2", 7);
            InsertAlias(462, "ARMOR 3", 7);
            InsertAlias(463, "ARMOR 4", 7);
            InsertAlias(464, "ARMOR 5", 7);
            InsertAlias(465, "ARMOR 6", 7);
            InsertAlias(466, "ARMOR 7", 7);
            InsertAlias(467, "BOMBSIT1", 17);
            InsertAlias(468, "BOMBSIT2", 17);
            InsertAlias(469, "BOMBSIT3", 17);
            InsertAlias(470, "BOMBSIT4", 17);
            InsertAlias(471, "BOMBSIT5", 17);
            InsertAlias(472, "BOMBSIT6", 17);
            InsertAlias(473, "BOMBSIT7", 17);
            InsertAlias(474, "BOX 1", 0);
            InsertAlias(475, "BOX 2", 0);
            InsertAlias(476, "BOX 3", 0);
            InsertAlias(477, "BOX 4", 0);
            InsertAlias(478, "BOX 5", 0);
            InsertAlias(479, "COMBO 1", 10);
            InsertAlias(480, "COMBO 2", 10);
            InsertAlias(481, "COMBO 3", 10);
            InsertAlias(482, "COMBO 4", 10);
            InsertAlias(483, "COMBO 5", 10);
            InsertAlias(484, "COMBO 6", 10);
            InsertAlias(485, "COMBO 7", 10);
            InsertAlias(486, "CONTROL1", 3);
            InsertAlias(487, "CONTROL2", 3);
            InsertAlias(488, "CONTROL3", 3);
            InsertAlias(489, "CONTROL4", 3);
            InsertAlias(490, "CONTROL5", 3);
            InsertAlias(491, "CONTROL6", 3);
            InsertAlias(492, "CONTROL7", 3);
            InsertAlias(493, "CONTROL8", 3);
            InsertAlias(494, "CURE 1", 13);
            InsertAlias(495, "CURE 2", 13);
            InsertAlias(496, "CURE 3", 13);
            InsertAlias(497, "CURE 4", 13);
            InsertAlias(498, "CURE 5", 13);
            InsertAlias(499, "CURE 6", 13);
            InsertAlias(500, "CURE 7", 13);
            InsertAlias(501, "DOMBOX 1", 12);
            InsertAlias(502, "DOMBOX 2", 12);
            InsertAlias(503, "DOMBOX 3", 12);
            InsertAlias(504, "DOMBOX 4", 12);
            InsertAlias(505, "DOMBOX 5", 12);
            InsertAlias(506, "FLAGBOX1", 16);
            InsertAlias(507, "FLAGBOX2", 16);
            InsertAlias(508, "FLAGBOX3", 16);
            InsertAlias(509, "FLAGBOX4", 16);
            InsertAlias(510, "FLAGBOX5", 16);
            InsertAlias(511, "FLAGBOX6", 16);
            InsertAlias(512, "FLAGBOX7", 16);
            InsertAlias(513, "MEDIC 1", 4);
            InsertAlias(514, "MEDIC 2", 4);
            InsertAlias(515, "MEDIC 3", 4);
            InsertAlias(516, "MEDIC 4", 4);
            InsertAlias(517, "MEDIC 5", 4);
            InsertAlias(518, "MEDIC 6", 4);
            InsertAlias(519, "MEDIC 7", 4);
            InsertAlias(520, "MINE 1", 8);
            InsertAlias(521, "MINE 2", 8);
            InsertAlias(522, "MINE 3", 8);
            InsertAlias(523, "MINE 4", 8);
            InsertAlias(524, "MINE 5", 8);
            InsertAlias(525, "MINE 6", 8);
            InsertAlias(526, "MINE 7", 8);
            InsertAlias(527, "MISSION1", 26);
            InsertAlias(528, "MISSION2", 26);
            InsertAlias(529, "MISSION3", 26);
            InsertAlias(530, "MISSION4", 26);
            InsertAlias(531, "MISSION5", 26);
            InsertAlias(532, "MISSION6", 26);
            InsertAlias(533, "MISSION7", 26);
            InsertAlias(534, "MONEY 1", 28);
            InsertAlias(535, "MONEY 2", 28);
            InsertAlias(536, "MONEY 3", 28);
            InsertAlias(537, "MONEY 4", 28);
            InsertAlias(538, "MYSTERY1", 15);
            InsertAlias(539, "MYSTERY2", 15);
            InsertAlias(540, "MYSTERY3", 15);
            InsertAlias(541, "MYSTERY4", 15);
            InsertAlias(542, "MYSTERY5", 15);
            InsertAlias(543, "MYSTERY6", 15);
            InsertAlias(544, "MYSTERY7", 15);
            InsertAlias(545, "PERK 1", 18);
            InsertAlias(546, "PERK 2", 18);
            InsertAlias(547, "PERK 3", 18);
            InsertAlias(548, "PERK 4", 18);
            InsertAlias(549, "PERK 5", 18);
            InsertAlias(550, "PERK 6", 18);
            InsertAlias(551, "PERK 7", 18);
            InsertAlias(552, "RUSH 1", 25);
            InsertAlias(553, "RUSH 2", 25);
            InsertAlias(554, "RUSH 3", 25);
            InsertAlias(555, "RUSH 4", 25);
            InsertAlias(556, "RUSH 5", 25);
            InsertAlias(557, "RUSH 6", 25);
            InsertAlias(558, "RUSH 7", 25);
            InsertAlias(559, "VAULT 1", 27);
            InsertAlias(560, "VAULT 2", 27);
            InsertAlias(561, "VAULT 3", 27);
            InsertAlias(562, "VAULT 4", 27);
            InsertAlias(563, "VAULT 5", 27);
            InsertAlias(564, "VAULT 6", 27);
            InsertAlias(565, "VAULT 7", 27);
            InsertAlias(566, "WEAPON 1", 14);
            InsertAlias(567, "WEAPON 2", 14);
            InsertAlias(568, "WEAPON 3", 14);
            InsertAlias(569, "WEAPON 4", 14);
            InsertAlias(570, "WEAPON 5", 14);
            InsertAlias(571, "WEAPON 6", 14);
            InsertAlias(572, "WEAPON 7", 14);
            connection.Close();

        }

        private void GameTimeBtn_Click(object sender, EventArgs e)
        {

        }

        private void CloseTip_Popup(object sender, PopupEventArgs e)
        {

        }
    }        
}
