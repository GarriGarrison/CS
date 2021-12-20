using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents
{
    public static class DeviceUses
    {
        public static string system_name;       //имя системы для указания пути к файлам
        public static string device_name;       //имя устройства для указания пути к файлам
        public static string way_open;          //путь к содержанию устройства
        public static string way_datasheet;     //путь к техническим документам устройства
        public static string way_type_device;   //путь, тип устройства
        public static string way_type_docum;    //путь к открытию типа документов (письма, документы, ттз)
        public static string docum_type;        //тип открываемых документов (КД, ТД, ПО...)
        public static string view_device_name;  //отображаемое имя устройства или системы в названии вызываемой кнопки
        public static string view_system_name;  //отобраемое имя системы, куда входит выбраное устройство
        public static string file_name;         //имя файла в выбранного документа для открытия pdf и загрузки атрибутов
        public static string file_rev;          //ревизия файла выбранного документа для открытия архивной pdf
        public static bool fSystem;             //флаг наличия системы у устройства
        public static bool fRar;                //флаг отображения архивных копий
        public static bool fWay;                //флаг выбора устройства (и появления пути к документам)


        public struct AtributeFile
        {
            public static string autor;
            public static string control;
            public static string version;
            public static string date;
            public static string program;
        }
    }
}
