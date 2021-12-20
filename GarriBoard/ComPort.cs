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
using System.IO.Ports;

namespace GarriBoard
{
    public struct cmd
    {
        public static readonly byte[] marker = { 0x10 };
        public static readonly byte[] code = { 0xAA };
        
        public static readonly byte[] getInfo = { 0x68, 0x05, 0x00, 0x01, 0x04 };
        public static readonly byte[] getGonfig = { 0x68, 0x05, 0x00, 0x02, 0x07 };
        public static readonly byte[] getDeviceParamsHandle = { 0x68, 0x05, 0x00, 0x03, 0x06 };
        public static readonly byte[] getSectorParamsHandle = { 0x68, 0x05, 0x00, 0x04, 0x01 };
        public static readonly byte[] getDeviceParams = { 0x68, 0x05, 0x00, 0x05, 0x00 };
        public static readonly byte[] getSectorParams = { 0x68, 0x06, 0x00, 0x06, 0x00, 0x00 };
        public static readonly byte[] getAddSensHandle = { 0x68, 0x05, 0x00, 0x0B, 0x0E };
        public static readonly byte[] getAlarmHandle = { 0x68, 0x06, 0x00, 0x0C, 0x01, 0x0B };
        public static readonly byte[] getFaultyHandle = { 0x68, 0x06, 0x00, 0x0C, 0x02, 0x08 };
        public static readonly byte[] resetAlarm = { 0x68, 0x05, 0x00, 0x0D, 0x08 };

        public static byte[] setState = { 0x68, 0x07, 0x00, 0x0E, 0x00, 0x00, 0x09 };
        public static readonly byte[] executeResetMCU = { 0x68, 0x06, 0x00, 0x0F, 0xF1, 0xF8 };
        public static readonly byte[] executeSettingClear = { 0x68, 0x06, 0x00, 0x0F, 0xF2, 0xFB };
        public static readonly byte[] executeRC = { 0x68, 0x06, 0x00, 0x0F, 0xF3, 0xFA };
        public static byte[] executeSerialNumer = { 0x68, 0x16, 0x00, 0x0F, 0xF1, 0xB9, 0x30, 0x30, 0x30, 0x30, 0x20, 0xEA, 0xE2,
                                                    0x30, 0x20, 0x30, 0x30, 0x30, 0x30, 0xE3, 0x00, 0x8A };
        public static readonly byte[] getAddrees = { 0x68, 0x05, 0x00, 0x10, 0x15 };

        public static byte[] setDeviceParam = { 0x68, 0x08, 0x00, 0x07, 0x00, 0x00, 0x00, 0x0F };
        public static byte[] setDeviceParamMatrix = new byte[69];
        public static byte[] setSectorParam = { 0x68, 0x0A, 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03 };
        public static byte[] setSectorParamMatrix = new byte[2053];
    }

    public class DataProc  //static - чтобы не созавать объект класс, а использовать сам класс в качестве объекта
    {
        public DataProc(SerialPort p)
        {
            port = p;
            packet = new byte[PACKET_FRAMES];
        }

        private const int PACKET_FRAMES = 0xFFFF;
        private const int HEAD_CMD = 0x68;
        private const int SC = 0xE5;
        private const int NACK = 0x1F;
        private const int SD1 = 0x10;
        private const int SD2 = 0xAA;

        SerialPort port;
        public byte[] packet;  //принятый пакет данных
        public string readbuff;
        public int errorRX;
        //public bool isOpen;  //test



