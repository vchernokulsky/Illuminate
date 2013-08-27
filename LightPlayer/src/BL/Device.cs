using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using Intems.LightPlayer.Transport;

namespace Intems.LightPlayer.BL
{
    public class Device : IPackageSender
    {
        private readonly IPEndPoint _endPoint;
        private readonly IPackageSender _sender;
        private readonly SequenceCollection _sequenceCollection;


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
            _sender = new UdpSender(addr, port);
        }

        public Device(IPEndPoint point, SequenceCollection collection)
        {
            _endPoint = point;
            _sender = new UdpSender(_endPoint.Address.ToString(), _endPoint.Port);
            _sequenceCollection = collection;
        }

        public IPEndPoint EndPoint
        {
            get { return _endPoint; }
        }

        public void SetTime(TimeSpan time)
        {
            var frames = _sequenceCollection.FramesByTime(time);
            var packages =
                frames.Select(frm => frm.Command.GetBytes()).Select(bytes => new Package(bytes)).ToList();
            _sender.SendPackages(packages);
        }

        public void SendPackage(Package package)
        {
            if (_sender != null)
                _sender.SendPackage(package);
        }

        public void SendPackages(IEnumerable<Package> packages)
        {
            if(_sender != null)
                _sender.SendPackages(packages);
        }
    }
}
