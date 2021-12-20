using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;


namespace Documents
{
    public partial class MainForm : Form
    {
        //private const string way_letters = @"Devices\\letters\\_list.dat";

        public MainForm()
        {
            InitializeComponent();
            listBoxContent.Width = 959;
        }
        

        public string System_name { get => System_name; set => System_name = value; }

        //Загрузка приложения
        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] readUpdate = File.ReadAllLines(@"Setup\\update.dat");
            foreach (string s in readUpdate)
            {
                listBoxUpdate.Items.Add(s);
            }
            string str = listBoxUpdate.Items[0].ToString();
            string str_year = str.Substring(6, 4);
            string str_moutch = str.Substring(3, 2);
            string str_day = str.Substring(0, 2);
            Text = "Цирера_v5.0  [База данных -" + str_year + str_moutch + str_day + "]";
        }

        //Выбор устройства и загрузка данных
        private void buttonDevice_Click(object sender, EventArgs e)
        {
            listBoxContent.Items.Clear();
            listBoxRar.Items.Clear();
            listBoxLetters.Items.Clear();

            DeviceForm deviceForm = new DeviceForm(this);
            DialogResult resultDevice = deviceForm.ShowDialog();

            buttonDevice.Text = DeviceUses.view_device_name;
            if (DeviceUses.fSystem)
            {
                labelSystem.Text = DeviceUses.view_system_name;
                labelSystem.Visible = true;
            }
            else
            {
                labelSystem.Visible = false;
                labelSystem.Text = "";
            }

            if (DeviceUses.fWay) ViewButtonEnabled();
        }

        private void buttonContent_Click(object sender, EventArgs e)
        {
            listBoxLetters.Visible = false;
        }

        private void buttonLetters_Click(object sender, EventArgs e)
        {
            if (DeviceUses.fWay) { LoadDocums(DOCUM_LETTETS); }
            listBoxLetters.Visible = true;
        }

        private void buttonDocums_Click(object sender, EventArgs e)
        {
            if (DeviceUses.fWay) { LoadDocums(DOCUM_DOCUMS); }
            listBoxLetters.Visible = true;
        }

        private void buttonTTZ_Click(object sender, EventArgs e)
        {
            if (DeviceUses.fWay) { LoadDocums(DOCUM_TTZ); }
            listBoxLetters.Visible = true;
        }

        private void buttonStructure_Click(object sender, EventArgs e)
        {
            if (DeviceUses.fWay)
            {
                Process.Start(@"Devices\\" + DeviceUses.way_open + "\\Structure.vsd");
            }
        }

        private void buttonDefect_Click(object sender, EventArgs e)
        {
            if (DeviceUses.fWay)
            {
                Process.Start(@"Devices\\" + DeviceUses.way_open + "\\JornalDefect.docx");
            }
        }

        private void buttonNote_Click(object sender, EventArgs e)
        {
            if (DeviceUses.fWay)
            {
                Process.Start(@"Devices\\" + DeviceUses.way_open + "\\NOte.docx");
            }
        }
        private void btnKD_Click(object sender, EventArgs e)
        {
            LoadContent("KD");
            ViewBtnLed(BTN_KD);
            DeviceUses.docum_type = "KD";
        }

        private void btnTD_Click(object sender, EventArgs e)
        {
            LoadContent("TD");
            ViewBtnLed(BTN_TD);
            DeviceUses.docum_type = "TD";
        }

        private void btnHEX_Click(object sender, EventArgs e)
        {
            LoadContent("HEX");
            ViewBtnLed(BTN_HEX);
            DeviceUses.docum_type = "HEX";
        }

        private void btnKTA_Click(object sender, EventArgs e)
        {
            LoadContent("KTA");
            ViewBtnLed(BTN_KTA);
            DeviceUses.docum_type = "KTA";
        }

        private void btnDD_Click(object sender, EventArgs e)
        {
            LoadContent("DD");
            ViewBtnLed(BTN_DD);
            DeviceUses.docum_type = "DD";
        }

        private void btnIZV_Click(object sender, EventArgs e)
        {
            LoadContent("IZV");
            ViewBtnLed(BTN_IZV);
            DeviceUses.docum_type = "IZV";
        }

        private void btnTF_Click(object sender, EventArgs e)
        {
            LoadContent("TF");
            DeviceUses.docum_type = "TF";
            ViewBtnLed(BTN_TF);
        }

        private void btnJunk_Click(object sender, EventArgs e)
        {
            LoadContent("JUNK");
            ViewBtnLed(BTN_JUNK);
            DeviceUses.docum_type = "JUNK";
        }

        private void buttonSP_Click(object sender, EventArgs e)
        {
            LoadContent("SP");
            ViewBtnLed(BTN_SP);
            DeviceUses.docum_type = "SP";
        }

        private void buttonDI_Click(object sender, EventArgs e)
        {
            LoadContent("DI");
            ViewBtnLed(BTN_DI);
            DeviceUses.docum_type = "DI";
        }

        private void buttonRar_Click(object sender, EventArgs e)
        {
            if (!DeviceUses.fRar)
            {
                listBoxRar.Visible = true;
                listBoxContent.Width = 645;
                indRar.BackColor = Color.SkyBlue;
                DeviceUses.fRar = true;
                buttonLetters.Enabled = false;
                buttonDocums.Enabled = false;
                buttonTTZ.Enabled = false;
            }
            else
            {
                listBoxContent.Width = 959;
                listBoxRar.Visible = false;
                indRar.BackColor = Color.WhiteSmoke;
                DeviceUses.fRar = false;
                buttonLetters.Enabled = true;
                buttonDocums.Enabled = true;
                buttonTTZ.Enabled = true;
            }

        }
        private void listBoxContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str_now;
            int i = 0;

            str_now = listBoxContent.SelectedItem.ToString();

            if (str_now.IndexOf("#") > 0)
            {
                DeviceUses.file_name = str_now.Substring(str_now.IndexOf("#") + 1);

                listBoxRar.Items.Clear();
                string[] readRar = File.ReadAllLines(@"Devices\\" + DeviceUses.way_datasheet + "\\Datasheets\\" + DeviceUses.docum_type + "\\" + DeviceUses.file_name + ".dat");
                foreach (string s in readRar)
                {
                    if (i > 6) listBoxRar.Items.Add(s);
                    i++;
                }

                DeviceUses.AtributeFile.autor = readRar[0].ToString();
                DeviceUses.AtributeFile.control = readRar[1].ToString();
                DeviceUses.AtributeFile.version = readRar[2].ToString();
                DeviceUses.AtributeFile.date = readRar[3].ToString();
                DeviceUses.AtributeFile.program = readRar[4].ToString();
            }

            if (str_now.IndexOf("&") > 0)
            {
                DeviceUses.file_name = str_now.Substring(str_now.IndexOf("&") + 1);
            }
        }

        private void listBoxContent_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(@"Devices\\" + DeviceUses.way_datasheet + "\\Datasheets\\" + DeviceUses.docum_type + "\\" + DeviceUses.file_name + ".pdf");
        }

        private void listBoxRar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str_now;

            str_now = listBoxRar.SelectedItem.ToString();
            DeviceUses.file_rev = "_" + str_now.Substring(0, str_now.IndexOf(" "));
        }

        private void listBoxRar_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(@"Devices\\" + DeviceUses.way_datasheet + "\\Rar\\" + DeviceUses.docum_type + "\\" + DeviceUses.file_name + DeviceUses.file_rev + ".pdf");
        }

        private void listBoxLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str_now;

            str_now = listBoxLetters.SelectedItem.ToString();
            if (str_now.IndexOf("#") > 0)
            {
                DeviceUses.file_name = str_now.Substring(str_now.IndexOf("#") + 1);
            }
        }

        private void listBoxLetters_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(@"Devices\\" + DeviceUses.way_type_device + "\\" + DeviceUses.way_type_docum + "\\" + DeviceUses.file_name + ".pdf");
        }

        private void contextMenuContent_Opening(object sender, CancelEventArgs e)
        {
            //AtributeForm atributeForm = new AtributeForm();
            //atributeForm.ShowDialog();
        }

        private void toolMenuContentAtribute_Click(object sender, EventArgs e)
        {
            AtributeForm atributeForm = new AtributeForm();
            atributeForm.ShowDialog();
        }

        private void contextMenuRar_Opening(object sender, CancelEventArgs e)
        {
            CommentForm commentForm = new CommentForm();
            commentForm.ShowDialog();
        }
    }
}
