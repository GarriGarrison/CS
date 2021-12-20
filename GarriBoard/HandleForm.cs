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
    public partial class HandleForm : Form
    {
        public HandleForm()
        {
            InitializeComponent();
        }

        private void HandleForm_Load(object sender, EventArgs e)
        {
            /* Общая информация об устройстве */
            textBoxFactoryName.Text = DeviceUses.factoryName;
            textBoxDeviceCode.Text = DeviceUses.deviceCode;
            textBoxVersion.Text = DeviceUses.version;
            textBoxEmail.Text = DeviceUses.email;
            textBoxDeviceName.Text = DeviceUses.deviceName;
            textBoxDeviceModel.Text = DeviceUses.deviceModel;
            textBoxSerialNumber.Text = DeviceUses.serialNumer;

            /* Конфигурация устройства */
            textBoxReadyTime.Text = DeviceUses.readyTime.ToString();
            textBoxESQ.Text = DeviceUses.sensExternal.ToString();
            textBoxASQ.Text = DeviceUses.sensAdditional.ToString();
            textBoxRelayQnt.Text = DeviceUses.qntRelay.ToString();
            textBoxDeviceParamsQnt.Text = DeviceUses.qntDeviceParams.ToString();
            textBoxSectorQnt.Text = DeviceUses.qntSector.ToString();
            textBoxSectorParamsQnt.Text = DeviceUses.qntSectorParms.ToString();
            textBoxAlarmCodesQnt.Text = DeviceUses.qntAlarmCodes.ToString();
            textBoxFaultyCodesQnt.Text = DeviceUses.qntFaultyCodes.ToString();
        }
    }
}
