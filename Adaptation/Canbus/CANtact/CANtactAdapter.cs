using A320_Cockpit.Domain.Entity.Cockpit;
using System.IO.Ports;

namespace A320_Cockpit.Adaptation.Canbus.CANtact
{
    /// <summary>
    /// Système de connexion au bus via le port USB et le module CANtact (firmware slcan)
    /// </summary>
    public class CANtactAdapter : ICanbus
    {
        public event EventHandler<Frame> ?MessageReceived;

        private readonly SerialPort serialPort;
        private readonly string comPort;
        private readonly int serialBaudRate;
        private readonly string canBaudRate;

        /// <summary>
        /// Création d'une nouvelle connexion
        /// </summary>
        /// <param name="serialPort">La communication avec le port USB</param>
        /// <param name="comPort">Le COM port du port USB</param>
        /// <param name="serialBaudRate">La vitesse de communication USB</param>
        /// <param name="canBaudRate">La vitesse de communicaton du CAN Bus</param>
        public CANtactAdapter(SerialPort serialPort, string comPort, int serialBaudRate, string canBaudRate)
        {
            this.serialPort = serialPort;
            this.comPort = comPort;
            this.serialBaudRate = serialBaudRate;
            this.canBaudRate = canBaudRate;
            this.serialPort.DataReceived += SerialPort_DataReceived;
        }

        /// <summary>
        /// L'état de la connexion au port USB (n'est pas actuellement capable de terminer si il est connecté au CAN BUs)
        /// </summary>
        public bool IsOpen
        {
            get { return serialPort.IsOpen; }
        }

        /// <summary>
        /// Ouvre la connexion
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Open()
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

            if (!serialPort.IsOpen)
            {
                throw new Exception("Unable to connect to serial bus can with unknown error");
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
        /// Vérifie que le module est bien un CANtact avec le firmware slcan
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private bool CheckHardware()
        {
            bool isOk = false;

            if (!serialPort.IsOpen)
            {
                throw new Exception("Unable to check hardware, serial port is closed");
            }

            try
            {
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.Write("V\r");
                string version = serialPort.ReadExisting();
                isOk = version.Contains("cantact");
                serialPort.DataReceived += SerialPort_DataReceived;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to write into Serial bus, but port is opened", e);
            }

            if (!isOk)
            {
                serialPort.Close();
                throw new Exception("Hardware is not a CANable. Please flash with slcan firmware");
            }

            return isOk;
        }

        /// <summary>
        /// Envoi une frame au CAN Bus
        /// </summary>
        /// <param name="frame">La frame à envoyer</param>
        /// <exception cref="Exception"></exception>
        public void Send(Frame frame)
        {
           
            if (!serialPort.IsOpen)
            {
                throw new Exception("Unable to send frame over bus, serial port is closed");
            }

            if (serialPort.IsOpen)
            {
                string canFrameData = "";
                canFrameData += frame.Id.ToString("X").PadLeft(3, '0');
                canFrameData += frame.Size.ToString();

                foreach (int value in frame.Data)
                {
                    canFrameData += value.ToString("X").PadLeft(2);
                }

                serialPort.Write("t" + canFrameData);
                Console.WriteLine("-> " + frame.ToString());
                serialPort.Write("\r");
            }
        }

        /// <summary>
        /// Reception d'une nouvelle frame du CAN Bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (MessageReceived != null)
                {
                    string message = serialPort.ReadExisting();
                    if (message != null && message.StartsWith("t"))
                    {
                        int id = int.Parse(message.Substring(1, 3), System.Globalization.NumberStyles.HexNumber);
                        int size = int.Parse(message.Substring(4, 1));

                        Frame frame = new(id, size);

                        for (int i = 0; i < size; i++)
                        {
                            frame.Data[i] = byte.Parse(message.Substring(5 + i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                        }

                        Console.WriteLine("<- " + frame.ToString());
                        MessageReceived.Invoke(this, frame);
                    }
                }
            }
            catch (TimeoutException)
            {
            }
        }

        /// <summary>
        /// Retourne les ports disponibles
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string[] FindAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            return ports.Distinct().ToArray();
        }
    }
}