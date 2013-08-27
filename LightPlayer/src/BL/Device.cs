using System.IO.Ports;
using Intems.LightPlayer.Transport;

namespace Intems.LightPlayer.BL
{
    public class Device : IPackageSender
    {
        private readonly IPackageSender _sender;

        public Device(string portName)
        {
#if DEBUG
            var serialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
            serialPort.Open();
            _sender = new SerialportSender(serialPort);
#endif
        }

        public Device(string addr, int port)
        {
            Address = addr;
            Port = port;

            _sender = new UdpSender(addr, port);
        }

        public string Address { get; set; }
        public int Port { get; set; }

        public void SendPackage(Package package)
        {
            if (_sender != null)
                _sender.SendPackage(package);
        }
    }
}
