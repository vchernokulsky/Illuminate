using System;
using System.IO;
using System.IO.Ports;

namespace Intems.LightPlayer.Transport
{
    public class SerialportSender : IPackageSender
    {
        private readonly SerialPort _port;

#if DEBUG
        private readonly StreamWriter _writer;
#endif

        public SerialportSender(SerialPort port)
        {
            _port = port;
#if DEBUG
            var parentDir = Directory.GetParent(@"..\");
            _writer = new StreamWriter(Path.Combine(parentDir.FullName, "send.log"));
#endif
        }

        public void SendPackage(Package package)
        {
            try
            {
                if (_port != null && _port.IsOpen)
                {
                    var buffer = package.ToArray();
                    _port.Write(buffer, 0, buffer.Length);

                    WriteLog(package);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private void WriteLog(Package package)
        {
#if DEBUG
            var dt = DateTime.Now;
            _writer.Write(dt.ToString("mm:ss.fff") + "    :    ");
            _writer.WriteLine(package.ToString());
            _writer.Flush();
#endif
        }
    }
}
