using A320_Cockpit.Domain.Entity.Cockpit;
using A320_Cockpit.Domain.Enum;
using System.IO.Ports;
using System.Text;

namespace A320_Cockpit.Adaptation.Canbus.ArduinoSerialCan
{
    /// <summary>
    /// Adapteur pour faire la communication au bus CAN via un Arduino
    /// </summary>
    public class ArduinoSerialCanAdapter : ICanbus
    {
        private readonly SerialPort serialPort;
        private readonly System.Timers.Timer ping;
        private int ?pingId;
        private bool pingRandomData;

        /// <summary>
        /// Création d'une nouvelle connexion
        /// </summary>
        /// <param name="serialPort">La communication avec le port USB</param>
        /// <param name="comPort">Le COM port du port USB</param>
        public ArduinoSerialCanAdapter(SerialPort serialPort, string comPort)
        {
            serialPort.BaudRate = 115200;
            serialPort.PortName = comPort;

            this.serialPort = serialPort;
            this.serialPort.DataReceived += SerialPort_DataReceived;

            pingRandomData = true;
            ping = new();
            ping.Elapsed += Ping_Elapsed;
        }

        /// <summary>
        /// Retourne si la communication est ouverte
        /// </summary>
        public bool IsOpen => serialPort.IsOpen;

        /// <summary>
        /// Evenement d'un message entrant
        /// </summary>
        public event EventHandler<Frame>? MessageReceived;

        /// <summary>
        /// Active le système de ping
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="id"></param>
        /// <param name="randomData"></param>
        public void ActivePing(int interval, FrameId pingId, bool randomData)
        {
            this.pingId = (int)pingId;
            pingRandomData = randomData;
            ping.Interval = interval;
            ping.Start();
        }

        /// <summary>
        /// Réactive le ping
        /// </summary>
        public void ResumePing()
        {
            if(!ping.Enabled && pingId != null)
            {
                ping.Start();
            }
        }

        /// <summary>
        /// Suspend le ping
        /// </summary>
        public void SuspendPing()
        {
            if(ping.Enabled && pingId != null)
            {
                ping.Stop();
            }
        }

        /// <summary>
        /// Ferme la connexion
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Close()
        {
            serialPort.Close();
            if (serialPort.IsOpen)
            {
                throw new Exception("Unable to close serial port connexion with unknwon error");
            }
        }

        /// <summary>
        /// Ouvre la connexion
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Open()
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }

            if (!serialPort.IsOpen)
            {
                throw new Exception("Unable to connect to serial bus can with unknown error");
            }
        }
        
        /// <summary>
        /// Envoi une frame au bus CAN
        /// </summary>
        /// <param name="frame"></param>
        /// <exception cref="Exception"></exception>
        public void Send(Frame frame)
        {
            if (!serialPort.IsOpen)
            {
                throw new Exception("Unable to send frame over bus, serial port is closed");
            }

            string canFrameData = frame.Id.ToString("X3");

            for(int i = 0; i < frame.Size; i++)
            {
                canFrameData += frame.Data[i].ToString("X2");
            }

            byte[] buffer = Encoding.ASCII.GetBytes(canFrameData + "\n");
            serialPort.BaseStream.WriteAsync(buffer, 0, buffer.Length);

            Console.WriteLine("--> " + frame.ToString());
        }

        /// <summary>
        /// Reception d'un nouveau message, transformation en Frame et invocation de l'event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (MessageReceived != null)
            {
                string message = serialPort.ReadLine();
                if (message != null)
                {
                    int id = int.Parse(message[..3], System.Globalization.NumberStyles.HexNumber);
                    int size = (message.Length - 3) / 2;

                    Frame frame = new(id, size);

                    for (int i = 0; i < size; i++)
                    {
                        int indexStart = (i * 2) + 3;
                        int indexEnd = indexStart + 2;
                        frame.Data[i] = byte.Parse(message[indexStart..indexEnd], System.Globalization.NumberStyles.HexNumber); ;
                    }

                    Console.WriteLine("<-- " + frame.ToString());
                    MessageReceived.Invoke(this, frame);
                }
            }
        }

        /// <summary>
        /// Lancement d'un ping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Ping_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if(pingId != null)
            {
                Frame frame = new((int)pingId, 1);

                if (pingRandomData)
                {
                    frame.Data[0] = (byte)new Random().Next(0, 255);
                }
                else
                {
                    frame.Data[0] = 0x01;
                }

                Send(frame);
            }
        }
    }
}
