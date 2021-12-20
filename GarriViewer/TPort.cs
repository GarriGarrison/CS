using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPort
{
    class TPort
    {
    }

    public static class DataProc
    {
        public const int PACKET_FRAMES = 3, FBANDS = 20, CHANNELS = 4, DEFAULT_HISTORY = 15;
        public const int PACKET_INTERVAL_MS = 100 * PACKET_FRAMES / 5;
        public static readonly byte[] START_SEQ = { 0xAA };  //{ 0xAA, 0xAA};
        public static readonly byte[] STOP_SEQ = { 0xBB, 0xBB };

        public static byte[] packet;

        public static bool alarm = false;
        public static int alarm_frames;
        public static bool[,] alarms = new bool[PACKET_FRAMES, CHANNELS];

        static DataProc()
        {
            packet = new byte[PACKET_FRAMES];
        }

        public static void ProcessPacket(int data_idx)
        {
       
            if (packet[data_idx] == 0x00)
                alarm = false;
            else
                alarm = true;

        }
    }
}
