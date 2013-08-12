﻿using System;
using System.IO;
using System.IO.Ports;

namespace Intems.LightPlayer.Transport
{
    public class SerialPortSender : IPackageSender
    {
        private readonly SerialPort _port;

#if DEBUG
        private readonly StreamWriter _writer;

        public SerialPortSender()
        {
            _writer = new StreamWriter("send.log");
        }
#endif
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
