using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Radario.Common;

namespace Intems.LightPlayer.BL
{
    public class DeviceDiscoverer
    {
        private const int Port = 15200;
        private const string DeviceRequest = "WHERE ARE YOU?";

        private readonly UdpClient _client;
        private readonly IPEndPoint _broadcastEndPoint;

        private readonly int _expectedDevCount;
        private int _timeout = 3000;

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

        public DeviceDiscoverer(int port, int expectedDevCount, int timeout) : this(port, expectedDevCount)
        {
            _timeout = timeout;
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
                SimpleLog.Log.Error(ex.Message, ex);
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
                CollectAnswersOrTimeout();

                //TODO: remove hardcoded device
                var ep = new IPEndPoint(IPAddress.Parse("192.168.1.10"), 9750);
                var dev = new Device(ep);
                _devices.Add(dev);
            }
            catch (Exception ex)
            {
                SimpleLog.Log.Error(ex.Message, ex);
            }
        }

        private void CollectAnswersOrTimeout()
        {
            bool isTimeout = false;
            bool isAllAnswersCollected = false;
            while (!isAllAnswersCollected && !isTimeout)
            {
                lock (_locker)
                {
                    isAllAnswersCollected = _devices.Count == _expectedDevCount;
                    isTimeout = ((_timeout -= 50) <= 0);
                }
                Thread.Sleep(50);
            }
        }
    }
}
