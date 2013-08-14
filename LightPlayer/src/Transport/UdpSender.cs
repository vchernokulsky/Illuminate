using System;
using System.Net;
using System.Net.Sockets;

namespace Intems.LightPlayer.Transport
{
    class UdpSender : IPackageSender
    {
        private readonly Socket _socket;
        private readonly IPEndPoint _endPoint;

        public UdpSender(string ipAddr, string port)
        {
            var remoteAddr = IPAddress.Parse(ipAddr);
            var remotePort = Int32.Parse(port);
            _endPoint = new IPEndPoint(remoteAddr, remotePort);

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void SendPackage(Package package)
        {
            if (_socket != null)
            {
                try
                {
                    _socket.SendTo(package.ToArray(), _endPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
