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
        private readonly List<Device> _devices;

        public DeviceDiscoverer()
        {
            _client = new UdpClient();
            _devices = new List<Device>();
        }

        public IEnumerable<Device> Discover()
        {
            try
            {
                var broadcastPoint = new IPEndPoint(IPAddress.Broadcast, Port);
                var dgram = Encoding.ASCII.GetBytes(DeviceRequest);
                _client.Send(dgram, dgram.Length, broadcastPoint);
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
                var point = new IPEndPoint(IPAddress.Loopback, Port);
                var dataBytes = _client.Receive(ref point);
                var answer = Encoding.ASCII.GetString(dataBytes);
                var newDevice = new Device(point.Address.ToString(), point.Port);
                _devices.Add(newDevice);

                Console.WriteLine(answer);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
