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
    public partial class ParamsForm : Form
    {
        MainForm mf = new MainForm();
        //DataProc pdata;

        int num_param = 0;

        public ParamsForm(MainForm parent)
        {
            InitializeComponent();
            //pdata = new DataProc(mf.serialPort);
            this.mf = parent;
        }

        private void ParamsForm_Load(object sender, EventArgs e)
        {
            listBoxChange.Items.Add("Средство");

            for (int i = 0; i < DeviceUses.qntSector; i++)
                listBoxChange.Items.Add("   Cектор " + (i+1).ToString());
        }

        private void ParamsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //mf.Dispose();
        }

        private void listBoxChange_Click(object sender, EventArgs e)
        {
            labelItemNow.Text = listBoxChange.SelectedIndex.ToString();
            num_param = listBoxChange.SelectedIndex;

            string[] row = { "name", "type", "access", "min", "default", "max", "value" };
            int num_row = 0;

            //Очистка таблицы
            tableParams.Rows.Clear();
            //for (int i = 0; i < tableParams.Rows.Count; i++)
            //{
            //    tableParams.Rows.RemoveAt(i);
            //}


            if (listBoxChange.SelectedIndex == 0)   //Средство
            {
                for (int i = 0; i < DeviceUses.qntDeviceParams; i++)
                {
                    row[0] = DeviceUses.handlDevice[i].hName;
                    row[1] = DeviceUses.handlDevice[i].hType.ToString();
                    row[2] = DeviceUses.handlDevice[i].hAccess.ToString();
                    row[3] = DeviceUses.handlDevice[i].minValue.ToString();
                    row[4] = DeviceUses.handlDevice[i].defaultValue.ToString();
                    row[5] = DeviceUses.handlDevice[i].maxValue.ToString();
                    row[6] = DeviceUses.handlDevice[i].value.ToString();

                    tableParams.Rows.Add(row);
                }
            }
            else if (listBoxChange.SelectedIndex > 0)  //Сектора
            {
                num_row = listBoxChange.SelectedIndex;
                for (int i = 0; i < DeviceUses.qntSectorParms; i++)
                {
                    row[0] = DeviceUses.handlSector[i, num_row - 1].hName;
                    row[1] = DeviceUses.handlSector[i, num_row - 1].hType.ToString();
                    row[2] = DeviceUses.handlSector[i, num_row - 1].hAccess.ToString();
                    row[3] = DeviceUses.handlSector[i, num_row - 1].minValue.ToString();
                    row[4] = DeviceUses.handlSector[i, num_row - 1].defaultValue.ToString();
                    row[5] = DeviceUses.handlSector[i, num_row - 1].maxValue.ToString();
                    row[6] = DeviceUses.handlSector[i, num_row - 1].value.ToString();

                    tableParams.Rows.Add(row);
                }
            }
        }

        private void tableParams_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                //MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                if (num_param == 0)
                {
                    if (mf.pdata.SetDeviceParams(e.RowIndex + 1, Convert.ToUInt16(tableParams[6, e.RowIndex + 1].Value)) == true)
                    {
                        MessageBox.Show("Парамет успешно установлен");
                    }
                }
                else
                {
                    if (mf.pdata.SetSectorParams(num_param, 1, e.RowIndex + 1, Convert.ToUInt16(tableParams[6, e.RowIndex + 1].Value)) == true)
                    {
                        MessageBox.Show("Парамет успешно установлен");
                    }
                }
            }
        }

        private void buttonValueAll_Click(object sender, EventArgs e)
        {
            int sector_start = Convert.ToInt32(textBoxSectorStart.Text);
            int sector_count = Convert.ToInt32(textBoxSectorCount.Text);


            if (num_param == 0)
            {
                if (mf.pdata.SetDeviceParams(0, Convert.ToUInt16(textBoxValueAll.Text)) == true)
                {
                    MessageBox.Show("Новые значения успечно установлены");
                    mf.pdata.GetDeviceParams();
                    // listBoxChange_Click();
                }
            }
            else
            {
                if (mf.pdata.SetSectorParams(sector_start, sector_count, 0, Convert.ToUInt16(textBoxValueAll.Text)) == true)
                {
                    MessageBox.Show("Новые значения успечно установлены");
                    mf.pdata.GetSectorParams();
                }
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            int sector_start = Convert.ToInt32(textBoxSectorStart.Text);
            int sector_count = Convert.ToInt32(textBoxSectorCount.Text);


            if (num_param == 0)
            {
                for (int i = 0; i < DeviceUses.qntDeviceParams; i++)
                {
                    DeviceUses.handlDevice[i].newValue = Convert.ToUInt16(tableParams[6, i + 1].Value);
                }

                if (mf.pdata.SetDeviceParamsMatrix(DeviceUses.qntDeviceParams) == true)
                {
                    MessageBox.Show("Новые значения успечно установлены");
                    mf.pdata.GetDeviceParams();
                }
            }
            else
            {
                for (int i = 0; i < DeviceUses.qntSectorParms; i++)
                {
                    DeviceUses.handlSector[i, num_param].newValue = Convert.ToUInt16(tableParams[6, i + 1].Value);
                }

                if (mf.pdata.SetSectorParamsMatrix(sector_start, sector_count, DeviceUses.qntSectorParms) == true)
                {
                    MessageBox.Show("Новые значения успечно установлены");
                    mf.pdata.GetSectorParams();
                }
            }
        }
    }
}
