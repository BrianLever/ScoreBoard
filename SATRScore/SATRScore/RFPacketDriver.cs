using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.Management;      //need System.Management package

using System.Data.OleDb;
using System.Data;
using System.IO;

namespace SATRScore
{
   
    public class RFPacketDriver
    {
        const int retransmit_limit = 3;
        const int PACKET_LEN = 44;
        const char SOB = 'D';
        const char SOID = 'I';
        const char SOCH = 'H';
        const char SODB = 'C';
        const char SOTS = 'T';
        const char SOBOOT = 'K';
        const char SOGEN = 'G';
        const char EOB = '\n';
        const int SOH = 01;
        const int EOT = 04;
        const int DLE = 16;

        const int RX_MODE = 0;
        const int TX_MODE = 1;

        private int countReconnect = 5;
        private bool WaitComplete = false;

        public struct _RADIO_PACKET
        {
            public byte CommandCode;
            public byte BattleCode;
            public UInt16 SenderID;
            public UInt16 ReceiverID;
            public byte ByteParameter1;
            public byte ByteParameter2;
            public UInt16 IntegerParameter1;
            public UInt16 IntegerParameter2;
            public UInt16 IntegerParameter3;
            public UInt16 IntegerParameter4;
            public UInt16 IntegerParameter5;
            public UInt16 IntegerParameter6;
            public UInt16 IntegerParameter7;
            public char[] StringParameter;//18 bytes
            public uint CRC32; //4 bytes
        }; // 44bytes

        public System.IO.Ports.SerialPort serialPort1;
        private OleDbConnection connection = new OleDbConnection();
        public void WriteErrorLog(string textline)
        {
            using (StreamWriter sw = File.AppendText("SATRScoreErrorLog.txt"))

            //using (StreamWriter sw = new StreamWriter("SATRScoreErrorLog.txt"))
            {
                DateTime localDate = DateTime.Now;

                string s = localDate.ToLongTimeString() + ": " + textline;
                sw.WriteLine(s);

                sw.Close();
            }
        }
        public void UnlockDisplay()
        {
            try
            {
                Connect_OLE();
                OleDbCommand command = new OleDbCommand();

                command.Connection = connection;
                command.CommandText = "UPDATE rfUSB SET state = 0 WHERE connector='display' ; ";
                command.ExecuteNonQuery();
                CloseDBConnect();
                command.Dispose();

            }
            catch (Exception ex)
            {
              
                MessageBox.Show("Unlock Display" + ex.Message);
                WriteErrorLog("Unlock Display" + ex.Message);
                return;
            }
        }
        private void WaitForDisplayTimer_Tick(object sender, EventArgs e)
        {
           
            Program.rf.updateSelfConnect(1);// request rf connection by admin to Display

            int isDisplayConnected = Program.rf.getDisplayConnected(); // wait unless disconnect from ScoreDisplay


            if (isDisplayConnected == 1)//Display still connected so try again up to 5 times
            {

                countReconnect--;
                if (countReconnect == 0)
                {

                    MessageBox.Show("Display not released USB RF module ", "Message");
                    WriteErrorLog("Display not released USB RF module +");

                    Program.rf.updateSelfConnect(0);   //admin disconnected.
                    WaitComplete = true;
                }
                else
                    return;
            }
            else
            {
                WaitComplete = true;
            }

        }

