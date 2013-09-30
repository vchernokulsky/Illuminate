using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

            _client = new UdpClient();
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

        private void WaitForAnswer()
        {
            try
            {
                for (int i = 0; i < _expectedDevCount; i++)
                {
                    var remoteEp = new IPEndPoint(IPAddress.Loopback, Port);
                    byte[] dataBytes = _client.Receive(ref remoteEp);
                    var newDevice = new Device(remoteEp);
                    _devices.Add(newDevice);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
