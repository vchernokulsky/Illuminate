using System;
using System.IO.Ports;

namespace Intems.LightPlayer.Transport
{
    public class SerialPortSender : IPackageSender
    {
        private readonly SerialPort _port;

        public SerialPortSender()
        {
        }

        public SerialPortSender(SerialPort port)
        {
            _port = port;
        }

        public void SendPackage(Package package)
        {
            try
            {
                if (_port != null && _port.IsOpen)
                {
                    var buffer = package.ToArray();
                    _port.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
