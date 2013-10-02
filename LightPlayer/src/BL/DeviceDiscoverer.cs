using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Intems.LightPlayer.BL
{
    public class DeviceDiscoverer
    {
        private const int Port = 15200;
        private const string DeviceRequest = "WHERE ARE YOU?";

        private readonly UdpClient _client;
        private readonly IPEndPoint _broadcastEndPoint;

        private readonly int _expectedDevCount;
        private readonly List<Device> _devices;

        public DeviceDiscoverer() : this(Port)
        {
        }

        public DeviceDiscoverer(int port)
        {
            _devices = new List<Device>();

            var ep = new IPEndPoint(IPAddress.Any, 0);
            _client = new UdpClient(ep);
            _broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, port);
        }

        public DeviceDiscoverer(int port, int expectedDevCount) : this(port)
        {
            _expectedDevCount = expectedDevCount;
        }

        public ICollection<Device> Discover()
        {
            try
            {
                var dgram = Encoding.ASCII.GetBytes(DeviceRequest);
                _client.Send(dgram, dgram.Length, _broadcastEndPoint);
                WaitForAnswer();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return _devices;
        }

        private readonly object _locker = new object();
        private void WaitForAnswer()
        {
            try
            {
                for (int i = 0; i < _expectedDevCount; i++)
                {
                    var remoteEp = new IPEndPoint(IPAddress.Any, Port);
                    _client.BeginReceive(asyncRes =>
                    {
                        lock (_locker)
                        {
                            _client.EndReceive(asyncRes, ref remoteEp);
                            var newDevice = new Device(remoteEp);
                            _devices.Add(newDevice);
                        }
                    }, null);
                }
                //TODO: надо как-то "правильно" ожидать завершение опроса
                Thread.Sleep(3000);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
