using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Documents
{
    public partial class DeviceForm : Form
    { 
        private MainForm mform_referense;
        public DeviceForm(MainForm mf)
        {
            mform_referense = mf;
            InitializeComponent();
        }

        //Загрузка формы
        private void DeviceForm_Load(object sender, EventArgs e)
        {
            string[] readText = File.ReadAllLines(@"Setup\\devices.dat");
            foreach (string s in readText)
            {
                listBoxDevice.Items.Add(s);
            }
        }

        private void listBoxDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAccept.Enabled = true;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            string str_now = listBoxDevice.SelectedItem.ToString();
            if (str_now.IndexOf("#") > 0)
            {
                DeviceUses.system_name = str_now.Substring(str_now.IndexOf("@") + 1, str_now.IndexOf("#") - str_now.IndexOf("@") - 1);
                DeviceUses.device_name = str_now.Substring(str_now.IndexOf(">>") + 2);
                DeviceUses.view_system_name = str_now.Substring(str_now.IndexOf("#") + 1, str_now.IndexOf(">>") - str_now.IndexOf("#") - 1);
                DeviceUses.view_device_name = str_now.Substring(0, str_now.IndexOf(" "));
                DeviceUses.way_open = DeviceUses.system_name;
                DeviceUses.way_datasheet = DeviceUses.system_name + "\\" + DeviceUses.device_name;
                DeviceUses.fSystem = true;
            }
            else
            {
                DeviceUses.system_name = "";
                DeviceUses.device_name = str_now.Substring(str_now.IndexOf(">>") + 2);
                DeviceUses.view_system_name = "";
                DeviceUses.view_device_name = str_now.Substring(0, str_now.IndexOf(" "));
                DeviceUses.way_open = DeviceUses.device_name;
                DeviceUses.way_datasheet = DeviceUses.device_name;
                DeviceUses.fSystem = false;
            }
            
            DeviceUses.fWay = true;
        }
    }
}
