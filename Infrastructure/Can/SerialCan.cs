using A320_Cockpit.Domain.Can;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A320_Cockpit.Infrastructure.Can
{
    internal class SerialCan: ICan
    {       
        private readonly SerialPort _serialPort;

        private readonly string _comPort;

        private readonly int _serialBaudRate;

        private readonly string _canBaudRate;

        public event EventHandler<MessageRecievedEventArgs>? MessageReceivedEvent;

        public SerialCan(SerialPort serialPort, string comPort, int serialBaudRate, string canBaudRate)
        {
            _serialPort = serialPort;
            _comPort = comPort;
            _serialBaudRate = serialBaudRate;
            _canBaudRate = canBaudRate;
            _serialPort.DataReceived += _serialPort_DataReceived;
        }

        public bool Open()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }

            // open serial connexion
            _serialPort.PortName = _comPort;
            _serialPort.BaudRate = _serialBaudRate;

            _serialPort.Open();

            if (_serialPort.IsOpen)
            {
                // open can bus connexion
                _serialPort.Write("O\r");
                // set bitrate
                _serialPort.Write("S");
                _serialPort.Write(_canBaudRate);
                _serialPort.Write("\r");

                CheckHardware();
            }

            return _serialPort.IsOpen;
        }

        public void Close()
        {
            _serialPort.Close();
        }

        private bool CheckHardware()
        {
            bool isOk = false;

            if (_serialPort.IsOpen)
            {
                _serialPort.DataReceived -= _serialPort_DataReceived;
                _serialPort.Write("V\r");
                string version = _serialPort.ReadExisting();
                isOk = version.Contains("cantact");
                _serialPort.DataReceived += _serialPort_DataReceived;
                
                if(!isOk)
                {
                    _serialPort.Close();
                    throw new Exception("Hardware is not a CANable. Please flash with slcan firmware");
                }
            }

            return isOk;
        }

        public bool IsOpen
        {
            get { return _serialPort.IsOpen; }
        }

        public bool Send(CanMessage message)
        {
            bool success = false;

            if (_serialPort.IsOpen)
            {
                string canFrameData = "t";
                canFrameData += message.Id.ToString("X").PadLeft(3, '0');
                canFrameData += message.Size.ToString();

                foreach (int value in message.Data)
                {
                    canFrameData += value.ToString("X").PadLeft(2);
                }

                _serialPort.Write(canFrameData);
                _serialPort.Write("\r");
                success = true;
            }

            return success;
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (MessageReceivedEvent != null)
                {
                    string frame = _serialPort.ReadExisting();
                    if (frame != null && frame.StartsWith("t"))
                    {
                        int id = int.Parse(frame.Substring(1, 3), System.Globalization.NumberStyles.HexNumber);
                        int size = int.Parse(frame.Substring(4, 1));

                        CanMessage message = new(id, size);

                        for (int i = 0; i < size; i++)
                        {
                            message.Data[i] = byte.Parse(frame.Substring(5 + (i * 2), 2), System.Globalization.NumberStyles.HexNumber);
                        }

                        MessageReceivedEvent.Invoke(this, new MessageRecievedEventArgs(message));
                    }
                }
            }
            catch (System.TimeoutException)
            {
            }
        }

        public static string[] FindAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            return ports.Distinct().ToArray();
        }
    }
}