        public bool isConnect()
        {
            port.Write(cmd.marker, 0, 1);
            try
            {
                port.Read(packet, 0, 2);
                return true;
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос статуса!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool Marker()
        {
            port.Write(cmd.marker, 0, 1);
            try
            {
                port.Read(packet, 0, 2);

                DeviceUses.status = packet[0] + packet[1] * 256;
                return true;
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос статуса!!!  "
                      + e.Message, "Error!");
            }

            return false;
        }



        public bool GetInfo()
        {
            portClear();
            port.Write(cmd.getInfo, 0, cmd.getInfo.Length);
            try
            {
                //port.Read(packet, 0, 117);
                recivePacket();
                if (processPacket(0x01) == true)
                {
                    DeviceUses.factoryName = DeviceUses.ConvertStr(packet, 4, 16);
                    DeviceUses.deviceCode = DeviceUses.ConvertStr(packet, 20, 16);
                    DeviceUses.version = DeviceUses.ConvertStr(packet, 36, 16);
                    DeviceUses.email = DeviceUses.ConvertStr(packet, 52, 16);
                    DeviceUses.deviceName = DeviceUses.ConvertStr(packet, 68, 16);
                    DeviceUses.deviceModel = DeviceUses.ConvertStr(packet, 84, 16);
                    DeviceUses.serialNumer = DeviceUses.ConvertStr(packet, 100, 16);

                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос информации (0х01)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetConfig()
        {
            portClear();
            port.Write(cmd.getGonfig, 0, cmd.getGonfig.Length);
            try
            {
                //port.Read(packet, 0, 14);
                recivePacket();
                if (processPacket(0x02) == true)
                {
                    DeviceUses.readyTime = packet[4];
                    DeviceUses.sensExternal = packet[5];
                    DeviceUses.sensAdditional = packet[6];
                    DeviceUses.qntRelay = packet[7];
                    DeviceUses.qntDeviceParams = packet[8];
                    DeviceUses.qntSector = packet[9];
                    DeviceUses.qntSectorParms = packet[10];
                    DeviceUses.qntAlarmCodes = packet[11];
                    DeviceUses.qntFaultyCodes = packet[12];

                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос конфигурации (0х02)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetDeviceParamsHandle()
        {
            portClear();
            port.Write(cmd.getDeviceParamsHandle, 0, cmd.getDeviceParamsHandle.Length);
            try
            {
                recivePacket();
                if (processPacket(0x03) == true)
                {
                    for (int i = 0; i < DeviceUses.qntDeviceParams; i++)
                    {
                        DeviceUses.handlDevice[i].hType    = packet[0 + i * 24 + 4];
                        DeviceUses.handlDevice[i].hAccess  = packet[1 + i * 24 + 4];
                        DeviceUses.handlDevice[i].maxValue = packet[2 + i * 24 + 4] + packet[3 + i * 24 + 4] * 256;
                        DeviceUses.handlDevice[i].minValue = packet[4 + i * 24 + 4] + packet[5 + i * 24 + 4] * 256;
                        DeviceUses.handlDevice[i].defaultValue = packet[6 + i * 24 + 4] + packet[7 + i * 24 + 4] * 256;
                        DeviceUses.handlDevice[i].hName = DeviceUses.ConvertStr(packet, 8 + i * 24 + 4, 16);
                    }

                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос описания параметров средства (0х03)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetSectorParamsHandle()
        {
            portClear();
            port.Write(cmd.getSectorParamsHandle, 0, cmd.getSectorParamsHandle.Length);
            try
            {
                recivePacket();
                if (processPacket(0x04) == true)
                {
                    for (int i = 0; i < DeviceUses.qntSectorParms; i++)
                    {
                        for (int j = 0; j < 128; j++)
                        {
                            DeviceUses.handlSector[i, j].hType = packet[0 + i * 24 + 4];
                            DeviceUses.handlSector[i, j].hAccess = packet[1 + i * 24 + 4];
                            DeviceUses.handlSector[i, j].maxValue = packet[2 + i * 24 + 4] + packet[3 + i * 24 + 4] * 256;
                            DeviceUses.handlSector[i, j].minValue = packet[4 + i * 24 + 4] + packet[5 + i * 24 + 4] * 256;
                            DeviceUses.handlSector[i, j].defaultValue = packet[6 + i * 24 + 4] + packet[7 + i * 24 + 4] * 256;
                            DeviceUses.handlSector[i, j].hName = DeviceUses.ConvertStr(packet, 8 + i * 24 + 4, 16);
                        }
                    }

                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос описания параметров сектора (0х04)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetDeviceParams()
        {
            portClear();
            port.Write(cmd.getDeviceParams, 0, cmd.getDeviceParams.Length);
            try
            {
                recivePacket();
                if (processPacket(0x05) == true)
                {
                    for (int i = 0; i < DeviceUses.qntDeviceParams; i++)
                    {
                        DeviceUses.handlDevice[i].value = packet[0 + i * 2 + 4] + packet[1 + i * 2 + 4] * 256;
                    }

                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос параметров средства (0х05)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetSectorParams()
        {
            portClear();
            port.Write(cmd.getSectorParams, 0, cmd.getSectorParams.Length);
            try
            {
                recivePacket();
                if (processPacket(0x06) == true)
                {
                    for (int j = 0; j < DeviceUses.qntSector; j++)
                    {
                        for (int i = 0; i < DeviceUses.qntSectorParms; i++)
                        {
                            DeviceUses.handlSector[i, j].value = packet[(0 + i * 2 + 4) + j * 4] +
                                packet[(1 + i * 2 + 4) + j * 4] * 256;
                        }
                    }
                    
                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос параметров сектора (0х06)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetAddSensHandle()
        {
            portClear();
            port.Write(cmd.getAddSensHandle, 0, cmd.getAddSensHandle.Length);
            try
            {
                recivePacket();
                if (processPacket(0x0B) == true)
                {
                    for (int i = 0; i < DeviceUses.sensAdditional; i++)
                    {
                        DeviceUses.addSensHandle[i] = DeviceUses.ConvertStr(packet, i * 16 + 4, 16);
                    }

                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос описания доп. датчиков (0х0B)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetAlarmHandle()
        {
            portClear();
            port.Write(cmd.getAlarmHandle, 0, cmd.getAlarmHandle.Length);
            try
            {
                recivePacket();
                if (processPacket(0x0C) == true)
                {
                    for (int i = 0; i < DeviceUses.qntAlarmCodes; i++)
                    {
                        DeviceUses.alarmHandle[i] = DeviceUses.ConvertStr(packet, i * 16 + 4, 16);
                    }

                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос описания тревог (0х0C / 01)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetFaultyHandle()
        {
            portClear();
            port.Write(cmd.getFaultyHandle, 0, cmd.getFaultyHandle.Length);
            try
            {
                recivePacket();
                if (processPacket(0x0C) == true)
                {
                    for (int i = 0; i < DeviceUses.qntFaultyCodes; i++)
                    {
                        DeviceUses.faultyHandle[i] = DeviceUses.ConvertStr(packet, i * 16 + 4, 16);
                    }

                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос описания неисправностей (0х0C / 02)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool AlarmReset()
        {
            portClear();
            port.Write(cmd.resetAlarm, 0, cmd.resetAlarm.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на сброс тревог (0х0D)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool SetState(UInt16 bitState)
        {
            byte cxr = 0;
            
            DeviceUses.state |= (UInt16)(1 << bitState);
            cmd.setState[4] = (byte)DeviceUses.state;
            cmd.setState[5] = (byte)(DeviceUses.state >> 8);

            for (int i = 1; i < cmd.setState.Length - 2; i++)
            {
                cxr ^= cmd.setState[i];
            }
            cmd.setState[6] = cxr;

            portClear();
            port.Write(cmd.setState, 0, cmd.setState.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Нельзя изменить данное состояние!!!");
                    return false;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на установку нового состояния (0х0E)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool ClrState()
        {
            byte cxr = 0;
            
            DeviceUses.state = 0x0000;
            cmd.setState[4] = (byte)DeviceUses.state;
            cmd.setState[5] = (byte)(DeviceUses.state >> 8);

            for (int i = 1; i < cmd.setState.Length - 2; i++)
            {
                cxr ^= cmd.setState[i];
            }
            cmd.setState[6] = cxr;

            portClear();
            port.Write(cmd.setState, 0, cmd.setState.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на сброс всех состояний (0х0E)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool ClrState(UInt16 bitState)
        {
            byte cxr = 0;

            DeviceUses.state |= (UInt16)(0 << bitState);
            cmd.setState[4] = (byte)DeviceUses.state;
            cmd.setState[5] = (byte)(DeviceUses.state >> 8);

            for (int i = 1; i < cmd.setState.Length - 2; i++)
            {
                cxr ^= cmd.setState[i];
            }
            cmd.setState[6] = cxr;

            portClear();
            port.Write(cmd.setState, 0, cmd.setState.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Нельзя изменить данное состояние!!!");
                    return false;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на сброс состояния (0х0E)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool ExecuteF1()
        {
            portClear();
            port.Write(cmd.executeResetMCU, 0, cmd.executeResetMCU.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Данная команда не поддерживается!!!");
                    return false;
                }

            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на команду перезапуска (0х0F / F1)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool ExecuteF2()
        {
            portClear();
            port.Write(cmd.executeSettingClear, 0, cmd.executeSettingClear.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Данная команда не поддерживается!!!");
                    return false;
                }

            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на команду сброса к зав. настройкам (0х0F / F2)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool ExecuteF3()
        {
            portClear();
            port.Write(cmd.executeRC, 0, cmd.executeRC.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Данная команда не поддерживается!!!");
                    return false;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на команду ДК (0х0F / F3)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool ExecuteSerialNumer(int n1, int n2, int n3, int n4, int kv, int y1, int y2, int y3, int y4)
        {
            byte cxr = 0;

            cmd.executeSerialNumer[6] = (byte)n1;
            cmd.executeSerialNumer[7] = (byte)n2;
            cmd.executeSerialNumer[8] = (byte)n3;
            cmd.executeSerialNumer[9] = (byte)n4;
            cmd.executeSerialNumer[13] = (byte)kv;
            cmd.executeSerialNumer[15] = (byte)y1;
            cmd.executeSerialNumer[16] = (byte)y2;
            cmd.executeSerialNumer[17] = (byte)y3;
            cmd.executeSerialNumer[18] = (byte)y4;
            
            for (int i = 1; i < 21; i++)
            {
                cxr ^= cmd.executeSerialNumer[i];
            }
            cmd.executeSerialNumer[21] = cxr;

            portClear();
            port.Write(cmd.executeSerialNumer, 0, cmd.executeSerialNumer.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Данная команда не поддерживается!!!");
                    return false;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на команду установки серийного номера (0х0F / 20)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool GetAddress()
        {
            portClear();
            port.Write(cmd.getAddrees, 0, cmd.getAddrees.Length);
            try
            {
                recivePacket();
                if (processPacket(0x10) == true)
                {
                    DeviceUses.address = packet[4];
                    return true;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос адреса (0х10)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool SetDeviceParams(int num, UInt16 value)
        {
            byte cxr = 0;

            cmd.setDeviceParam[4] = (byte)num;
            cmd.setDeviceParam[5] = (byte)value;
            cmd.setDeviceParam[6] = (byte)(value >> 8);

            for (int i = 1; i < 7; i++)
            {
                cxr ^= cmd.setDeviceParam[i];
            }
            cmd.setDeviceParam[7] = cxr;

            portClear();
            port.Write(cmd.setDeviceParam, 0, cmd.setDeviceParam.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Данная команда не поддерживается!!!");
                    return false;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на установку параметров средства (0х07)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool SetDeviceParamsMatrix(int params_count)
        {
            byte cxr = 0;
            int len = params_count + 5;
            int param_i = 1;

            cmd.setDeviceParamMatrix[0] = 0x68;
            cmd.setDeviceParamMatrix[1] = (byte)len;
            cmd.setDeviceParamMatrix[2] = (byte)(len >> 8);
            cmd.setDeviceParamMatrix[3] = 0x08;

            for (int i = 4; i < (params_count + 1) * 2; i += 2)
            {
                cmd.setDeviceParamMatrix[i] = (byte)DeviceUses.handlDevice[param_i].newValue;
                cmd.setDeviceParamMatrix[i + 1] = (byte)(DeviceUses.handlDevice[param_i].newValue >> 8);
            }

            for (int i = 1; i < len; i++)
            {
                cxr ^= cmd.setDeviceParamMatrix[i];
            }
            cmd.setDeviceParamMatrix[8] = cxr;

            portClear();
            port.Write(cmd.setDeviceParam, 0, cmd.setDeviceParam.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Данная команда не поддерживается!!!");
                    return false;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на установку матрицы параметров средства (0х08)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool SetSectorParams(int offset, int count, int num, UInt16 value)
        {
            byte cxr = 0;

            cmd.setSectorParam[4] = (byte)offset;
            cmd.setSectorParam[5] = (byte)count;
            cmd.setSectorParam[6] = (byte)num;
            cmd.setSectorParam[7] = (byte)value;
            cmd.setSectorParam[8] = (byte)(value >> 8);

            for (int i = 1; i < 9; i++)
            {
                cxr ^= cmd.setSectorParam[i];
            }
            cmd.setSectorParam[9] = cxr;

            portClear();
            port.Write(cmd.setSectorParam, 0, cmd.setSectorParam.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Данная команда не поддерживается!!!");
                    return false;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на установку параметров сектора (0х09)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }

        public bool SetSectorParamsMatrix(int offset, int count, int params_count)
        {
            byte cxr = 0;
            int len = params_count + 5 + 2;
            int param_i = 1;

            cmd.setSectorParamMatrix[0] = 0x68;
            cmd.setSectorParamMatrix[1] = (byte)len;
            cmd.setSectorParamMatrix[2] = (byte)(len >> 8);
            cmd.setSectorParamMatrix[3] = 0x0A;

            cmd.setSectorParamMatrix[4] = (byte)offset;
            cmd.setSectorParamMatrix[5] = (byte)count;

            for (int i = 6; i < (params_count + 1) * 2; i += 2)
            {
                cmd.setSectorParamMatrix[i] = (byte)DeviceUses.handlSector[param_i, offset].newValue;
                cmd.setSectorParamMatrix[i + 1] = (byte)(DeviceUses.handlSector[param_i, offset].newValue >> 8);
            }

            for (int i = 1; i < len; i++)
            {
                cxr ^= cmd.setSectorParamMatrix[i];
            }
            cmd.setSectorParamMatrix[8] = cxr;

            portClear();
            port.Write(cmd.setDeviceParam, 0, cmd.setDeviceParam.Length);
            try
            {
                packet[0] = (byte)port.ReadByte();
                if (packet[0] == SC)
                    return true;
                else if (packet[0] == NACK)
                {
                    MessageBox.Show("Данная команда не поддерживается!!!");
                    return false;
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на установку параметров сектора (0х09)!!!  "
                     + e.Message, "Error!");
            }

            return false;
        }



        public string MarkerCode()
        {
            int code_num = 0;

            portClear();
            port.Write(cmd.code, 0, cmd.code.Length);
            try
            {
                port.Read(packet, 0, DeviceUses.qntSector + 2);


                //Расшифровка типа события
                for (int i = 0; i < DeviceUses.qntSector; i++)
                {
                    if ((packet[i + 2] & 0x10) == 0x10)
                    {
                        code_num = packet[i + 2] & 0x0F;
                    }
                    return "Тревога внуть объекта - " + DeviceUses.alarmHandle[code_num];
                }

                for (int i = 0; i < DeviceUses.qntSector; i++)
                {
                    if ((packet[i + 2] & 0x20) == 0x20)
                    {
                        code_num = packet[i + 2] & 0x0F;
                    }
                    return "Тревога с объекта - " + DeviceUses.alarmHandle[code_num];
                }

                for (int i = 0; i < DeviceUses.qntSector; i++)
                {
                    if ((packet[i + 2] & 0x30) == 0x30)
                    {
                        code_num = packet[i + 2] & 0x0F;
                    }
                    return "Тревога - " + DeviceUses.alarmHandle[code_num];
                }

                for (int i = 0; i < DeviceUses.qntSector; i++)
                {
                    if ((packet[i + 2] & 0x40) == 0x40)
                    {
                        code_num = packet[i + 2] & 0x0F;
                    }
                    return "Тревога доп. датчика - " + DeviceUses.addSensHandle[code_num];
                }

                for (int i = 0; i < DeviceUses.qntSector; i++)
                {
                    if ((packet[i + 2] & 0x80) == 0x80)
                    {
                        code_num = packet[i + 2] & 0x0F;
                    }
                    return "Неисправность - " + DeviceUses.faultyHandle[code_num];
                }
            }
            catch (TimeoutException e)
            {
                MessageBox.Show("Устройство не ответило на запрос кода события (0хAA)!!!  "
                     + e.Message, "Error!");
            }

            return "Код неопределён";
        }




        private void recivePacket()
        {
            int len = 0;

            packet[0] = (byte)port.ReadByte();
            packet[1] = (byte)port.ReadByte();
            packet[2] = (byte)port.ReadByte();

            len = packet[1] + packet[2] * 256;
            port.Read(packet, 3, len - 3);
        }

        private bool processPacket(byte command)
        {
            int cxr = 0;
            int len = packet[1] + packet[2] * 256;

            for (int i = 1; i < len - 1; i++)
                cxr ^= packet[i];

            readbuff = "";
            for (int i = 0; i < len; i++)
                readbuff += packet[i].ToString() + " ";

            if (packet[0] != HEAD_CMD)
            {
                errorRX = 1;
                return false;
            }
                
            if (packet[len - 1] != cxr)
            {
                errorRX = 2;
                return false;
            }
               
            if (packet[3] != command)
            {
                errorRX = 3;
                return false;
            }


            errorRX = 0;
            return true;
        }



        private void portClear()
        {
            port.DiscardInBuffer();  //очистка приёмного буфера
            port.DiscardOutBuffer();  //очистка буфера передачи

            for (int i = 0; i < packet.Length; i++)
                packet[i] = 0x00;
        }
    }
}