        public bool rfPortOpen(bool SkipDisplayWait)
        {
            if (!SkipDisplayWait) 
                if (serialPort1.IsOpen)
                 return true;

            string portname = "";
            string port_state;
            string device_name;

            try
            {
                //Disconnected Display from RF PORT.
                if (!SkipDisplayWait)
                {
              //      MessageBox.Show("Wait for Display Release");
                    WaitComplete = false;
                    Timer WaitForDisplayTimer = new Timer();
                    WaitForDisplayTimer.Interval = 1000;
                    WaitForDisplayTimer.Tick += new EventHandler(WaitForDisplayTimer_Tick);
                    WaitForDisplayTimer.Start();
                    while (!WaitComplete)
                    {
                        Application.DoEvents();
                    };
                    WaitForDisplayTimer.Stop();
                   //if (Program.rf.getDisplayConnected() == 0) MessageBox.Show("Displayed Released");
                }

                ManagementObjectCollection ManObjReturn;
                ManagementObjectSearcher ManObjSearch;
                ManObjSearch = new ManagementObjectSearcher("Select * from Win32_PnPEntity WHERE Caption like '%(COM%'");
                ManObjReturn = ManObjSearch.Get();

                foreach (ManagementObject ManObj in ManObjReturn)
                {
                    //int s = ManObj.Properties.Count;
                    //foreach (PropertyData d in ManObj.Properties)
                    //{
                    //    MessageBox.Show(d.Name);
                    //}
                    //MessageBox.Show(ManObj["DeviceID"].ToString());
                    //MessageBox.Show(ManObj["PNPDeviceID"].ToString());
                    //MessageBox.Show(ManObj["Name"].ToString());
                    //MessageBox.Show(ManObj["Caption"].ToString());
                    //MessageBox.Show(ManObj["Description"].ToString());
                    //MessageBox.Show(ManObj["ProviderType"].ToString());
                    //MessageBox.Show(ManObj["Status"].ToString());
                    device_name = ManObj["Name"].ToString();
                    port_state = ManObj["Status"].ToString();
                    if (port_state == "OK")
                    {
                        int pos = device_name.IndexOf("USB-SERIAL");
                        if (pos >= 0 || (device_name.IndexOf("USB to UART") >= 0))
                        {
                            portname = device_name.Substring(device_name.IndexOf("COM"), 5);
                            if (portname.Substring(4, 1) == ")")
                                portname = portname.Substring(0, 4);
                            break;
                        }
                    }
                    portname = "";
                }
                if (portname == "")
                {
                    //MessageBox.Show("Failed to connect RF USB-232 Module");
                    WriteErrorLog("Failed to connect RF USB-232 Module");
                    return false;
                }
                serialPort1 = new SerialPort();
                serialPort1.PortName = portname; //"COM10";
                serialPort1.BaudRate = 115200;
                //serialPort1.ReadBufferSize = 45;
                //serialPort1.WriteBufferSize = 45;
                //serialPort1.ReceivedBytesThreshold = PACKET_LEN+1;
                serialPort1.ReadTimeout = 5000;
                serialPort1.WriteTimeout = 5000;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);

                if (serialPort1.IsOpen)
                    serialPort1.Close();
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open."+ ex.Message);
                WriteErrorLog("Failed to open." + ex.Message);
                return false;
            }
            return true;
        }
        public void rfPortClose()
        {
            //MessageBox.Show("Port Closed");
            if (serialPort1.IsOpen)
                serialPort1.Close();
            updateSelfConnect(0);
        }

