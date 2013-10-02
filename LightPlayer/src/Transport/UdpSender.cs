using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Intems.LightPlayer.Transport
{
    public class UdpSender : IPackageSender
    {
        private readonly Socket _socket;
        private readonly IPEndPoint _endPoint;

        public UdpSender(string ipAddr, int port)
        {
            var remoteAddr = IPAddress.Parse(ipAddr);
            _endPoint = new IPEndPoint(remoteAddr, port);

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

        public void SendPackages(IEnumerable<Package> packages)
        {
            foreach (var package in packages)
                SendPackage(package);
        }

        public string Address
        {
            get { return _endPoint.Address.MapToIPv4().ToString(); }
        }

        public int Port
        {
            get { return _endPoint.Port; }
        }
    }
}
