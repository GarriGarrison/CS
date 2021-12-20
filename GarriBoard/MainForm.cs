using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;



namespace GarriBoard
{
    public partial class MainForm : Form
    {
        public DataProc pdata;
        public MainForm()
        {
            InitializeComponent();
            viewInit();
            pdata = new DataProc(serialPort);
            DeviceUses.qntSector = 1;
        }

        ~MainForm()
        {
            serialPort.Close();
        }



        private void timerDataTime_Tick(object sender, EventArgs e)
        {
            panelDate.Text = System.DateTime.Now.ToLongDateString();
            panelTime.Text = System.DateTime.Now.ToLongTimeString();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = this.CreateGraphics();
            //g.DrawLine(new Pen(Color.Red), 10, 10, 100, 100);

            Pen pen = new Pen(Color.White);

            /* Контур платы */
            pen.Width = 5;
            //e.Graphics.DrawEllipse(pen, 10, 10, 100, 100);
            e.Graphics.DrawLine(pen, 15, 110, 15, 470);
            e.Graphics.DrawLine(pen, 15, 470, 700, 470);
            e.Graphics.DrawLine(pen, 700, 470, 700, 50);
            e.Graphics.DrawLine(pen, 700, 50, 90, 50);
            e.Graphics.DrawLine(pen, 90, 50, 15, 110);

            /* Контур разъёмов */
            pen.Width = 3;
            //e.Graphics.DrawLine(pen, 50, 105, 50, 450);
        }



        private void comboBoxPort_DropDown(object sender, EventArgs e)
        {
            comboBoxPort.Items.Clear();
            
            string[] enableComPorts = SerialPort.GetPortNames();

            foreach (string port in enableComPorts)
            {
                comboBoxPort.Items.Add(port);
            }
        }

        private void buttonPort_Click(object sender, EventArgs e)
        {
           if (serialPort.IsOpen)  //(pdata.isOpen)
            {
                timerMarker.Enabled = false;
                serialPort.Close();
                viewPortDisconnect();
                ListBoxLog.Items.Insert(0, "Сом-порт закрыт");
                //pdata.isOpen = false;
           }
           else
           {
                serialPort.PortName = comboBoxPort.Text;
                try
                {
                    serialPort.Open();
                    buttonPort.Text = "ОТКЛ";
                    buttonPort.BackColor = Color.LawnGreen;
                    ListBoxLog.Items.Add("Сом-порт успешно открыт");

                    if (pdata.isConnect() == true)
                    {
                        viewPortConnect();
                    }
                }
                catch (Exception ex)
                {
                    viewPortDisconnect();
                    MessageBox.Show("Ошибка открытия ComPort!!!  "
                      + ex.Message, "Error!");
                }

                //buttonPort.Text = "ОТКЛ";
                //buttonPort.BackColor = Color.LawnGreen;
                //ListBoxLog.Items.Insert(0, "Сом-порт успешно открыт");
                //viewPortConnect();
                //pdata.isOpen = true;
            }
        }

        private void ListBoxLog_DoubleClick(object sender, EventArgs e)
        {
            ListBoxLog.Items.Clear();
        }

        private void butStateBit0_Click(object sender, EventArgs e)
        {
            if (flag.isState0)
            {
                pdata.ClrState(0);
                butStateBit0.BackColor = Color.DarkGreen;
                flag.isState0 = false;
            }
            else
            {
                pdata.SetState(0);
                butStateBit0.BackColor = Color.LimeGreen;
                flag.isState0 = true;
            }
        }

        private void butStateBit1_Click(object sender, EventArgs e)
        {
            if (flag.isState1)
            {
                pdata.ClrState(1);
                butStateBit0.BackColor = Color.DarkGreen;
                flag.isState1 = false;
            }
            else
            {
                pdata.SetState(1);
                butStateBit0.BackColor = Color.LimeGreen;
                flag.isState1 = true;
            }
        }