        int preReadBytes = 0;
        int isFullPacket = 0;
        byte[] Rx_buffer = new byte[0xffff];
        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen == false) return;

            //Initialize a buffer to hold the received data 
            byte[] buffer = new byte[serialPort1.ReadBufferSize];

            try
            {

                //There is no accurate method for checking how many bytes are read 
                //unless you check the return from the Read method 
                int bytesRead = serialPort1.Read(buffer, 0, serialPort1.ReadBufferSize);

                //=========== To full buffer =====================================================
                if (bytesRead > PACKET_LEN)
                {
                    System.Buffer.BlockCopy(buffer, 0, Rx_buffer, 0, bytesRead);
                    isFullPacket = 1;
                    preReadBytes = 0;
                }
                else
                {
                    System.Buffer.BlockCopy(buffer, 0, Rx_buffer, preReadBytes, bytesRead);

                    if (preReadBytes + bytesRead > PACKET_LEN)
                    {
                        isFullPacket = 1;
                        preReadBytes = 0;
                    }
                    else
                    {
                        isFullPacket = 0;
                        preReadBytes = bytesRead;
                    }
                }

                //==================================================
                if (isFullPacket == 1)// (bytesRead >= PACKET_LEN)
                {
                    uint crc32 = CRC32(Rx_buffer, (byte)PACKET_LEN, RX_MODE);
                    if (crc32 == 0)
                    {
                        ReceivedPacket(Rx_buffer, PACKET_LEN);
                    }
                    isFullPacket = 0;
                    preReadBytes = 0;
                }//==================================================

                System.Threading.Thread.Sleep(200);
            }
            catch (Exception ex)
            {
                WriteErrorLog("DataReceived error." + ex.Message);
                //MessageBox.Show("DataReceived error." + ex.Message);
            }
        }

        //=================== Radio Packet Receive =================================================================================
        public void ReceivedPacket(byte[] buff, int size)
        {
            byte[] RxBuff = new byte[PACKET_LEN];

            if (size >= PACKET_LEN)
            {
                RxBuff = buff;

                char[] strPara = new char[18];

                _RADIO_PACKET radio;
                radio.CommandCode = RxBuff[0];
                radio.BattleCode = RxBuff[1];
                radio.SenderID = (UInt16)(RxBuff[3] * 0x100 + RxBuff[2]);
                radio.ReceiverID = (UInt16)(RxBuff[5] * 0x100 + RxBuff[4]);
                radio.ByteParameter1 = RxBuff[6];
                radio.ByteParameter2 = RxBuff[7];
                radio.IntegerParameter1 = (UInt16)(RxBuff[9] * 0x100 + RxBuff[8]);
                radio.IntegerParameter2 = (UInt16)(RxBuff[11] * 0x100 + RxBuff[10]);
                radio.IntegerParameter3 = (UInt16)(RxBuff[13] * 0x100 + RxBuff[12]);
                radio.IntegerParameter4 = (UInt16)(RxBuff[15] * 0x100 + RxBuff[14]);
                radio.IntegerParameter5 = (UInt16)(RxBuff[17] * 0x100 + RxBuff[16]);
                radio.IntegerParameter6 = (UInt16)(RxBuff[19] * 0x100 + RxBuff[18]);
                radio.IntegerParameter7 = (UInt16)(RxBuff[21] * 0x100 + RxBuff[20]);

                radio.StringParameter = strPara;
                int ind = 0;
                for (ind = 0; ind < 18; ind++)
                {
                    if (RxBuff[ind + 22] == 0) break;
                    strPara[ind] = (char)RxBuff[ind + 22];
                }

                string alias = new string(radio.StringParameter, 0, ind);

                Program.ReceiveRadioPacket(radio.CommandCode, radio.BattleCode, radio.SenderID, radio.ReceiverID, radio.ByteParameter1,
                    radio.IntegerParameter1, radio.IntegerParameter2, radio.IntegerParameter3, radio.IntegerParameter4, radio.IntegerParameter5, radio.IntegerParameter6, radio.IntegerParameter7,
                    radio.ByteParameter2, alias);
            }
        }
        //=================== Radio Packet Sending =================================================================================
        public void rfSendPacket(int DBLevel, int CommandCode, Int16 BattleCode, int SenderID, int ReceiverID, Int16 ByteParameter1,
            int IntegerParameter1, int IntegerParameter2, int IntegerParameter3, int IntegerParameter4, int IntegerParameter5,
            int IntegerParameter6, int IntegerParameter7, Int16 ByteParameter2, string StringParameter)
        {
            //            SenderID = ReceiverID;

            if (serialPort1.IsOpen == false) return;

            char[] strPara = new char[18];
            byte[] TxBuff = new byte[PACKET_LEN + 2]; //[0]-data Length, [1]-'D':data command, [3->n]-data array

            int index = 0;
            TxBuff[index++] = PACKET_LEN + 1;
            TxBuff[index++] = (byte)'D';  //data command character

            TxBuff[index++] = (byte)CommandCode;
            TxBuff[index++] = (byte)BattleCode;

            TxBuff[index++] = (byte)(SenderID & 0xff);
            TxBuff[index++] = (byte)((SenderID & 0xff00) >> 8);
            TxBuff[index++] = (byte)(ReceiverID & 0xff);
            TxBuff[index++] = (byte)((ReceiverID & 0xff00) >> 8);

            TxBuff[index++] = (byte)ByteParameter1;
            TxBuff[index++] = (byte)ByteParameter2;

            TxBuff[index++] = (byte)(IntegerParameter1 & 0xff);
            TxBuff[index++] = (byte)((IntegerParameter1 & 0xff) >> 8);

            TxBuff[index++] = (byte)(IntegerParameter2 & 0xff);
            TxBuff[index++] = (byte)((IntegerParameter2 & 0xff) >> 8);

            TxBuff[index++] = (byte)(IntegerParameter3 & 0xff);
            TxBuff[index++] = (byte)((IntegerParameter3 & 0xff) >> 8);

            TxBuff[index++] = (byte)(IntegerParameter4 & 0xff);
            TxBuff[index++] = (byte)((IntegerParameter4 & 0xff) >> 8);

            TxBuff[index++] = (byte)(IntegerParameter5 & 0xff);
            TxBuff[index++] = (byte)((IntegerParameter5 & 0xff) >> 8);

            TxBuff[index++] = (byte)(IntegerParameter6 & 0xff);
            TxBuff[index++] = (byte)((IntegerParameter6 & 0xff) >> 8);

            TxBuff[index++] = (byte)(IntegerParameter7 & 0xff);
            TxBuff[index++] = (byte)((IntegerParameter7 & 0xff) >> 8);

            strPara = StringParameter.ToCharArray();
            for (int i = 0; i < 18; i++)
            {
                if (i < strPara.Length)
                    TxBuff[index++] = (byte)strPara[i];
                else
                    TxBuff[index++] = 0;
            }
            TxBuff[PACKET_LEN + 2 - 1] = 0;
            TxBuff[PACKET_LEN + 2 - 2] = 0;
            TxBuff[PACKET_LEN + 2 - 3] = 0;
            TxBuff[PACKET_LEN + 2 - 4] = 0;

            //------- Adding the CRC32 Checking Code ---------------
            uint crc32 = CRC32(TxBuff, PACKET_LEN, TX_MODE);
            TxBuff[PACKET_LEN + 2 - 1] = (byte)(crc32 & 0xFF);
            TxBuff[PACKET_LEN + 2 - 2] = (byte)((crc32 >> 8) & 0xFF);
            TxBuff[PACKET_LEN + 2 - 3] = (byte)((crc32 >> 16) & 0xFF);
            TxBuff[PACKET_LEN + 2 - 4] = (byte)((crc32 >> 24) & 0xFF);

            crc32 = CRC32(TxBuff, PACKET_LEN, TX_MODE);     // must be crc32 = 0 when Received

            //TxBuff[0] = PACKET_LEN ;


            //------- Write the data buffer to 232 port -------------
            //serialPort1.ReadExisting();
            for (int i = 0; i < retransmit_limit; i++)
            {
                serialPort1.Write(TxBuff, 0, PACKET_LEN + 2);
                System.Threading.Thread.Sleep(500);//Wait 500 milleseconds before resending again to allow for response
            }
        }


        // CRC : Cyclic Redundancy Check=========================================================================================
        public const uint CRC_DIVISOR = 0x93939393;   //0b10010011100100111001001110010011; // for CRC-32
        public const uint CRC_MAX_MSb = 0x80000000;  //0b10000000000000000000000000000000;

        public uint CRC32(byte[] buff, byte size, int mode = RX_MODE)//mode = 0; Rx,          mode=1; Tx
        {
            int startPos = 0;
            if (mode == TX_MODE) startPos = 2;  //Remove 2 bytes for length and command byte
            uint remainder = 0; // remainder for transmitter or
            // syndrome for receiver
            byte val, mask;
            for (int i = startPos; i < startPos + size; i++)
            {
                val = buff[i];
                mask = 0x80;
                while (mask != 0)
                {
                    remainder <<= 1;
                    if ((val & mask) != 0)
                        remainder |= 1;
                    if ((remainder & CRC_MAX_MSb) != 0)
                        remainder ^= CRC_DIVISOR;
                    mask >>= 1;
                }
            }
            return remainder;
        }

        /**
          * Static table used for the table_driven implementation.
          *****************************************************************************/
        private readonly ushort[] crc_table = { 0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50a5, 0x60c6, 0x70e7, 0x8108, 0x9129, 0xa14a, 0xb16b, 0xc18c, 0xd1ad, 0xe1ce, 0xf1ef };

        /****************************************************************************
        * Update the crc value with new data.
        *
        * \param crc      The current crc value.
        * \param data     Pointer to a buffer of \a data_len bytes.
        * \param len		Number of bytes in the \a data buffer.
        * \return         The updated crc value.
        *****************************************************************************/
        private ushort CalculateCrc(byte[] data, uint len)
        {
            uint i;
            ushort crc = 0;
            uint index = 0;

            while (len-- != 0)
            {
                i = (uint)((crc >> 12) ^ (data[index] >> 4));
                crc = (ushort)(crc_table[i & 0x0F] ^ (crc << 4));
                i = (uint)((crc >> 12) ^ (data[index] >> 0));
                crc = (ushort)(crc_table[i & 0x0F] ^ (crc << 4));
                index++;
            }

            return (ushort)(crc & 0xFFFF);
        }
        private void OpenDBConnect()
        {
            if (connection != null && connection.State == ConnectionState.Closed)
                connection.Open();
        }

        private void CloseDBConnect()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();

        }
        




        //----------- OLE --------------------------------------------------------------
        public void Connect_OLE()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = Program.ConnectionString;
                OpenDBConnect();
            }
        }
        public void updateSelfConnect(int state)
        {
            try
            {
              //  if (state == 0)
              //      MessageBox.Show("Admin  Released");
              //  else
             //       MessageBox.Show("Admin Connect");
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                OpenDBConnect();
                command.CommandText = "UPDATE rfUSB SET state = " + state.ToString() + " WHERE connector='admin' ; ";
                command.ExecuteNonQuery();
                command.Dispose();
                CloseDBConnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update connection FAIL "+ ex.Message);
                WriteErrorLog("Update connection FAIL " + ex.Message);
                return;
            }
        }
        public int getAdminConnected()
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            OpenDBConnect();
            command.CommandText = "SELECT State FROM rfUSB WHERE connector='admin' ;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int isConnected = reader.GetByte(0);
                reader.Close();
                CloseDBConnect();
                return isConnected;
            }
            else
                MessageBox.Show("Failed to read connector state from DB");
            CloseDBConnect();
            return 0;
        }

        public int getDisplayConnected()
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            OpenDBConnect();

            command.CommandText = "SELECT State FROM rfUSB WHERE connector='display' ;";
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int isConnected = reader.GetByte(0);
                reader.Close();
                CloseDBConnect();
                return isConnected;
            }
            else
                MessageBox.Show("Failed to read connetor state from DB");
            CloseDBConnect();
            return 0;
        }
        //------------------------------------------------------------------------------


    }
}
