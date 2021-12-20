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
    public partial class DescriptForm : Form
    {
        public DescriptForm()
        {
            InitializeComponent();
        }

        private void DescriptForm_Load(object sender, EventArgs e)
        {
            textBoxAddSens1.Text = DeviceUses.addSensHandle[0];
            textBoxAddSens2.Text = DeviceUses.addSensHandle[1];
            textBoxAddSens3.Text = DeviceUses.addSensHandle[2];
            textBoxAddSens4.Text = DeviceUses.addSensHandle[3];

            textBoxAlarmCode1.Text = DeviceUses.alarmHandle[0];
            textBoxAlarmCode2.Text = DeviceUses.alarmHandle[1];
            textBoxAlarmCode3.Text = DeviceUses.alarmHandle[2];
            textBoxAlarmCode4.Text = DeviceUses.alarmHandle[3];
            textBoxAlarmCode5.Text = DeviceUses.alarmHandle[4];
            textBoxAlarmCode6.Text = DeviceUses.alarmHandle[5];
            textBoxAlarmCode7.Text = DeviceUses.alarmHandle[6];
            textBoxAlarmCode8.Text = DeviceUses.alarmHandle[7];
            textBoxAlarmCode9.Text = DeviceUses.alarmHandle[8];
            textBoxAlarmCode10.Text = DeviceUses.alarmHandle[9];
            textBoxAlarmCode11.Text = DeviceUses.alarmHandle[10];
            textBoxAlarmCode12.Text = DeviceUses.alarmHandle[11];
            textBoxAlarmCode13.Text = DeviceUses.alarmHandle[12];
            textBoxAlarmCode14.Text = DeviceUses.alarmHandle[13];
            textBoxAlarmCode15.Text = DeviceUses.alarmHandle[14];
            textBoxAlarmCode16.Text = DeviceUses.alarmHandle[15];

            textBoxFaultyCode1.Text = DeviceUses.faultyHandle[0];
            textBoxFaultyCode2.Text = DeviceUses.faultyHandle[1];
            textBoxFaultyCode3.Text = DeviceUses.faultyHandle[2];
            textBoxFaultyCode4.Text = DeviceUses.faultyHandle[3];
            textBoxFaultyCode5.Text = DeviceUses.faultyHandle[4];
            textBoxFaultyCode6.Text = DeviceUses.faultyHandle[5];
            textBoxFaultyCode7.Text = DeviceUses.faultyHandle[6];
            textBoxFaultyCode8.Text = DeviceUses.faultyHandle[7];
            textBoxFaultyCode9.Text = DeviceUses.faultyHandle[8];
            textBoxFaultyCode10.Text = DeviceUses.faultyHandle[9];
            textBoxFaultyCode11.Text = DeviceUses.faultyHandle[10];
            textBoxFaultyCode12.Text = DeviceUses.faultyHandle[11];
            textBoxFaultyCode13.Text = DeviceUses.faultyHandle[12];
            textBoxFaultyCode14.Text = DeviceUses.faultyHandle[13];
            textBoxFaultyCode15.Text = DeviceUses.faultyHandle[14];
            textBoxFaultyCode16.Text = DeviceUses.faultyHandle[15];
        }
    }
}