        private void butStateBit2_Click(object sender, EventArgs e)
        {
           if (flag.isState2)
            {
                pdata.ClrState(2);
                butStateBit0.BackColor = Color.DarkGreen;
                flag.isState2 = false;
            }
            else
            {
                pdata.SetState(2);
                butStateBit0.BackColor = Color.LimeGreen;
                flag.isState2 = true;
            }
        }

        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            btnGetInfo.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.GetInfo() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x01  Get Info -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Описание");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x01 Get Info -> Ошибка!!!");
        }

        private void btnGetConfig_Click(object sender, EventArgs e)
        {
            btnGetConfig.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;
            
            if (pdata.GetConfig() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x02  Get Config -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Описание");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x02 Get Config -> Ошибка!!!");
        }

        private void btnGetDeviceParamsHandle_Click(object sender, EventArgs e)
        {
            btnGetDeviceParamsHandle.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.GetDeviceParamsHandle() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x03  Get Device Params Handle -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Параметры");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x03 Get Device Params Handle -> Ошибка!!!");
        }

        private void btnGetSectorParamsHandle_Click(object sender, EventArgs e)
        {
            btnGetSectorParamsHandle.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.GetSectorParamsHandle() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x04  Get Sector Params Handle -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Параметры");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x04 Get Sector Params Handle -> Ошибка!!!");
        }

        private void btnGetDeviceParams_Click(object sender, EventArgs e)
        {
            btnGetDeviceParams.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.GetDeviceParams() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x05  Get Device Params -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Параметры");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x05 Get Device Params -> Ошибка!!!");
        }

        private void btnGetSectorParams_Click(object sender, EventArgs e)
        {
            btnGetSectorParams.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.GetSectorParams() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x06  Get Sector Params -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Параметры");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x06 Get Sector Params -> Ошибка!!!");
        }

        private void btnGetAddSensHandle_Click(object sender, EventArgs e)
        {
            btnGetAddSensHandle.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.GetAddSensHandle() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x0B  Get Add Sens Description -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Коды и доп. датчики");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x0B Get Add Sens Description -> Ошибка!!!");
        }

        private void btnGetAddress_Click(object sender, EventArgs e)
        {
            btnGetAddress.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.GetAddress() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x10  Get Address -> Успешно");
                panelAddress.Text = DeviceUses.address.ToString();
                if (!flag.AutoWork) MessageBox.Show("Информация получена!\n См. статусную строку");
            }
            else
            {
                //ListBoxLog.Items.Insert(0, "cmd 0x10 Get Address -> Ошибка!!!");
                //ListBoxLog.Items.Insert(0, "TX: 68 05 00 10 15");
                ListBoxLog.Items.Insert(0, "Ошибка " + pdata.errorRX.ToString());
                ListBoxLog.Items.Insert(0, "RX: " + pdata.readbuff);
            }
        }

        private void btnGetCodeAlarmHandle_Click(object sender, EventArgs e)
        {
            btnGetCodeAlarmHandle.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.GetAlarmHandle() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x0C / 01  Get Alarm Description -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Коды и доп. датчики");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x0C / 01 Get Alarm Description -> Ошибка!!!");
        }

        private void btnGetCodeFaultyHandle_Click(object sender, EventArgs e)
        {
            btnGetCodeFaultyHandle.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;
            
            if (pdata.GetFaultyHandle() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x0C / 02  Get Faulty Description -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Информация получена!\n Меню->Устройство->Коды и доп. датчики");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x0C / 02 Get Faulty Description -> Ошибка!!!");
        }

        private void btnResetAlarm_Click(object sender, EventArgs e)
        {
            btnResetAlarm.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.AlarmReset() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x0D  Reset Alarm -> Успешно");
                if (!flag.AutoWork)  MessageBox.Show("Тревоги сброшены!");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x0D Reset Alarm -> Ошибка!!!");
        }

        private void btnExNumer_Click(object sender, EventArgs e)
        {
            int n1, n2, n3, n4, kv, y1, y2, y3, y4;
            
            btnExNumer.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            //MessageBox.Show("Установка серийного номера производится через Меню -> Устройство -> Команды");

            n1 = Int32.Parse(editN1.Text);
            n2 = Int32.Parse(editN2.Text);
            n3 = Int32.Parse(editN3.Text);
            n4 = Int32.Parse(editN4.Text);
            kv = Int32.Parse(editKv.Text);
            y1 = Int32.Parse(editY1.Text);
            y2 = Int32.Parse(editY2.Text);
            y3 = Int32.Parse(editY3.Text);
            y4 = Int32.Parse(editY4.Text);


            if (pdata.ExecuteSerialNumer(n1, n2, n3, n4, kv, y1, y2, y3, y4) == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x0F/20  Set Serial Numer -> Успешно");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x0F/20  Set Serial Numer -> Ошибка!!!");
        }

        private void btnExReset_Click(object sender, EventArgs e)
        {
            btnExReset.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.ExecuteF1() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x0F / F1  Reset MCU -> Успешно");
                MessageBox.Show("Устройство перезагружено!");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x0F / F1 Reset MCU -> Ошибка!!!");
        }

        private void btnExSetDef_Click(object sender, EventArgs e)
        {
            btnExSetDef.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.ExecuteF2() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x0F / F2  Setting Default -> Успешно");
                MessageBox.Show("Настройки сброшены к заводским установкам!");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x0F / F2 Setting Default -> Ошибка!!!");
        }

        private void btnExRC_Click(object sender, EventArgs e)
        {
            btnExRC.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.ExecuteF3() == true)
            {
                ListBoxLog.Items.Insert(0, "cmd 0x0F / F3  Remote Control -> Успешно");
                MessageBox.Show("Команда ДК отправлена. Обновите статус!");
            }
            else
                ListBoxLog.Items.Insert(0, "cmd 0x0F / F3 Remote Control -> Ошибка!!!");
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void buttonStatus_Click(object sender, EventArgs e)
        {
            buttonStatus.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            if (pdata.Marker() == true)
                viewStatus();
        }

        private void buttonCode_Click(object sender, EventArgs e)
        {
            buttonCode.BackColor = Color.Blue;
            timerBtnActive.Enabled = true;

            ListBoxLog.Items.Insert(0, pdata.MarkerCode());
        }

        private void buttonAutoStatus_Click(object sender, EventArgs e)
        {
            if (flag.AutoMarker)
            {
                timerMarker.Enabled = false;
                buttonAutoStatus.BackColor = Color.SteelBlue;
                flag.AutoMarker = false;
            }
            else
            {
                buttonAutoStatus.BackColor = Color.Blue;
                timerMarker.Enabled = true;
                flag.AutoMarker = true;
            }
        }

        private void buttonStartDE_Click(object sender, EventArgs e)
        {
           if (flag.AutoWork)
            {
                timerMarker.Enabled = false;
                buttonStartDE.BackColor = Color.Green;
                flag.AutoWork = false;
            }
           else
            {
                buttonStartDE.BackColor = Color.LawnGreen;
                flag.AutoWork = true;

                if (pdata.Marker() == true)
                {
                    viewStatus();
                    btnGetAddress.PerformClick();
                    btnGetInfo.PerformClick();
                    btnGetConfig.PerformClick();
                    if (DeviceUses.qntDeviceParams != 0)
                    {
                        btnGetDeviceParamsHandle.PerformClick();
                        btnGetDeviceParams.PerformClick();
                    }
                    if (DeviceUses.qntSectorParms != 0)
                    {
                        btnGetSectorParamsHandle.PerformClick();
                        btnGetSectorParams.PerformClick();
                    }
                    if (DeviceUses.sensAdditional != 0)
                    {
                        btnGetAddSensHandle.PerformClick();
                    }
                    if (DeviceUses.qntAlarmCodes != 0)
                    {
                        btnGetCodeAlarmHandle.PerformClick();
                    }
                    if (DeviceUses.qntFaultyCodes != 0)
                    {
                        btnGetCodeFaultyHandle.PerformClick();
                    }
                    timerMarker.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Устройство не подключенО!!!!!!");
                }
            }
        }

        //**************************************************************************************************************************************************
        private void timerMarker_Tick(object sender, EventArgs e)
        {
            if (pdata.Marker() == true)
                viewStatus();
        }

        private void timerBtnActive_Tick(object sender, EventArgs e)
        {
            btnGetInfo.BackColor = Color.AliceBlue;
            btnGetConfig.BackColor = Color.AliceBlue;
            btnGetDeviceParamsHandle.BackColor = Color.AliceBlue;
            btnGetSectorParamsHandle.BackColor = Color.AliceBlue;
            btnGetDeviceParams.BackColor = Color.AliceBlue;
            btnGetSectorParams.BackColor = Color.AliceBlue;
            btnGetAddSensHandle.BackColor = Color.AliceBlue;
            btnGetAddress.BackColor = Color.AliceBlue;
            btnGetCodeAlarmHandle.BackColor = Color.AliceBlue;
            btnGetCodeFaultyHandle.BackColor = Color.AliceBlue;
            btnResetAlarm.BackColor = Color.AliceBlue;
            btnExNumer.BackColor = Color.AliceBlue;
            btnExReset.BackColor = Color.AliceBlue;
            btnExSetDef.BackColor = Color.AliceBlue;
            btnExRC.BackColor = Color.AliceBlue;
            buttonReserve.BackColor = Color.AliceBlue;

            buttonStatus.BackColor = Color.SteelBlue;
            buttonCode.BackColor = Color.SteelBlue;

            timerBtnActive.Enabled = false;
        }


        //**************************************************************************************************************************************************
        // Функции отображения
        private void viewInit()
        {
            pin1_1.BackgroundImage = pinList.Images[5];
            pin1_2.BackgroundImage = pinList.Images[0];
            pin1_3.BackgroundImage = pinList.Images[0];
            pin1_4.BackgroundImage = pinList.Images[0];
            pin1_5.BackgroundImage = pinList.Images[0];
            pin1_6.BackgroundImage = pinList.Images[0];
            pin2_1.BackgroundImage = pinList.Images[5];
            pin2_2.BackgroundImage = pinList.Images[0];
            pin2_3.BackgroundImage = pinList.Images[0];
            pin2_4.BackgroundImage = pinList.Images[0];
            pin2_5.BackgroundImage = pinList.Images[0];
            pin2_6.BackgroundImage = pinList.Images[0];
        }

        private void viewPortConnect()
        {
            pin1_1.BackgroundImage = pinList.Images[6];
            pin1_2.BackgroundImage = pinList.Images[1];
            pin2_1.BackgroundImage = pinList.Images[8];
            pin2_2.BackgroundImage = pinList.Images[3];
            pin2_3.BackgroundImage = pinList.Images[2];
            pin2_4.BackgroundImage = pinList.Images[2];
            pin2_5.BackgroundImage = pinList.Images[2];
            pin1_6.BackgroundImage = pinList.Images[4];
            pin2_6.BackgroundImage = pinList.Images[4];
            panelPort.Text = "Порт: " + serialPort.PortName;
            panelSpeed.Text = "Скорость: " + serialPort.BaudRate.ToString();
            indStatusBit0.Visible = true;
            indStatusBit1.Visible = true;
            indStatusBit2.Visible = true;
            indStatusBit3.Visible = true;
            indStatusBit4.Visible = true;
            indStatusBit5.Visible = true;
            indStatusBit6.Visible = true;
            indStatusBit7.Visible = true;
            indStatusBit8.Visible = true;
            indStatusBit9.Visible = true;
            indStatusBit10.Visible = true;
            indStatusBit11.Visible = true;
            indStatusBit12.Visible = true;
            indStatusBit13.Visible = true;
            indStatusBit14.Visible = true;
            indStatusBit15.Visible = true;
            indAS1.Visible = true;
            indAS2.Visible = true;
            indAS3.Visible = true;
            indAS4.Visible = true;
            indES1.Visible = true;
            indES2.Visible = true;
            indES3.Visible = true;
            indES4.Visible = true;
            butStateBit0.Visible = true;
            butStateBit1.Visible = true;
            butStateBit2.Visible = true;
        }

        private void viewPortDisconnect()
        {
            buttonPort.Text = "ВКЛ";
            buttonPort.BackColor = Color.Green;
            panelPort.Text = "Порт: ";
            panelSpeed.Text = "Скорость: ";
            panelAddress.Text = "Адрес: ";
            viewInit();


            indStatusBit0.Visible  = false;
            indStatusBit1.Visible  = false;
            indStatusBit2.Visible  = false;
            indStatusBit3.Visible  = false;
            indStatusBit4.Visible  = false;
            indStatusBit5.Visible  = false;
            indStatusBit6.Visible  = false;
            indStatusBit7.Visible  = false;
            indStatusBit8.Visible  = false;
            indStatusBit9.Visible  = false;
            indStatusBit10.Visible = false;
            indStatusBit11.Visible = false;
            indStatusBit12.Visible = false;
            indStatusBit13.Visible = false;
            indStatusBit14.Visible = false;
            indStatusBit15.Visible = false;
            indAS1.Visible = false;
            indAS2.Visible = false;
            indAS3.Visible = false;
            indAS4.Visible = false;
            indES1.Visible = false;
            indES2.Visible = false;
            indES3.Visible = false;
            indES4.Visible = false;
            butStateBit0.Visible = false;
            butStateBit1.Visible = false;
            butStateBit2.Visible = false;
        }

        private void viewStatus()
        {
            if ((DeviceUses.status & 0x0001) == 0x0001)
            {
                indStatusBit0.BackColor = Color.LimeGreen;
                labelStatusBit0.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit0.BackColor = Color.DarkGreen;
                labelStatusBit0.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0002) == 0x0002)
            {
                indStatusBit1.BackColor = Color.LimeGreen;
                labelStatusBit1.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit1.BackColor = Color.DarkGreen;
                labelStatusBit1.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0004) == 0x0004)
            {
                indStatusBit2.BackColor = Color.LimeGreen;
                labelStatusBit2.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit2.BackColor = Color.DarkGreen;
                labelStatusBit2.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0008) == 0x0008)
            {
                indStatusBit3.BackColor = Color.LimeGreen;
                labelStatusBit3.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit3.BackColor = Color.DarkGreen;
                labelStatusBit3.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0010) == 0x0010)
            {
                indStatusBit4.BackColor = Color.LimeGreen;
                labelStatusBit4.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit4.BackColor = Color.DarkGreen;
                labelStatusBit4.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0020) == 0x0020)
            {
                indStatusBit5.BackColor = Color.LimeGreen;
                labelStatusBit5.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit5.BackColor = Color.DarkGreen;
                labelStatusBit5.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0040) == 0x0040)
            {
                indStatusBit6.BackColor = Color.LimeGreen;
                labelStatusBit6.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit6.BackColor = Color.DarkGreen;
                labelStatusBit6.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0080) == 0x0080)
            {
                indStatusBit7.BackColor = Color.LimeGreen;
                labelStatusBit7.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit7.BackColor = Color.DarkGreen;
                labelStatusBit7.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0100) == 0x0100)
            {
                indStatusBit8.BackColor = Color.LimeGreen;
                labelStatusBit8.BackColor = Color.LimeGreen;

                timerMarker.Enabled = false;
                

            }
            else
            {
                indStatusBit8.BackColor = Color.DarkGreen;
                labelStatusBit8.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0200) == 0x0200)
            {
                indStatusBit9.BackColor = Color.LimeGreen;
                labelStatusBit9.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit9.BackColor = Color.DarkGreen;
                labelStatusBit9.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0400) == 0x0400)
            {
                indStatusBit10.BackColor = Color.LimeGreen;
                labelStatusBit10.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit10.BackColor = Color.DarkGreen;
                labelStatusBit10.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x0800) == 0x0800)
            {
                indStatusBit11.BackColor = Color.LimeGreen;
                labelStatusBit11.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit11.BackColor = Color.DarkGreen;
                labelStatusBit11.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x1000) == 0x1000)
            {
                indStatusBit12.BackColor = Color.LimeGreen;
                labelStatusBit12.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit12.BackColor = Color.DarkGreen;
                labelStatusBit12.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x2000) == 0x2000)
            {
                indStatusBit13.BackColor = Color.LimeGreen;
                labelStatusBit13.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit13.BackColor = Color.DarkGreen;
                labelStatusBit13.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x4000) == 0x4000)
            {
                indStatusBit14.BackColor = Color.LimeGreen;
                labelStatusBit14.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit14.BackColor = Color.DarkGreen;
                labelStatusBit14.BackColor = Color.DarkGreen;
            }

            if ((DeviceUses.status & 0x8000) == 0x8000)
            {
                indStatusBit15.BackColor = Color.LimeGreen;
                labelStatusBit15.BackColor = Color.LimeGreen;
            }
            else
            {
                indStatusBit15.BackColor = Color.DarkGreen;
                labelStatusBit15.BackColor = Color.DarkGreen;
            }
        }

//***************************************************************************************************************************************************
        /** Меню -> Устройство */
        private void MenuHandle_Click(object sender, EventArgs e)
        {
            HandleForm handleForm = new HandleForm();
            handleForm.Owner = this;  //сообщаем окну кто его владелец
            handleForm.ShowDialog();
        }

        private void MenuParams_Click(object sender, EventArgs e)
        {
            //serialPort.Close();
            //timerMarker.Enabled = false;
            ParamsForm paramsForm = new ParamsForm(this);
            paramsForm.Owner = this;
            paramsForm.ShowDialog();
            //timerMarker.Enabled = true;
            //serialPort.Open();
        }

        private void MenuAddSensHandle_Click(object sender, EventArgs e)
        {
            DescriptForm descriptForm = new DescriptForm();
            descriptForm.Owner = this;
            descriptForm.ShowDialog();
        }

        private void MenuCommands_Click(object sender, EventArgs e)
        {
            //serialPort.Close();
            //timerMarker.Enabled = false;
            CommandForm commandForm = new CommandForm(this);
            commandForm.Owner = this;
            commandForm.ShowDialog();
            //timerMarker.Enabled = true;
            //serialPort.Open();
        }
    }
}
