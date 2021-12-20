using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GarriBoard
{
    public partial class CommandForm : Form
    {
        MainForm mf = new MainForm();
        //DataProc pdata;

        public CommandForm(MainForm parent)
        {
            InitializeComponent();
            //pdata = new DataProc(mf.serialPort);
            this.mf = parent;
        }
        private void CommandForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //mf.Dispose();
        }

        private void buttonResetMCU_Click(object sender, EventArgs e)
        {
            if (mf.pdata.ExecuteF1() == true)
            {
                buttonResetMCU.BackColor = Color.SkyBlue;
                buttonResetMCU.Enabled = false;
                timerBtnF1.Enabled = true;
            }
        }

        private void buttonSettingClear_Click(object sender, EventArgs e)
        {
            if (mf.pdata.ExecuteF2() == true)
            {
                buttonSettingClear.BackColor = Color.SkyBlue;
                buttonSettingClear.Enabled = false;
                timerBtnF2.Enabled = true;
            }
        }

        private void buttonRC_Click(object sender, EventArgs e)
        {
            if (mf.pdata.ExecuteF3() == true)
            {
                buttonRC.BackColor = Color.SkyBlue;
                buttonRC.Enabled = false;
                timerBtnF3.Enabled = true;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int n1, n2, n3, n4, kv, y1, y2, y3, y4;

            n1 = Int32.Parse(textBoxN1.Text);
            n2 = Int32.Parse(textBoxN2.Text);
            n3 = Int32.Parse(textBoxN3.Text);
            n4 = Int32.Parse(textBoxN4.Text);
            kv = Int32.Parse(textBoxKv.Text);
            y1 = Int32.Parse(textBoxY1.Text);
            y2 = Int32.Parse(textBoxY2.Text);
            y3 = Int32.Parse(textBoxY3.Text);
            y4 = Int32.Parse(textBoxY4.Text);

            if (mf.pdata.ExecuteSerialNumer(n1,n2,n3,n4,kv,y1,y2,y3,y4) == true)
            {
                buttonOk.BackColor = Color.SkyBlue;
                buttonOk.Enabled = false;
                timerBtnSerNum.Enabled = true;
            }
        }

        private void timerBtnF1_Tick(object sender, EventArgs e)
        {
            timerBtnF1.Enabled = false;
            buttonResetMCU.BackColor = Color.Honeydew;
            buttonResetMCU.Enabled = true;
        }

        private void timerBtnF2_Tick(object sender, EventArgs e)
        {
            timerBtnF2.Enabled = false;
            buttonSettingClear.BackColor = Color.Honeydew;
            buttonSettingClear.Enabled = true;
        }

        private void timerBtnF3_Tick(object sender, EventArgs e)
        {
            timerBtnF3.Enabled = false;
            buttonRC.BackColor = Color.Honeydew;
            buttonRC.Enabled = true;
        }

        private void timerBtnSerNum_Tick(object sender, EventArgs e)
        {
            timerBtnSerNum.Enabled = false;
            buttonOk.BackColor = Color.Honeydew;
            buttonOk.Enabled = true;
        }
    }
}
