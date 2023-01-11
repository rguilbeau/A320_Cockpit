using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A320_Cockpit.Domain.CanBus;

namespace A320_Cockpit.Adapter.CanBusAdapter.SerialCanBusAdapter
{
    internal class SerialCan : ICanBus
    {
        private readonly SerialPort serialPort;

        private readonly string comPort;

        private readonly int serialBaudRate;

        private readonly string canBaudRate;

        //public event EventHandler<MessageRecievedEventArgs>? MessageReceivedEvent;

        public SerialCan(SerialPort serialPort, string comPort, int serialBaudRate, string canBaudRate)
        {
            this.serialPort = serialPort;
            this.comPort = comPort;
            this.serialBaudRate = serialBaudRate;
            this.canBaudRate = canBaudRate;
            this.serialPort.DataReceived += _serialPort_DataReceived;
        }

        public bool Open()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }

            // open serial connexion
            serialPort.PortName = comPort;
            serialPort.BaudRate = serialBaudRate;

            serialPort.Open();

            if (serialPort.IsOpen)
            {
                // open can bus connexion
                serialPort.Write("O\r");
                // set bitrate
                serialPort.Write("S");
                serialPort.Write(canBaudRate);
                serialPort.Write("\r");

                CheckHardware();
            }

            return serialPort.IsOpen;
        }

        public void Close()
        {
            serialPort.Close();
        }

        private bool CheckHardware()
        {
            bool isOk = false;

            if (serialPort.IsOpen)
            {
                serialPort.DataReceived -= _serialPort_DataReceived;
                serialPort.Write("V\r");
                string version = serialPort.ReadExisting();
                isOk = version.Contains("cantact");
                serialPort.DataReceived += _serialPort_DataReceived;

                if (!isOk)
                {
                    serialPort.Close();
                    throw new Exception("Hardware is not a CANable. Please flash with slcan firmware");
                }
            }

            return isOk;
        }

        public bool IsOpen
        {
            get { return serialPort.IsOpen; }
        }

        public bool Send(Frame frame)
        {
            bool success = false;

            if (serialPort.IsOpen)
            {
                string canFrameData = "t";
                canFrameData += frame.Id.ToString("X").PadLeft(3, '0');
                canFrameData += frame.Size.ToString();

                foreach (int value in frame.Data)
                {
                    canFrameData += value.ToString("X").PadLeft(2);
                }

                serialPort.Write(canFrameData);
                serialPort.Write("\r");
                success = true;
            }

            return success;
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            /*try
            {
                if (MessageReceivedEvent != null)
                {
                    string frame = serialPort.ReadExisting();
                    if (frame != null && frame.StartsWith("t"))
                    {
                        int id = int.Parse(frame.Substring(1, 3), System.Globalization.NumberStyles.HexNumber);
                        int size = int.Parse(frame.Substring(4, 1));

                        CanMessage message = new(id, size);

                        for (int i = 0; i < size; i++)
                        {
                            message.Data[i] = byte.Parse(frame.Substring(5 + i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                        }

                        MessageReceivedEvent.Invoke(this, new MessageRecievedEventArgs(message));
                    }
                }
            }
            catch (TimeoutException)
            {
            }*/
        }

        public static string[] FindAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            return ports.Distinct().ToArray();
        }
    }
}