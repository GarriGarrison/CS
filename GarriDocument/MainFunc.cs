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
    partial class MainForm
    {
        const int DOCUM_LETTETS = 1;
        const int DOCUM_DOCUMS  = 2;
        const int DOCUM_TTZ = 3;

        private void LoadDocums(int change_type)
        {
            listBoxRar.Items.Clear();
            listBoxLetters.Items.Clear();
            

            switch (change_type)
            {
                case DOCUM_LETTETS:
                    DeviceUses.way_type_docum = "\\_letters";
                    break;
                case DOCUM_DOCUMS:
                    DeviceUses.way_type_docum = "\\_docums";
                    break;
                case DOCUM_TTZ:
                    DeviceUses.way_type_docum = "\\_ttz";
                    break;
            }

            if (DeviceUses.fSystem)
            {
                DeviceUses.way_type_device = DeviceUses.system_name;
            }
            else
            {
                DeviceUses.way_type_device = DeviceUses.device_name;
            }


            string[] readText = File.ReadAllLines(@"Devices\\" + DeviceUses.way_type_device + DeviceUses.way_type_docum + "\\_list.dat");
            foreach (string s in readText)
            {
                listBoxLetters.Items.Add(s);
            }
        }



        const int BTN_KD   = 1;
        const int BTN_TD   = 2;
        const int BTN_HEX  = 3;
        const int BTN_KTA  = 4;
        const int BTN_DD   = 5;
        const int BTN_IZV  = 6;
        const int BTN_TF   = 7;
        const int BTN_JUNK = 8;
        const int BTN_SP   = 9;
        const int BTN_DI   = 10;

        private void ViewBtnLed(int btn_led)
        {
            bool btnKD   = false;
            bool btnTD   = false;
            bool btnHEX  = false;
            bool btnKTA  = false;
            bool btnDD   = false;
            bool btnIZV  = false;
            bool btnTF   = false;
            bool btnJUNK = false;
            bool btnSP   = false;
            bool btnDI   = false;


            switch (btn_led)
            {
                case BTN_KD:
                    btnKD = true;
                    break;
                case BTN_TD:
                    btnTD = true;
                    break;
                case BTN_HEX:
                    btnHEX = true;
                    break;
                case BTN_KTA:
                    btnKTA = true;
                    break;
                case BTN_DD:
                    btnDD = true;
                    break;
                case BTN_IZV:
                    btnIZV = true;
                    break;
                case BTN_TF:
                    btnTF = true;
                    break;
                case BTN_JUNK:
                    btnJUNK = true;
                    break;
                case BTN_SP:
                    btnSP = true;
                    break;
                case BTN_DI:
                    btnDI = true;
                    break;
            }


            if (btnKD)   { indKD.BackColor = Color.SkyBlue;   } else { indKD.BackColor = Color.WhiteSmoke;   }
            if (btnTD)   { indTD.BackColor = Color.SkyBlue;   } else { indTD.BackColor = Color.WhiteSmoke;   }
            if (btnHEX)  { indHEX.BackColor = Color.SkyBlue;  } else { indHEX.BackColor = Color.WhiteSmoke;  }
            if (btnKTA)  { indKTA.BackColor = Color.SkyBlue;  } else { indKTA.BackColor = Color.WhiteSmoke;  }
            if (btnDD)   { indDD.BackColor = Color.SkyBlue;   } else { indDD.BackColor = Color.WhiteSmoke;   }
            if (btnIZV)  { indIZV.BackColor = Color.SkyBlue;  } else { indIZV.BackColor = Color.WhiteSmoke;  }
            if (btnTF)   { indTF.BackColor = Color.SkyBlue;   } else { indTF.BackColor = Color.WhiteSmoke;   }
            if (btnJUNK) { indJunk.BackColor = Color.SkyBlue; } else { indJunk.BackColor = Color.WhiteSmoke; }
            if (btnSP)   { indSP.BackColor = Color.SkyBlue;   } else { indSP.BackColor = Color.WhiteSmoke;   }
            if (btnDI)   { indDI.BackColor = Color.SkyBlue;   } else { indDI.BackColor = Color.WhiteSmoke;   }

        }

        private void LoadContent(string way_type_docum)
        {
            string way_type_device;
            listBoxContent.Items.Clear();
            listBoxRar.Items.Clear();


            if (DeviceUses.fSystem)
            {
                    way_type_device = DeviceUses.system_name + "\\" + DeviceUses.device_name;
            }
            else
            {
                    way_type_device = DeviceUses.device_name;
            }


            string[] readText = File.ReadAllLines(@"Devices\\" + way_type_device + "\\Datasheets\\" + way_type_docum + "\\_list.dat");
            foreach (string s in readText)
            {
                listBoxContent.Items.Add(s);
            }
        }


        private void ViewButtonEnabled()
        {
            buttonContent.Enabled = true;
            buttonLetters.Enabled = true;
            buttonDocums.Enabled = true;
            buttonTTZ.Enabled = true;
            buttonStructure.Enabled = true;
            buttonDefect.Enabled = true;
            buttonNote.Enabled = true;

            btnKD.Enabled = true;
            btnTD.Enabled = true;
            btnHEX.Enabled = true;
            btnKTA.Enabled = true;
            btnDD.Enabled = true;
            btnIZV.Enabled = true;
            btnTF.Enabled = true;
            btnJunk.Enabled = true;
            btnSP.Enabled = true;
            btnDI.Enabled = true;
            buttonRar.Enabled = true;

            listBoxLetters.Enabled = true;
            listBoxContent.Enabled = true;
        }
    }
}
