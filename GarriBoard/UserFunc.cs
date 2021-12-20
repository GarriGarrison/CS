using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GarriBoard
{
    public struct flag
    {
        public static bool AutoMarker;
        public static bool AutoWork;
        
        //public static bool isResetAlarm;  //флаг успешного сброса тревог
        //public static bool isNewState;  //флаг успешной установки нового состояния
        public static bool isState0, isState1, isState2;
    }
    public static class DeviceUses
    {
        public stфatic UInt16 state;
        public static int status;

        public static UInt16 address;
        /* Общая информация об устройстве */
        public static string factoryName;
        public static string deviceCode;
        public static string version;
        public static string email;
        public static string deviceName;
        public static string deviceModel;
        public static string serialNumer;
        /* Конфигурация устройства */
        public static int readyTime;
        public static int sensExternal;
        public static int sensAdditional;
        public static int qntRelay;
        public static int qntDeviceParams;
        public static int qntSector;
        public static int qntSectorParms;
        public static int qntAlarmCodes;
        public static int qntFaultyCodes;

        /* Описатели */
        public static Handles[] handlDevice = new Handles[32];
        public static Handles[,] handlSector = new Handles[16, 128];
        public static string[] addSensHandle = new string[4];
        public static string[] alarmHandle = new string[16];
        public static string[] faultyHandle = new string[16];


        public static string ConvertStr(byte[] array, int offset, int count)
        {
            string str = "";

            for (int i = offset; i < count + offset; i++)
                str += convertByte(array[i]);

            return str;
        }


        public struct Handles
        {
            public int hType;
            public int hAccess;
            public int maxValue;
            public int minValue;
            public int defaultValue;
            public int value;
            public int newValue;
            public string hName;
        }



        private static string convertByte(byte data)
        {
            string str = "";
            switch (data)
            {
                case 0x00:  str = "";  break;  //NOP
                case 0x01:  str = "SOH";  break;
                case 0x02:  str = "STX"; break;
                case 0x03:  str = "ETX"; break;
                case 0x04:  str = "EOT"; break;
                case 0x05:  str = "ENQ"; break;
                case 0x06:  str = "ACK"; break;
                case 0x07:  str = "BEL"; break;
                case 0x08:  str = "BS"; break;
                case 0x09:  str = "TAB"; break;
                case 0x0A:  str = "LF"; break;
                case 0x0B:  str = "VT"; break;
                case 0x0C:  str = "FF"; break;
                case 0x0D:  str = "CR"; break;
                case 0x0E:  str = "SO"; break;
                case 0x0F:  str = "SI"; break;
                case 0x10:  str = "DLE"; break;
                case 0x11:  str = "DC1"; break;
                case 0x12:  str = "DC2"; break;
                case 0x13:  str = "DC3"; break;
                case 0x14:  str = "DC4"; break;
                case 0x15:  str = "NAC"; break;
                case 0x16:  str = "SYN"; break;
                case 0x17:  str = "ETB"; break;
                case 0x18:  str = "CAN"; break;
                case 0x19:  str = "EM"; break;
                case 0x1A:  str = "SUB"; break;
                case 0x1B:  str = "ESC"; break;
                case 0x1C:  str = "FS"; break;
                case 0x1D:  str = "GS"; break;
                case 0x1E:  str = "RS"; break;
                case 0x1F:  str = "US"; break;
                case 0x20: str = " "; break;  //SP пробел
                case 0x21: str = "!"; break;
                case 0x22: str = "\""; break;
                case 0x23: str = "#"; break;
                case 0x24: str = "$"; break;
                case 0x25: str = "%"; break;
                case 0x26: str = "&"; break;
                case 0x27: str = "`"; break;
                case 0x28: str = "("; break;
                case 0x29: str = ")"; break;
                case 0x2A: str = "*"; break;
                case 0x2B: str = "+"; break;
                case 0x2C: str = ","; break;
                case 0x2D: str = "-"; break;
                case 0x2E: str = "."; break;
                case 0x2F: str = "/"; break;
                case 0x30: str = "0"; break;
                case 0x31: str = "1"; break;
                case 0x32: str = "2"; break;
                case 0x33: str = "3"; break;
                case 0x34: str = "4"; break;
                case 0x35: str = "5"; break;
                case 0x36: str = "6"; break;
                case 0x37: str = "7"; break;
                case 0x38: str = "8"; break;
                case 0x39: str = "9"; break;
                case 0x3A: str = ":"; break;
                case 0x3B: str = ";"; break;
                case 0x3C: str = "<"; break;
                case 0x3D: str = "="; break;
                case 0x3E: str = ">"; break;
                case 0x3F: str = "?"; break;
                case 0x40: str = "@"; break;
                case 0x41: str = "A"; break;
                case 0x42: str = "B"; break;
                case 0x43: str = "C"; break;
                case 0x44: str = "D"; break;
                case 0x45: str = "E"; break;
                case 0x46: str = "F"; break;
                case 0x47: str = "G"; break;
                case 0x48: str = "H"; break;
                case 0x49: str = "I"; break;
                case 0x4A: str = "J"; break;
                case 0x4B: str = "K"; break;
                case 0x4C: str = "L"; break;
                case 0x4D: str = "M"; break;
                case 0x4E: str = "N"; break;
                case 0x4F: str = "O"; break;
                case 0x50: str = "P"; break;
                case 0x51: str = "Q"; break;
                case 0x52: str = "R"; break;
                case 0x53: str = "S"; break;
                case 0x54: str = "T"; break;
                case 0x55: str = "U"; break;
                case 0x56: str = "V"; break;
                case 0x57: str = "W"; break;
                case 0x58: str = "X"; break;
                case 0x59: str = "Y"; break;
                case 0x5A: str = "Z"; break;
                case 0x5B: str = "["; break;
                case 0x5C: str = "\\"; break;
                case 0x5D: str = "]"; break;
                case 0x5E: str = "^"; break;
                case 0x5F: str = "_"; break;
                case 0x60: str = "`"; break;
                case 0x61: str = "a"; break;
                case 0x62: str = "b"; break;
                case 0x63: str = "c"; break;
                case 0x64: str = "d"; break;
                case 0x65: str = "e"; break;
                case 0x66: str = "f"; break;
                case 0x67: str = "g"; break;
                case 0x68: str = "h"; break;
                case 0x69: str = "i"; break;
                case 0x6A: str = "j"; break;
                case 0x6B: str = "k"; break;
                case 0x6C: str = "l"; break;
                case 0x6D: str = "m"; break;
                case 0x6E: str = "n"; break;
                case 0x6F: str = "o"; break;
                case 0x70: str = "p"; break;
                case 0x71: str = "q"; break;
                case 0x72: str = "r"; break;
                case 0x73: str = "s"; break;
                case 0x74: str = "t"; break;
                case 0x75: str = "u"; break;
                case 0x76: str = "v"; break;
                case 0x77: str = "w"; break;
                case 0x78: str = "x"; break;
                case 0x79: str = "y"; break;
                case 0x7A: str = "z"; break;
                case 0x7B: str = "{"; break;
                case 0x7C: str = "|"; break;
                case 0x7D: str = "}"; break;
                case 0x7E: str = "~"; break;
                case 0x7F: str = "DEL"; break;
                case 0x80: str = "Ђ"; break;
                case 0x81: str = "Ѓ"; break;
                case 0x82: str = "‚"; break;
                case 0x83: str = "ѓ"; break;
                case 0x84: str = "„"; break;
                case 0x85: str = "…"; break;
                case 0x86: str = "†"; break;
                case 0x87: str = "‡"; break;
                case 0x88: str = "€"; break;
                case 0x89: str = "‰"; break;
                case 0x8A: str = "Љ"; break;
                case 0x8B: str = "‹"; break;
                case 0x8C: str = "Њ"; break;
                case 0x8D: str = "Ќ"; break;
                case 0x8E: str = "Ћ"; break;
                case 0x8F: str = "Џ"; break;
                case 0x90: str = "ђ"; break;
                case 0x91: str = "‘"; break;
                case 0x92: str = "’"; break;
                case 0x93: str = "“"; break;
                case 0x94: str = "”"; break;
                case 0x95: str = "•"; break;
                case 0x96: str = "–"; break;
                case 0x97: str = "—"; break;
                case 0x98: str = "�"; break;
                case 0x99: str = "™"; break;
                case 0x9A: str = "љ"; break;
                case 0x9B: str = "›"; break;
                case 0x9C: str = "њ"; break;
                case 0x9D: str = "ќ"; break;
                case 0x9E: str = "ћ"; break;
                case 0x9F: str = "џ"; break;
                case 0xA0: str = "SPC"; break;
                case 0xA1: str = "Ў"; break;
                case 0xA2: str = "ў"; break;
                case 0xA3: str = "Ј"; break;
                case 0xA4: str = "¤"; break;
                case 0xA5: str = "Ґ"; break;
                case 0xA6: str = "¦"; break;
                case 0xA7: str = "§"; break;
                case 0xA8: str = "Ё"; break;
                case 0xA9: str = "©"; break;
                case 0xAA: str = "Є"; break;
                case 0xAB: str = "«"; break;
                case 0xAC: str = "¬"; break;
                case 0xAD: str = "®"; break;
                case 0xAE: str = "®"; break;
                case 0xAF: str = "Ї"; break;
                case 0xB0: str = "°"; break;
                case 0xB1: str = "±"; break;
                case 0xB2: str = "І"; break;
                case 0xB3: str = "і"; break;
                case 0xB4: str = "ґ"; break;
                case 0xB5: str = "μ"; break;
                case 0xB6: str = "¶"; break;
                case 0xB7: str = "·"; break;
                case 0xB8: str = "ё"; break;
                case 0xB9: str = "№"; break;
                case 0xBA: str = "є"; break;
                case 0xBB: str = "»"; break;
                case 0xBC: str = "ј"; break;
                case 0xBD: str = "Ѕ"; break;
                case 0xBE: str = "ѕ"; break;
                case 0xBF: str = "ї"; break;
                case 0xC0: str = "А"; break;
                case 0xC1: str = "Б"; break;
                case 0xC2: str = "В"; break;
                case 0xC3: str = "Г"; break;
                case 0xC4: str = "Д"; break;
                case 0xC5: str = "Е"; break;
                case 0xC6: str = "Ж"; break;
                case 0xC7: str = "З"; break;
                case 0xC8: str = "И"; break;
                case 0xC9: str = "Й"; break;
                case 0xCA: str = "К"; break;
                case 0xCB: str = "Л"; break;
                case 0xCC: str = "М"; break;
                case 0xCD: str = "Н"; break;
                case 0xCE: str = "О"; break;
                case 0xCF: str = "П"; break;
                case 0xD0: str = "Р"; break;
                case 0xD1: str = "С"; break;
                case 0xD2: str = "Т"; break;
                case 0xD3: str = "У"; break;
                case 0xD4: str = "Ф"; break;
                case 0xD5: str = "Х"; break;
                case 0xD6: str = "Ц"; break;
                case 0xD7: str = "Ч"; break;
                case 0xD8: str = "Ш"; break;
                case 0xD9: str = "Щ"; break;
                case 0xDA: str = "Ъ"; break;
                case 0xDB: str = "Ы"; break;
                case 0xDC: str = "Ь"; break;
                case 0xDD: str = "Э"; break;
                case 0xDE: str = "Ю"; break;
                case 0xDF: str = "Я"; break;
                case 0xE0: str = "а"; break;
                case 0xE1: str = "б"; break;
                case 0xE2: str = "в"; break;
                case 0xE3: str = "г"; break;
                case 0xE4: str = "д"; break;
                case 0xE5: str = "е"; break;
                case 0xE6: str = "ж"; break;
                case 0xE7: str = "з"; break;
                case 0xE8: str = "и"; break;
                case 0xE9: str = "й"; break;
                case 0xEA: str = "к"; break;
                case 0xEB: str = "л"; break;
                case 0xEC: str = "м"; break;
                case 0xED: str = "н"; break;
                case 0xEE: str = "о"; break;
                case 0xEF: str = "п"; break;
                case 0xF0: str = "р"; break;
                case 0xF1: str = "с"; break;
                case 0xF2: str = "т"; break;
                case 0xF3: str = "у"; break;
                case 0xF4: str = "ф"; break;
                case 0xF5: str = "х"; break;
                case 0xF6: str = "ц"; break;
                case 0xF7: str = "ч"; break;
                case 0xF8: str = "ш"; break;
                case 0xF9: str = "щ"; break;
                case 0xFA: str = "ъ"; break;
                case 0xFB: str = "ы"; break;
                case 0xFC: str = "ь"; break;
                case 0xFD: str = "э"; break;
                case 0xFE: str = "ю"; break;
                case 0xFF: str = "я"; break;
            }

            return str;
        }
    }
}
